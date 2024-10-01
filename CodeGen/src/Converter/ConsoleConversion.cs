using CodeGen.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class ConsoleConversion : MethodConversionBase
    {
        public override CppSyntaxNode Convert(CppSyntaxNode syntaxNode)
        {
            CppIdentifierSyntax className = syntaxNode.FirstMember.GetFirstMember() as CppIdentifierSyntax;
            CppIdentifierSyntax methodName = syntaxNode.FirstMember.GetNextMember() as CppIdentifierSyntax;

            string writeLineStr = methodName.Identifier == "WriteLine" ? " << std::endl" : "";

            CppArgumentList args = syntaxNode.GetFirstMember<CppArgumentList>();
            CppSyntaxNode argNode = args.FirstMember.FirstMember;
            string strLiteral = "unhandled";

            if (argNode.IsKind(CppSyntaxKind.StringLiteral))
                strLiteral = (argNode as CppStringLiteralSyntax).Token;
            else if (argNode.IsKind(CppSyntaxKind.IdentifierName))
                strLiteral = (argNode as CppIdentifierSyntax).Identifier;

            // Create new node that represends std::cout << strliteral
            CppOperatorExpressionSyntax expression = new CppOperatorExpressionSyntax()
            {
                NewMembers =
                {
                    new CppIdentifierSyntax() { Identifier = "std::cout" },
                    new CppInsertionOperatorSyntax()
                    {
                        NewMember = new CppStringLiteralSyntax()
                        {
                            Token = strLiteral + writeLineStr
                        }
                    }
                }
            };

            return expression;
        }
    }
}
