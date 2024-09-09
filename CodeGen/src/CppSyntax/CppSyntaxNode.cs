using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal abstract class CppSyntaxNode
    {
        private List<CppSyntaxNode> _leafNodes = new List<CppSyntaxNode>();
        private CppSyntaxKind _kind;

        public List<CppSyntaxNode> Members { get => _leafNodes; }
        public CppSyntaxNode FirstMember { get => _leafNodes.First(); }
        public CppSyntaxKind Kind { get => _kind; }

        public CppSyntaxNode(CppSyntaxKind kind)
        {
            _kind = kind;
        }

        public void AddNode(CppSyntaxNode node)
        {
            _leafNodes.Add(node);
        }

        public bool HasMember<T>() where T : CppSyntaxNode, new()
        {
            return Members.OfType<T>().Any();
        }

        public T GetFirstMember<T>() where T : CppSyntaxNode, new()
        {
            return Members.OfType<T>().First();
        }

        public abstract string GetHeaderText(int depth);
        public abstract string GetSourceText(int depth);
    }
}
