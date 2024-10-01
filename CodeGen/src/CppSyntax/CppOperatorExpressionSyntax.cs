using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppOperatorExpressionSyntax : CppSyntaxNode
    {

        public CppOperatorExpressionSyntax() : base(CppSyntaxKind.OperatorExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            formated.Clear();

            // Identifier
            var lhs = GetFirstMember();
            if (lhs != null)
                formated.Write(lhs.GetSourceText(0));
            // Insertion operator
            var rhs = GetNextMember();
            if (rhs != null)
                formated.Write(rhs.GetSourceText(0));

            return formated.ToString();
        }
    }
}
