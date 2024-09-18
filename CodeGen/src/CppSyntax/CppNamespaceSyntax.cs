using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppNamespaceSyntax : CppSyntaxNode
    {
        public List<CppClassSyntax> Classes { get => Members.OfType<CppClassSyntax>().ToList(); }

        public CppNamespaceSyntax() : base(CppSyntaxKind.NamespaceDeclaration)
        {

        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string identifier = GetFirstMember<CppIdentifierSyntax>().Identifier;

            formated.WriteLine($"namespace {identifier}");
            formated.WriteLine("{");

            foreach (var klass in Classes)
            {
                formated.WriteLine(klass.GetHeaderText(depth + 1));
            }

            formated.WriteLine("}");
            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            foreach (var member in Members)
            {
                if (member is CppIdentifierSyntax)
                {
                    string identifier = (member as CppIdentifierSyntax).Identifier;
                    formated.WriteLine($"namespace {identifier}");
                    formated.WriteLine("{");
                }
                else
                {
                    formated.WriteLine(member.GetSourceText(depth + 1));
                }
            }

            formated.WriteLine("}");
            return formated.ToString();
        }
    }
}
