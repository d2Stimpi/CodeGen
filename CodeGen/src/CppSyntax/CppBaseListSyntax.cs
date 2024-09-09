using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppBaseListSyntax : CppSyntaxNode
    {
        public CppBaseListSyntax() : base(CppSyntaxKind.BaseList)
        {
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            string baseListText = "";

            foreach (var member in Members)
            {
                if (baseListText.Length == 0)
                    baseListText += "public " + member.GetHeaderText(0);
                else
                    baseListText += ", " + member.GetHeaderText(0);
            }
            formated.Write(baseListText);

            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return "";
        }
    }
}
