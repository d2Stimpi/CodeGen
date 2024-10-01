using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppInvocationExpressionSyntax : CppSyntaxNode
    {
        public CppArgumentList ArgumentList { get => GetFirstMember<CppArgumentList>(); }
        
        public CppInvocationExpressionSyntax() : base(CppSyntaxKind.InvocationExpression)
        {

        }

        public string GetExpressionIdentifier()
        {
            if (FirstMember is CppIdentifierSyntax)
            {
                return (FirstMember as CppIdentifierSyntax).Identifier;
            }
            else
            {
                if (FirstMember.HasMembers && FirstMember.FirstMember is CppIdentifierSyntax)
                {
                    return (FirstMember.FirstMember as CppIdentifierSyntax).Identifier;
                }
            }

            return "";
        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            formated.Clear();

            string exprTxt = FirstMember.GetSourceText(0);
            string argsTxt = "";

            if (ArgumentList != null)
                argsTxt = ArgumentList.GetSourceText(0);

            // {expression}({argument list})
            formated.Write($"{exprTxt}({argsTxt})");

            return formated.ToString();
        }
    }
}
