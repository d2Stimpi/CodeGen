using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.src.CppSyntax
{
    internal class CppArgumentSyntax : CppSyntaxNode
    {
        public CppArgumentSyntax() : base(CppSyntaxKind.Argument)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return formated.ToString();
        }
    }
}
