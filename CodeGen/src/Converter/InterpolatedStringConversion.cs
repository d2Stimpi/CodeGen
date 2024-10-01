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
        // std::string variaton of statements created from expressions found in onterpolated string expression
        private List<CppSyntaxNode> _statements = new List<CppSyntaxNode>();
        private CppSyntaxNode _op;
        private string _ossVar;

        public List<CppSyntaxNode> Statements { get => _statements; }
        public string VarName { get => _ossVar; }

        public InterpolatedStringConversion(CSharpSyntaxNode node)
        {
            // First statement will be oss declaration
            _ossVar = "oss"; 
            CppLocalDeclarationStatementSyntax ossDecl = new CppLocalDeclarationStatementSyntax();
            ossDecl.AddNode(new CppVariableDeclarationSyntax()
            {
                NewMembers =
                {
                    new CppIdentifierSyntax() { Identifier = "std::ostringstream" },
                    new CppVariableDeclaratorSyntax()
                    {
                        Identifier = _ossVar
                    }
                }
            });
            _statements.Add(ossDecl);

            // Expression statement to add to ( << [expression] )
            CppExpressionStatementSyntax constructStrExprssion = new CppExpressionStatementSyntax();
            constructStrExprssion.AddNode(new CppOperatorExpressionSyntax()
            {
                NewMembers =
                {
                    new CppIdentifierSyntax() { Identifier = _ossVar }
                }
            });
            _statements.Add(constructStrExprssion);
            _op = constructStrExprssion.FirstMember;

            Visit(node);
        }

        public override void VisitInterpolation(InterpolationSyntax node)
        {
            StackAddNode(new CppUnhandledSyntax(node.Kind()));
            base.VisitInterpolation(node);
        }

        public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            Console.WriteLine("InterpolatedStringText:" + node.ToString());

            _op.AddNode(new CppInsertionOperatorSyntax()
            {
                NewMember = new CppOperatorExpressionSyntax()
                {
                    NewMembers =
                    {
                        new CppStringLiteralSyntax() { Token = "\"" + node.ToString() + "\"" }
                    }
                }
            });
            _op = _op.GetFirstMember<CppInsertionOperatorSyntax>().FirstMember;


            base.VisitInterpolatedStringText(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            CppInvocationExpressionSyntax invocation = StackAddNode(new CppInvocationExpressionSyntax()) as CppInvocationExpressionSyntax;
            if (!node.Parent.IsKind(SyntaxKind.Argument))
            {
                base.VisitInvocationExpression(node);

                _op.AddNode(new CppInsertionOperatorSyntax()
                {
                    NewMember = new CppOperatorExpressionSyntax()
                    {
                        NewMember = invocation
                    }
                });
                _op = _op.GetFirstMember<CppInsertionOperatorSyntax>().FirstMember;
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
