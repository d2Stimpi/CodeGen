using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class CppSimpleAssignmentExpressionSyntax : CppSyntaxNode
    {
        public CppSimpleAssignmentExpressionSyntax() : base(CppSyntaxKind.SimpleAssignmentExpression)
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

            var first = GetFirstMember();
            if (first!= null && first.IsKind(CppSyntaxKind.IdentifierName))
            {
                CppIdentifierSyntax lhsIdentifier = FirstMember as CppIdentifierSyntax;
                formated.Write(lhsIdentifier.GetSourceText(0) + " = ");
                var rhsNode = GetNextMember();
                if (rhsNode != null)
                {
                    formated.Write(rhsNode.GetSourceText(0));
                }
            }
            else
            {
                // TODO: emit error or warning
                Console.WriteLine("SimpleAssignment expression, left side identifier expected but not found!");
                formated.Write("Error in SimpleAssignmentExpression");
            }

            return formated.ToString();
        }
    }
}
