using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class CppReturnStatementSyntax : CppSyntaxNode
    {
        public CppReturnStatementSyntax() : base(CppSyntaxKind.ReturnStatement)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            return "return " + FirstMember.GetSourceText(0);
        }
    }
}
