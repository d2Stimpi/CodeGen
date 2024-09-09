using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGen
{
    internal sealed class LoggingSyntaxWalker : CSharpSyntaxWalker
    {
        private int _tabs = 0;

        public override void Visit(SyntaxNode node)
        {
            _tabs++;
            Console.WriteLine($"{new String('\t', _tabs)} {node.Kind()}");

            base.Visit(node);
            _tabs--;
        }
    }
}
