using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppLocalDeclarationStatementSyntax : CppSyntaxNode
    {
        public CppLocalDeclarationStatementSyntax() : base(CppSyntaxKind.LocalDeclarationStatement)
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

            if (FirstMember != null)
            {
                formated.Write(FirstMember.GetSourceText(0));
            }
            else
            {
                formated.Write("Empty::LocalDeclarationExpression");
            }

            return formated.ToString();
        }
    }
}
