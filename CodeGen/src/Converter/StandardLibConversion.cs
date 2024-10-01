using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeGen.CppSyntax
{
    internal abstract class MethodConversionBase
    {
        public abstract CppSyntaxNode Convert(CppSyntaxNode syntaxNode);
    }

    internal sealed class StandardLibConversion
    {
        private static readonly Dictionary<string, MethodConversionBase> _literalConversionDict = new Dictionary<string, MethodConversionBase>()
        {
            { "Console", new ConsoleConversion() }
        };

        public static Dictionary<string, MethodConversionBase> ConversionDict { get => _literalConversionDict; }

        private static bool IsConvertable(string identifier)
        {
            return ConversionDict.ContainsKey(identifier);
        }

        private static CppSyntaxNode ConvertInvocationExpression(string identifier, CppInvocationExpressionSyntax invocationExpression)
        {
            var conversion = ConversionDict[identifier];
            return conversion.Convert(invocationExpression);
        }

        /* Public methods */

        public static CppSyntaxNode InvocationExpressionTryConvert(CppInvocationExpressionSyntax invocationExpression)
        {
            CppIdentifierSyntax classIdentifierSyntax = new CppIdentifierSyntax() { Identifier = "unknown" };
            CppIdentifierSyntax memberIdentifierSyntax = new CppIdentifierSyntax() { Identifier = "unknown" };

            if (invocationExpression.FirstMember is CppSimpleMemberAccessExpressionSyntax)
            {
                classIdentifierSyntax = invocationExpression.FirstMember.FirstMember as CppIdentifierSyntax;
                memberIdentifierSyntax = invocationExpression.FirstMember.NewMember as CppIdentifierSyntax;
            }
            else
            {
                Console.WriteLine("[INFO] InvocationExpressionTryConvert: skipped subtype " + invocationExpression.FirstMember.Kind);
            }

            if (IsConvertable(classIdentifierSyntax.Identifier))
            {
                return ConvertInvocationExpression(classIdentifierSyntax.Identifier, invocationExpression);
            }

            return null;
        }
    }
}
