using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class CppThisExpressionSyntax : CppSyntaxNode
    {
        public CppThisExpressionSyntax() : base(CppSyntaxKind.SimpleAssignmentExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            formated.Write("this");

            return formated.ToString();
        }
    }
}
