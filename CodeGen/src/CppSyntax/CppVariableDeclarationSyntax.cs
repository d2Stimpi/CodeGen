using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppVariableDeclarationSyntax : CppSyntaxNode
    {
        public CppVariableDeclarationSyntax() : base(CppSyntaxKind.VariableDeclaration)
        {

        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string declarationText = "";

            foreach (var member in Members)
            {
                if (declarationText.Length == 0)
                    declarationText += member.GetHeaderText(0);
                else
                    declarationText += " " + member.GetHeaderText(0);
            }

            formated.Write($"{declarationText};");

            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
