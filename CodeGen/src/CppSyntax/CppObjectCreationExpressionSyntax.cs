using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppObjectCreationExpressionSyntax : CppSyntaxNode
    {
        public CppObjectCreationExpressionSyntax() : base(CppSyntaxKind.ObjectCreationExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            CodeFormatString formated = new CodeFormatString(depth);

            return formated.ToString();
        }
    }
}
