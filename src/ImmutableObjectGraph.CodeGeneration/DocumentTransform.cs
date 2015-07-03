﻿namespace ImmutableObjectGraph.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Formatting;
    using Microsoft.CodeAnalysis.Simplification;
    using Validation;

    public class DocumentTransform
    {
        private const string GeneratedByAToolPreamble = @"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
";

        private DocumentTransform()
        {
        }

        public static async Task<Document> TransformAsync(Document inputDocument, IProgressAndErrors progress, bool simplify = false)
        {
            Requires.NotNull(inputDocument, "inputDocument");

            var workspace = inputDocument.Project.Solution.Workspace;
            var inputSemanticModel = await inputDocument.GetSemanticModelAsync();
            var inputSyntaxTree = inputSemanticModel.SyntaxTree;

            var inputFileLevelUsingDirectives = inputSyntaxTree.GetRoot().ChildNodes().OfType<UsingDirectiveSyntax>();

            var memberNodes = from syntax in inputSyntaxTree.GetRoot().DescendantNodes(n => n is CompilationUnitSyntax || n is NamespaceDeclarationSyntax || n is TypeDeclarationSyntax).OfType<MemberDeclarationSyntax>()
                              select syntax;

            var emittedMembers = new List<MemberDeclarationSyntax>();
            foreach (var memberNode in memberNodes)
            {
                var namespaceNode = memberNode.Parent as NamespaceDeclarationSyntax;

                var generators = FindCodeGenerators(inputSemanticModel, memberNode);
                foreach (var generator in generators)
                {
                    var generatedTypes = await generator.GenerateAsync(memberNode, inputDocument, progress, CancellationToken.None);
                    if (namespaceNode != null)
                    {
                        emittedMembers.Add(SyntaxFactory.NamespaceDeclaration(namespaceNode.Name)
                            .WithUsings(SyntaxFactory.List(namespaceNode.ChildNodes().OfType<UsingDirectiveSyntax>()))
                            .WithMembers(SyntaxFactory.List(generatedTypes)));
                    }
                    else
                    {
                        emittedMembers.AddRange(generatedTypes);
                    }
                }
            }

            var emittedTree = SyntaxFactory.CompilationUnit()
                .WithUsings(SyntaxFactory.List(inputFileLevelUsingDirectives).Add(
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("ImmutableObjectGraph"))))
                .WithMembers(SyntaxFactory.List(emittedMembers))
                .WithLeadingTrivia(SyntaxFactory.Comment(GeneratedByAToolPreamble))
                .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);

            // Format the tree to get reasonably good whitespace.
            var formattedTree = Formatter.Format(emittedTree, workspace, workspace.Options);

            // Reduce the document to get rid of unnecessary fully-qualified type names that just hurt readability.
            var formattedText = formattedTree.GetText();
            var document = inputDocument.Project.AddDocument("generated.cs", formattedText);
            if (simplify)
            {
                var annotatedDocument = document
                    .WithSyntaxRoot((await document.GetSyntaxRootAsync())
                    .WithAdditionalAnnotations(Simplifier.Annotation)); // allow simplification of the entire document
                document = await Simplifier.ReduceAsync(annotatedDocument);
            }

            return document;
        }

        private static IEnumerable<ICodeGenerator> FindCodeGenerators(SemanticModel document, SyntaxNode nodeWithAttributesApplied)
        {
            Requires.NotNull(document, "document");
            Requires.NotNull(nodeWithAttributesApplied, "nodeWithAttributesApplied");

            var symbol = document.GetDeclaredSymbol(nodeWithAttributesApplied);
            if (symbol != null)
            {
                foreach (var attributeData in symbol.GetAttributes())
                {
                    string generatorTypeName = GetCodeGeneratorTypeNameForAttribute(attributeData.AttributeClass);
                    if (generatorTypeName != null)
                    {
                        Type generatorType = Type.GetType(generatorTypeName);
                        ICodeGenerator generator = (ICodeGenerator)Activator.CreateInstance(generatorType, attributeData);
                        yield return generator;
                    }
                }
            }
        }

        private static string GetCodeGeneratorTypeNameForAttribute(INamedTypeSymbol attributeType)
        {
            if (attributeType != null)
            {
                foreach (var generatorCandidateAttribute in attributeType.GetAttributes())
                {
                    if (generatorCandidateAttribute.AttributeClass.Name == typeof(Generators.CodeGenerationAttribute).Name)
                    {
                        return (string)generatorCandidateAttribute.ConstructorArguments.Single().Value;
                    }
                }
            }

            return null;
        }

        private static Assembly GetAssembly(IAssemblySymbol symbol, Compilation compilation)
        {
            Requires.NotNull(symbol, "symbol");
            Requires.NotNull(compilation, "compilation");

            var matchingReferences = from reference in compilation.References.OfType<PortableExecutableReference>()
                                     where string.Equals(Path.GetFileNameWithoutExtension(reference.FilePath), symbol.Identity.Name, StringComparison.OrdinalIgnoreCase) // TODO: make this more correct
                                     select reference.FilePath;
            return Assembly.LoadFile(matchingReferences.First());
        }

        private static Type GetType(INamedTypeSymbol symbol, Compilation compilation)
        {
            Requires.NotNull(symbol, "symbol");

            var assembly = GetAssembly(symbol.ContainingAssembly, compilation);
            var nameBuilder = new StringBuilder();
            ISymbol symbolOrParent = symbol;
            while (symbolOrParent != null && !string.IsNullOrEmpty(symbolOrParent.Name))
            {
                if (nameBuilder.Length > 0)
                {
                    nameBuilder.Insert(0, ".");
                }

                nameBuilder.Insert(0, symbolOrParent.Name);
                symbolOrParent = symbolOrParent.ContainingSymbol;
            }

            Type type = assembly.GetType(nameBuilder.ToString(), true); // How to make this work more generally (nested types, etc)?
            return type;
        }
    }
}
