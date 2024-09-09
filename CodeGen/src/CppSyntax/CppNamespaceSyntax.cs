using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppNamespaceSyntax : CppSyntaxNode
    {
        private string _name;

        public string Identifier { get => _name; set => _name = value; }

        public CppNamespaceSyntax() : base(CppSyntaxKind.NamespaceDeclaration)
        {
            _name = "Unnamed";
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            formated.WriteLine($"namespace {Identifier}");
            formated.WriteLine("{");

            foreach (var member in Members)
            {
                formated.WriteLine(member.GetHeaderText(depth + 1));
            }

            formated.WriteLine("}");
            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return "";
        }
    }
}
