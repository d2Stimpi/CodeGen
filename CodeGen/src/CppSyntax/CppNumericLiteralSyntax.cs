﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppNumericLiteralSyntax : CppSyntaxNode
    {
        private string _literal = "";

        public string NumericLiteral { get => _literal; set => _literal = value; }

        public CppNumericLiteralSyntax() : base(CppSyntaxKind.NumericLiteral)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return NumericLiteral;
        }

        public override string GetSourceText(int depth)
        {
            return NumericLiteral;
        }
    }
}
