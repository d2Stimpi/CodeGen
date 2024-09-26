using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGen.CppSyntax
{
    internal sealed class CppUnhandledSyntax : CppSyntaxNode
    {
        private string _csharpKindStr;

        public string CSharpKind { get => _csharpKindStr; }

        public CppUnhandledSyntax(SyntaxKind kind) : base(CppSyntaxKind.Unhandled)
        {
            _csharpKindStr = kind.ToString();
        }

        public override string GetHeaderText(int depth)
        {
            return "unhandled";
        }

        public override string GetSourceText(int depth)
        {
            return "unhandled";
        }
    }
}
