using CodeGen.Analysis;
using CodeGen.src.CppSyntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    // Convert to:
    //  LocalDeclarationStatement
    //      - declare std::string variable
    //      VariableDeclaration (could be a generic - template type?)
    //          Identifier
    //          VariableDeclarator (describes how the var is declared)
    //              EqualsValueClause   (=)
    //  ExpressionStatement
    //      - append all nested expressions (interpolated string)

    internal sealed class InterpolatedStringConversion : StackSyntaxWalker
    {
        private List<string> _stringLiterals = new List<string>();
        private List<CppInvocationExpressionSyntax> _invocations = new List<CppInvocationExpressionSyntax>();
        // Final result of statements
        private List<CppSyntaxNode> _statements = new List<CppSyntaxNode>();

        public List<string> StringLiterals { get => _stringLiterals; }
        public List<CppInvocationExpressionSyntax> InvocationExpressions { get => _invocations; }
        public List<CppSyntaxNode> Statements { get => _statements; }
        

        public InterpolatedStringConversion(CSharpSyntaxNode node)
        {
            Visit(node);

            // Create variable declaration
            string declVarName = "genUniqueVarName";
            CppLocalDeclarationStatementSyntax declStatement = new CppLocalDeclarationStatementSyntax();
            declStatement.AddNode(new CppVariableDeclarationSyntax() {
                NewMembers =
                {
                    new CppIdentifierSyntax() { Identifier = "std::string" },
                    new CppVariableDeclaratorSyntax()
                    {
                        Identifier = declVarName,
                        NewMember = new CppEqualsValueClauseSyntax()
                        {
                            NewMember = new CppStringLiteralSyntax()
                            {
                                Token = "\"" + StringLiterals.First() + "\""
                            }
                        }
                    }
                }
            });
            _statements.Add(declStatement);

            // Append statements
            foreach (var invocExpr in _invocations)
            {
                CppExpressionStatementSyntax appendStatement = new CppExpressionStatementSyntax();
                appendStatement.AddNode(new CppInvocationExpressionSyntax()
                {
                    NewMembers =
                    {
                        new CppSimpleMemberAccessExpressionSyntax()
                        {
                            NewMembers =
                            {
                                new CppIdentifierSyntax() { Identifier = declVarName },
                                new CppIdentifierSyntax() { Identifier = "append" }
                            }
                        },
                        new CppArgumentList()
                        {
                            NewMember = new CppArgumentSyntax() { NewMember = invocExpr }
                        }
                    }
                });
                _statements.Add(appendStatement);
            }
        }

        public void AddStringLiteral(string str)
        {
            _stringLiterals.Add(str);
        }

        public override void VisitInterpolation(InterpolationSyntax node)
        {
            StackAddNode(new CppUnhandledSyntax(node.Kind()));
            base.VisitInterpolation(node);
        }

        public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            Console.WriteLine("InterpolatedStringText:" + node.ToString());
            AddStringLiteral(node.ToString());

            base.VisitInterpolatedStringText(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            CppInvocationExpressionSyntax invocation = StackAddNode(new CppInvocationExpressionSyntax()) as CppInvocationExpressionSyntax;
            if (!node.Parent.IsKind(SyntaxKind.Argument))
            {
                _invocations.Add(invocation);
                base.VisitInvocationExpression(node);
            }
            else
            {
                base.VisitInvocationExpression(node);
            }
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.Parent.IsKind(SyntaxKind.InvocationExpression))
            {
                StackAddNode(new CppIdentifierSyntax() { Identifier = node.Identifier.ToString() });
            }

            base.VisitIdentifierName(node);
        }

        public override void VisitArgumentList(ArgumentListSyntax node)
        {
            StackAddNode(new CppArgumentList());

            base.VisitArgumentList(node);
        }

        public override void VisitArgument(ArgumentSyntax node)
        {
            StackAddNode(new CppArgumentSyntax());

            base.VisitArgument(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.NumericLiteralExpression))
            {
                StackAddNode(new CppNumericLiteralSyntax() { NumericLiteral = node.Token.ToString() });
            }

            base.VisitLiteralExpression(node);
        }
    }
}
