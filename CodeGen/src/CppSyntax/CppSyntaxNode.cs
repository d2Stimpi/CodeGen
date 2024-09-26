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
        private CppSyntaxNode _parentNode;
        private CppSyntaxKind _kind;
        private int _iter = 0;

        public CppSyntaxNode Parent { get => _parentNode; set => _parentNode = value; }
        public List<CppSyntaxNode> Members { get => _leafNodes; }
        public CppSyntaxNode FirstMember { get => _leafNodes.First(); }
        public CppSyntaxKind Kind { get => _kind; }
        public bool HasMembers { get => _leafNodes.Count > 0; }

        // Convenience
        public CppSyntaxNode NewMember { get => _leafNodes.Last(); set => _leafNodes.Add(value); }
        public List<CppSyntaxNode> NewMembers { get => _leafNodes; set => value.ForEach(v => _leafNodes.Add(v)); }

        public CppSyntaxNode GetNextMember()
        {
            if (Members.Count > _iter)
                return Members[_iter++];

            return null;
        }

        public CppSyntaxNode GetFirstMember()
        {
            _iter = 0;
            return GetNextMember();
        }


        public CppSyntaxNode(CppSyntaxKind kind)
        {
            _kind = kind;
        }

        public bool IsKind(CppSyntaxKind kind)
        {
            return Kind == kind;
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
