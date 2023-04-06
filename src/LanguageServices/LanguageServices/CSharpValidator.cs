﻿using System.Collections.Generic;
using System.Linq;
using Compilify.Extensions;
using Microsoft.CodeAnalysis;
//using Roslyn.Compilers;

namespace Compilify.LanguageServices
{
    public class CSharpValidator : ICodeValidator
    {
        private readonly CSharpCompiler compiler;

        public CSharpValidator()
        {
            compiler = new CSharpCompiler();
        }

        public IEnumerable<EditorError> GetCompilationErrors(ICodeProject post)
        {
            var result = compiler.RoslynCompile(post).EmitToMemory();
            return result.Diagnostics
                         .Where(x => x.Info.Severity == DiagnosticSeverity.Error)
                         .Select(x => new EditorError
                         {
                             Location = DocumentLineSpan.Create(x.Location.GetLineSpan(true)),
                             Message = x.Info.GetMessage()
                         });
        }
    }
}