using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    class CppPredefineType : CppSyntaxNode
    {
        private string _typeName;

        public string TypeName { get => _typeName; set => _typeName = value;
        }
        public CppPredefineType() : base(CppSyntaxKind.PredefinedType)
        {

        }

        public override string GetHeaderText(int depth)
        {
            return TypeName;
        }

        public override string GetSourceText(int depth)
        {
            return TypeName;
        }
    }
}
