﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppStringLiteralSyntax : CppSyntaxNode
    {
        private string _strLiteral;

        public string Token { get => _strLiteral; set => _strLiteral = value; }

        public CppStringLiteralSyntax() : base(CppSyntaxKind.StringLiteral)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return "";
        }

        public override string GetSourceText(int depth)
        {
            return Token;
        }
    }
}
