using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeGen.CppSyntax
{
    internal abstract class MethodConversionBase
    {
        public abstract string Convert(CppArgumentList argumentList);
    }

    internal sealed class StandardLibConversion
    {
        private static readonly Dictionary<string, MethodConversionBase> _literalConversionDict = new Dictionary<string, MethodConversionBase>()
        {
            { "Console", new ConsoleConversion() }
        };

        public static Dictionary<string, MethodConversionBase> ConversionDict { get => _literalConversionDict; }

        public static bool IsConvertable(string identifier)
        {
            return ConversionDict.ContainsKey(identifier);
        }

        public static string ConvertInvocationExpression(string identifier, CppArgumentList argumentList)
        {
            string convertedStr = "";

            if (ConversionDict[identifier] != null)
            {
                var conversion = ConversionDict[identifier];
                convertedStr = conversion.Convert(argumentList);
            }

            return convertedStr;
        }

    }
}
