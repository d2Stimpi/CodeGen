using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeGen.CppSyntax;

namespace CodeGen
{
    internal sealed class CppASTWalker
    {
        private int _tabs = -1;

        public void Visit(CppSyntaxNode node)
        {
            string csharpKind = "";

            if (node is CppUnhandledSyntax)
                csharpKind = " [" + (node as CppUnhandledSyntax).CSharpKind + "]";

            _tabs++;
            Console.WriteLine($"{new String('\t', _tabs)} {node.Kind.ToString()}{csharpKind}");
            foreach (var member in node.Members)
            {
                Visit(member);
            }
            _tabs--;
        }
    }
}
