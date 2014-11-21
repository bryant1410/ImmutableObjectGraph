﻿namespace ImmutableObjectGraph.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.ImmutableObjectGraph_SFG;
    using Validation;

    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateImmutableAttribute : CodeGenerationAttribute
    {
        public GenerateImmutableAttribute()
        {
        }

        public override Task<MemberDeclarationSyntax> GenerateAsync(MemberDeclarationSyntax applyTo, Document document, IProgressAndErrors progress, CancellationToken cancellationToken)
        {
            return CodeGen.GenerateAsync((ClassDeclarationSyntax)applyTo, document, progress, cancellationToken);
        }
    }
}
