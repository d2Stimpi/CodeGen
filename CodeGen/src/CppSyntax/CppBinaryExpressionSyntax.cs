using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    public enum BinaryExpressionKind
    {
        AddExpression,
        SubtractExpression,
        MultiplyExpression,
        DivideExpression,

        UnhandledBinaryExpression
    }

    class CppBinaryExpressionSyntax : CppSyntaxNode
    {
        private BinaryExpressionKind _kind;

        public BinaryExpressionKind OperationKind { get => _kind; set => _kind = value; }

        public CppBinaryExpressionSyntax() : base(CppSyntaxKind.BinaryExpression)
        {
        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            string expressionText = "";

            // Left expression side
            expressionText += GetFirstMember().GetSourceText(0);

            switch (OperationKind)
            {
                case BinaryExpressionKind.AddExpression:
                    expressionText += " + ";
                    break;
                case BinaryExpressionKind.SubtractExpression:
                    expressionText += " - ";
                    break;
                case BinaryExpressionKind.MultiplyExpression:
                    expressionText += " * ";
                    break;
                case BinaryExpressionKind.DivideExpression:
                    expressionText += " / ";
                    break;
                default:
                    break;
            }

            // Right expression side
            expressionText += GetNextMember().GetSourceText(0);

            return expressionText;
        }
    }
}
