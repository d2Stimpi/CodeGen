using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    /*
     *  Should contain list of Statement type nodes (TODO: check/verify)
     *  - TODO: only after non logical statement string generation append ";"
     */
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

            formated.SetTabs(depth + 1);
            formated.WriteLine("{");
            foreach (var statement in Members)
            {
                string lineEnding = "";
                if (!statement.IsKind(CppSyntaxKind.Block))
                    lineEnding = ";";

                formated.WriteLine(statement.GetSourceText(depth + 1) + lineEnding);
            }
            formated.SetTabs(depth);
            formated.WriteLine("}");
            
            return formated.ToString();
        }
    }
}
