using CodeGen.Analysis;
using CodeGen.CppSyntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Converter
{
    internal sealed class PropertyConversion : StackSyntaxWalker
    {
        private CppClassSyntax _ownerClass;
        private CppMethodDeclarationSyntax _getMethod;
        private CppMethodDeclarationSyntax _setMethod;
        private bool _hasGetMethod = false;
        private bool _hasSetMethod = false;

        public CppMethodDeclarationSyntax GetMethod { get => _getMethod; }
        public CppMethodDeclarationSyntax SetMethod { get => _setMethod; }
        public bool HasGetter { get => _hasGetMethod; }
        public bool HasSetter { get => _hasSetMethod; }

        public PropertyConversion(CSharpSyntaxNode node, CppClassSyntax owner)
        {
            _ownerClass = owner;

            _getMethod = new CppMethodDeclarationSyntax();
            _getMethod.Modifiers.Add("public");
            _setMethod = new CppMethodDeclarationSyntax();
            _setMethod.Modifiers.Add("public");

            Visit(node);
        }

        private void CreatePropertyTypeMethods(string typeName)
        {
            _getMethod.ReturnType = typeName;
            CppBlockSyntax getBlockSyntax = new CppBlockSyntax();
            _getMethod.AddNode(getBlockSyntax);

            CppParameterListSyntax paramListSyntax = new CppParameterListSyntax();

            CppParameterSyntax paramSyntax = new CppParameterSyntax() { Identifier = "value" };
            paramListSyntax.AddNode(paramSyntax);

            CppPredefineType predefType = new CppPredefineType() { TypeName = typeName };
            paramSyntax.AddNode(predefType);

            CppBlockSyntax setBlockSyntax = new CppBlockSyntax();

            _setMethod.AddNode(paramListSyntax);
            _setMethod.AddNode(setBlockSyntax);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            _getMethod.Identifier = "Get" + node.Identifier;
            _setMethod.Identifier = "Set" + node.Identifier;

            _getMethod.Parent = _ownerClass;
            _setMethod.Parent = _ownerClass;

            base.VisitPropertyDeclaration(node);
        }

        public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            if (node.Parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                CreatePropertyTypeMethods(node.Keyword.ToString());
            }

            base.VisitPredefinedType(node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.Parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                CreatePropertyTypeMethods(node.Identifier.ToString());
            }
            else
            {
                StackAddNode(new CppIdentifierSyntax() { Identifier = node.Identifier.ToString() });
            }

            base.VisitIdentifierName(node);
        }

        public override void VisitAccessorList(AccessorListSyntax node)
        {
            // act as root for tracking
            StackAddNode(new CppUnhandledSyntax(node.Kind()));

            base.VisitAccessorList(node);
        }

        // Get / Set accessor
        public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            if (node.IsKind(SyntaxKind.GetAccessorDeclaration))
                _hasGetMethod = true;
            if (node.IsKind(SyntaxKind.SetAccessorDeclaration))
                _hasSetMethod = true;

            base.VisitAccessorDeclaration(node);
        }

        public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            CppExpressionStatementSyntax expressionStatement = StackAddNode(new CppExpressionStatementSyntax()) as CppExpressionStatementSyntax;

            if (node.Parent.IsKind(SyntaxKind.GetAccessorDeclaration))
            {
                CppReturnStatementSyntax returnStatement = StackAddNode(new CppReturnStatementSyntax()) as CppReturnStatementSyntax;
                expressionStatement.AddNode(returnStatement);
                _getMethod.GetFirstMember<CppBlockSyntax>().AddNode(expressionStatement);
            }
            if (node.Parent.IsKind(SyntaxKind.SetAccessorDeclaration))
            {
                _setMethod.GetFirstMember<CppBlockSyntax>().AddNode(expressionStatement);
            }

            base.VisitArrowExpressionClause(node);
        }

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            CppBinaryExpressionSyntax binaryExpressionSyntax = StackAddNode(new CppBinaryExpressionSyntax()) as CppBinaryExpressionSyntax;
            binaryExpressionSyntax.OperationKind = CppBinaryExpressionSyntax.BinaryExpressionKindFromSyntaxKind(node.Kind());

            base.VisitBinaryExpression(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            // TODO: other types?
            StackAddNode(new CppStringLiteralSyntax() { Token = node.Token.ToString() });

            base.VisitLiteralExpression(node);
        }

        public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            // TODO: other assignment types +=, -=, etc...
            StackAddNode(new CppSimpleAssignmentExpressionSyntax());

            base.VisitAssignmentExpression(node);
        }
    }
}
