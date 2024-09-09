using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppExpressionStatementSyntax : CppSyntaxNode
    {
        public CppExpressionStatementSyntax() : base(CppSyntaxKind.ExpressionStatement)
        {
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return "";
        }
    }
}
