using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using CodeGen.CppSyntax;

namespace CodeGen
{
    internal sealed class ConverterSyntaxWalker : CSharpSyntaxWalker
    {
        private CppSyntaxNode _rootNode;
        private Stack<CppSyntaxNode> _nodeStack = new Stack<CppSyntaxNode>();
        private CppClassSyntax _classNode;

        private List<SyntaxKind> _skipSyntaxKinds = new List<SyntaxKind>()
        {
            SyntaxKind.UsingDirective,
            SyntaxKind.PropertyDeclaration,
        };


        public CppSyntaxNode SyntaxTree { get => _rootNode; }

        public ConverterSyntaxWalker()
        {
            _rootNode = new CppRootSyntaxNode();
            _nodeStack.Push(_rootNode);
        }

        private CppSyntaxNode StackReplace(CppSyntaxNode node)
        {
            _nodeStack.Pop();
            _nodeStack.Push(node);

            return node;
        }

        public override void Visit(SyntaxNode node)
        {
            // Check if we want to skip a Kind
            var kind = _skipSyntaxKinds.Where(x => node.IsKind(x)).FirstOrDefault();
            if (kind == SyntaxKind.None)
            {

                _nodeStack.Push(new CppUnhandledSyntax(node.Kind()));

                base.Visit(node);

                var leafNode = _nodeStack.Pop();
                _nodeStack.Peek().AddNode(leafNode);
            }
            else
            {
                // Visit skipped nodes, since some of them are converted
                base.Visit(node);
            }
        }

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            StackReplace(new CppCompilationUnitNode());

            base.VisitCompilationUnit(node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            CppIdentifierSyntax identifierSyntax = StackReplace(new CppIdentifierSyntax()) as CppIdentifierSyntax;
            identifierSyntax.Identifier = node.Identifier.ToString();

            base.VisitIdentifierName(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            CppNamespaceSyntax namespaceSytnax = StackReplace(new CppNamespaceSyntax()) as CppNamespaceSyntax;
            namespaceSytnax.Identifier = node.Name.ToString();

            base.VisitNamespaceDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            _classNode = StackReplace(new CppClassSyntax()) as CppClassSyntax;
            _classNode.Identifier = node.Identifier.ToString();

            base.VisitClassDeclaration(node);
        }

        public override void VisitBaseList(BaseListSyntax node)
        {
            StackReplace(new CppBaseListSyntax());

            base.VisitBaseList(node);
        }

        public override void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
        {
            StackReplace(new CppSimpleBaseTypeSyntax());

            base.VisitSimpleBaseType(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            CppFieldDeclarationSyntax fieldSyntax = StackReplace(new CppFieldDeclarationSyntax()) as CppFieldDeclarationSyntax;
            fieldSyntax.Modifiers = node.Modifiers.Select(m => m.ToString()).ToList();

            base.VisitFieldDeclaration(node);
        }

        public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            CppPredefineType typeSyntax = StackReplace(new CppPredefineType()) as CppPredefineType;
            typeSyntax.TypeName = node.Keyword.ToString();

            base.VisitPredefinedType(node);
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            CppVariableDeclaratorSyntax declaratorSyntax = StackReplace(new CppVariableDeclaratorSyntax()) as CppVariableDeclaratorSyntax;
            declaratorSyntax.Identifier = node.Identifier.ToString();

            base.VisitVariableDeclarator(node);
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            StackReplace(new CppVariableDeclarationSyntax());

            base.VisitVariableDeclaration(node);
        }

        public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            StackReplace(new CppEqualsValueClauseSyntax());

            base.VisitEqualsValueClause(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.NumericLiteralExpression:
                    StackReplace(new CppNumericLiteralSyntax());
                    break;
                case SyntaxKind.StringLiteralExpression:
                    StackReplace(new CppStringLiteralSyntax());
                    break;
                default:
                    break;
            }

            base.VisitLiteralExpression(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            CppMethodDeclarationSyntax methodSyntax = StackReplace(new CppMethodDeclarationSyntax()) as CppMethodDeclarationSyntax;
            methodSyntax.Identifier = node.Identifier.ToString();
            methodSyntax.ReturnType = node.ReturnType.ToString();
            methodSyntax.Modifiers = node.Modifiers.Select(m => m.ToString()).ToList();

            base.VisitMethodDeclaration(node);
        }

        public override void VisitParameterList(ParameterListSyntax node)
        {
            StackReplace(new CppParameterListSyntax());

            base.VisitParameterList(node);
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            CppParameterSyntax parameterSyntax = StackReplace(new CppParameterSyntax()) as CppParameterSyntax;
            parameterSyntax.Identifier = node.Identifier.ToString();

            base.VisitParameter(node);
        }

        public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
            StackReplace(new CppTypeParameterListSyntax());

            base.VisitTypeParameterList(node);
        }

        public override void VisitTypeParameter(TypeParameterSyntax node)
        {
            CppTypeParameterSyntax typeParamSytnax = StackReplace(new CppTypeParameterSyntax()) as CppTypeParameterSyntax;
            typeParamSytnax.Identifier = node.Identifier.ToString();

            base.VisitTypeParameter(node);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            StackReplace(new CppExpressionStatementSyntax());

            base.VisitExpressionStatement(node);
        }

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.AddExpression:
                    StackReplace(new CppAddExpressionSyntax());
                    break;
                default:
                    break;
            }

            base.VisitBinaryExpression(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            PropertyConverter propertyConverter = new PropertyConverter();
            propertyConverter.VisitPropertyDeclaration(node);

            _classNode.AddNode(propertyConverter.GetMethod);
            _classNode.AddNode(propertyConverter.SetMethod);
        }
    }
}
