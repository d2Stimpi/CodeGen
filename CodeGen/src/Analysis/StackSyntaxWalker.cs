using CodeGen.CppSyntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Analysis
{
    /*
     * Using Stack<CppSyntaxNode> to track which AST branch is processed
     */

    internal class StackSyntaxWalker : CSharpSyntaxWalker
    {
        private Stack<CppSyntaxNode> _nodeStack = new Stack<CppSyntaxNode>();

        public CppSyntaxNode StackAddNode(CppSyntaxNode node)
        {
            if (_nodeStack.Count != 0)
                node.Parent = _nodeStack.Peek();
            _nodeStack.Push(node);

            return node;
        }

        public override void Visit(SyntaxNode node)
        {
            base.Visit(node);

            if (_nodeStack.Count > 1)
            {
                var leafNode = _nodeStack.Pop();
                _nodeStack.Peek().AddNode(leafNode);
            }
        }

    }
}
