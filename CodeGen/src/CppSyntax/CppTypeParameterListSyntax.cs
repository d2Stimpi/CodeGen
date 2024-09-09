using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppTypeParameterListSyntax : CppSyntaxNode
    {
        public CppTypeParameterListSyntax() : base(CppSyntaxKind.TypeParameterList)
        {
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string argsText = "";

            foreach (var arg in Members)
            {
                if (argsText.Length == 0)
                    argsText += "typename " + arg.GetHeaderText(0);
                else
                    argsText += ", typename " + arg.GetHeaderText(0);
            }
            formated.Write($"template <{argsText}> ");

            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return "";
        }
    }
}
