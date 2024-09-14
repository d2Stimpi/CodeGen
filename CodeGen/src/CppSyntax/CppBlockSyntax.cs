using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppBlockSyntax : CppSyntaxNode
    {
        public CppBlockSyntax() : base(CppSyntaxKind.Block)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);
            formated.Clear();

            formated.WriteLine("{");
            formated.WriteLine("}");
            
            return formated.ToString();
        }
    }
}
