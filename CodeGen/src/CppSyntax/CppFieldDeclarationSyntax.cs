using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppFieldDeclarationSyntax : CppSyntaxNode
    {
        private List<string> _modifiers = new List<string>();

        public List<string> Modifiers { get => _modifiers; set => _modifiers = value; }

        public Boolean IsStatic { get => _modifiers.Contains("static"); }
        public Boolean IsPublic { get => _modifiers.Contains("public"); }
        public Boolean IsProtected { get => _modifiers.Contains("protected"); }
        public Boolean IsPrivate { get => !IsPublic && !IsProtected; }

        public CppFieldDeclarationSyntax() : base(CppSyntaxKind.FieldDeclaration)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return FirstMember.GetHeaderText(depth);
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
