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
    internal sealed class PropertyConverter : CSharpSyntaxWalker
    {
        private CppMethodDeclarationSyntax _getMethodSyntax;
        private CppMethodDeclarationSyntax _setMethodSyntax;

        public CppMethodDeclarationSyntax GetMethod { get => _getMethodSyntax; }
        public CppMethodDeclarationSyntax SetMethod { get => _setMethodSyntax; }

        public PropertyConverter()
        {
            _getMethodSyntax = new CppMethodDeclarationSyntax();
            _getMethodSyntax.Modifiers.Add("public");
            _setMethodSyntax = new CppMethodDeclarationSyntax();
            _setMethodSyntax.Modifiers.Add("public");
        }

        public override void Visit(SyntaxNode node)
        {
            base.Visit(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            //_fieldSyntax.Modifiers = node.Modifiers.Select(m => m.ToString()).ToList();
            _getMethodSyntax.Identifier = "Get" + node.Identifier;
            _setMethodSyntax.Identifier = "Set" + node.Identifier;

            base.VisitPropertyDeclaration(node);
        }

        public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            if (node.Parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                _getMethodSyntax.ReturnType = node.Keyword.ToString();

                CppParameterListSyntax paramListSyntax = new CppParameterListSyntax();
            
                CppParameterSyntax paramSyntax = new CppParameterSyntax();
                paramSyntax.Identifier = "value";
                paramListSyntax.AddNode(paramSyntax);

                CppPredefineType predefType = new CppPredefineType();
                predefType.TypeName = node.Keyword.ToString();
                paramSyntax.AddNode(predefType);

                _setMethodSyntax.AddNode(paramListSyntax);
            }

            base.VisitPredefinedType(node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.Parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                _getMethodSyntax.ReturnType = node.Identifier.ToString();

                CppParameterListSyntax paramListSyntax = new CppParameterListSyntax();

                CppParameterSyntax paramSyntax = new CppParameterSyntax();
                paramSyntax.Identifier = "value";
                paramListSyntax.AddNode(paramSyntax);

                CppPredefineType predefType = new CppPredefineType();
                predefType.TypeName = node.Identifier.ToString();
                paramSyntax.AddNode(predefType);

                _setMethodSyntax.AddNode(paramListSyntax);
            }

            base.VisitIdentifierName(node);
        }

        // Get / Set accessor
        public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            base.VisitAccessorDeclaration(node);
        }

        public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            base.VisitArrowExpressionClause(node);
        }
    }
}
