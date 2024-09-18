using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppParameterListSyntax : CppSyntaxNode
    {
        public CppParameterListSyntax() : base(CppSyntaxKind.ParameterList)
        {
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string argsText = "";

            foreach (var param in Members)
            {
                if (argsText.Length == 0)
                    argsText += param.GetHeaderText(depth);
                else
                    argsText += ", " + param.GetHeaderText(depth);
            }

            formated.Write(argsText);
            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string argsText = "";

            foreach (var param in Members)
            {
                if (argsText.Length == 0)
                    argsText += param.GetSourceText(depth);
                else
                    argsText += ", " + param.GetSourceText(depth);
            }

            formated.Write(argsText);
            return formated.ToString();
        }
    }
}
