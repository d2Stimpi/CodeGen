using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.CppSyntax
{
    internal class ConsoleConversion : MethodConversionBase
    {
        public override string Convert(CppArgumentList argumentList)
        {
            string convertedTxt = "std::cout";

            foreach (var arg in argumentList.Members)
            {
                convertedTxt += " << " + arg.GetSourceText(0);
            }
            convertedTxt += " << std::endl";

            return convertedTxt;
        }
    }
}
