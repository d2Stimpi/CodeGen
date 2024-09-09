using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class CppTypeParameterSyntax : CppSyntaxNode
    {
        private string _name;

        public string Identifier { get => _name; set => _name = value; }

        public CppTypeParameterSyntax() : base(CppSyntaxKind.TypeParameter)
        {

        }

        public override string GetHeaderText(int depth)
        {
            string txt = "";

            if (Members.OfType<CppPredefineType>().Any())
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
