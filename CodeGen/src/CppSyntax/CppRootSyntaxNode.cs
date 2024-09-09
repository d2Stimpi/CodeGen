using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppRootSyntaxNode : CppSyntaxNode
    {
        public CppRootSyntaxNode() : base(CppSyntaxKind.Root)
        {

        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            foreach (var member in Members)
            {
                formated.WriteLine(member.GetHeaderText(depth));
            }

            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
