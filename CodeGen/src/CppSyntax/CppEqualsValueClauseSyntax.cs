using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppEqualsValueClauseSyntax : CppSyntaxNode
    {
        public CppEqualsValueClauseSyntax() : base(CppSyntaxKind.EqualsValueClause)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
