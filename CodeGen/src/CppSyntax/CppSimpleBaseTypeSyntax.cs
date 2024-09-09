using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppSimpleBaseTypeSyntax : CppSyntaxNode
    {
        public CppSimpleBaseTypeSyntax() : base(CppSyntaxKind.SimpleBaseType)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return FirstMember.GetHeaderText(0);
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
