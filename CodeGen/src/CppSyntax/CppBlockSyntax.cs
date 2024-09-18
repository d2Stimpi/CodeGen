﻿using CodeGen.CppSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal sealed class CppBlockSyntax : CppSyntaxNode
    {
        public CppBlockSyntax() : base(CppSyntaxKind.Block)
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

            formated.SetTabs(depth + 1);
            formated.WriteLine("{");
            foreach (var expr in Members)
            {
                formated.WriteLine(expr.GetSourceText(depth + 1));
            }
            formated.SetTabs(depth);
            formated.WriteLine("}");
            
            return formated.ToString();
        }
    }
}
