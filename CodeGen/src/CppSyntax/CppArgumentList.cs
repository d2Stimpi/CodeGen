using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppArgumentList : CppSyntaxNode
    {
        public CppArgumentList() : base(CppSyntaxKind.ArgumentList)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            int initialLength = formated.ToString().Length;

            foreach (var arg in Members)
            {
                if (formated.ToString().Length == initialLength)
                    formated.Write(arg.GetSourceText(0));
                else
                    formated.Write(", " + arg.GetSourceText(0));
            }

            return formated.ToString();
        }
    }
}
