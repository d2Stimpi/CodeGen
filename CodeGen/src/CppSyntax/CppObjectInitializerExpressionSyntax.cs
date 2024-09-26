using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class CppObjectInitializerExpressionSyntax : CppSyntaxNode
    {
        public CppObjectInitializerExpressionSyntax() : base(CppSyntaxKind.ObjectInitializerExpression)
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
