using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppInsertionOperatorSyntax : CppSyntaxNode
    {
        public CppInsertionOperatorSyntax() : base(CppSyntaxKind.InsertionOperator)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            return " << " + FirstMember.GetSourceText(0);
        }
    }
}
