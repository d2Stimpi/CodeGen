using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppParameterSyntax : CppSyntaxNode
    {
        private string _name;

        public string Identifier { get => _name; set => _name = value; }

        public CppParameterSyntax() : base(CppSyntaxKind.Parameter)
        {

        }

        public override string GetHeaderText(int depth)
        {
            string txt = "";

            if (Members.OfType<CppPredefineType>().Any())
                txt += FirstMember.GetHeaderText(depth) + " ";
            else if (Members.OfType<CppIdentifierSyntax>().Any())
                txt += FirstMember.GetHeaderText(depth) + " ";

            txt += Identifier;

            return txt;
        }

        public override string GetSourceText(int depth)
        {
            return "";
        }
    }
}
