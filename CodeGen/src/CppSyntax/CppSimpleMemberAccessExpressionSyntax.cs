using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppSimpleMemberAccessExpressionSyntax : CppSyntaxNode
    {
        public CppSimpleMemberAccessExpressionSyntax() : base(CppSyntaxKind.SimpleMemberAccessExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            string memberAccessTxt = "";

            foreach (var identifier in Members)
            {
                if (memberAccessTxt.Length == 0)
                    memberAccessTxt += identifier.GetSourceText(0);
                else
                    memberAccessTxt += "." + identifier.GetSourceText(0);
            }

            return "";
        }
    }
}
