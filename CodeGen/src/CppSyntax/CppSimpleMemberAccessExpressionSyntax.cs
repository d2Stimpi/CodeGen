using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppSimpleMemberAccessExpressionSyntax : CppSyntaxNode
    {
        private string _className;
        private string _memberName;

        public string ClassIdentifier { get => _className; set => _className = value; }
        public string MemberIdentifier { get => _memberName; set => _memberName = value; }

        public CppSimpleMemberAccessExpressionSyntax() : base(CppSyntaxKind.SimpleMemberAccessExpression)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            string memberAccessTxt = "";

            foreach (var identifier in Members)
            {
                if (memberAccessTxt.Length == 0)
                    memberAccessTxt += identifier.GetSourceText(0);
                else
                    memberAccessTxt += "." + identifier.GetSourceText(0);
            }

            return memberAccessTxt;
        }
    }
}
