using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO:
// - handle writing of include statements
// - compilation unit should represent a file pair h/cpp

namespace CodeGen.CppSyntax
{
    class CppCompilationUnitNode : CppSyntaxNode
    {

        public CppCompilationUnitNode() : base(CppSyntaxKind.CompilationUnit)
        {
        }

        public override string GetHeaderText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            foreach (var member in Members)
            {
                if (member is CppUnhandledSyntax)
                    continue;
              
                formated.WriteLine(member.GetHeaderText(depth));
            }

            return formated.ToString();
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            foreach (var member in Members)
            {
                if (member is CppUnhandledSyntax)
                    continue;

                formated.WriteLine(member.GetSourceText(depth));
            }

            return formated.ToString();
        }
    }
}
