using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppInvocationExpressionSyntax : CppSyntaxNode
    {
        public CppArgumentList Arguments { get => GetFirstMember<CppArgumentList>(); }

        public CppInvocationExpressionSyntax() : base(CppSyntaxKind.InvocationExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            string exprTxt = FirstMember.GetSourceText(0);
            string argsTxt = "";

            if (Arguments != null)
                argsTxt = Arguments.GetSourceText(0);

            // {expression}({argument list})
            formated.Write($"{exprTxt}({argsTxt});");

            return formated.ToString();
        }
    }
}
