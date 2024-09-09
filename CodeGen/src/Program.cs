using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeGen.CppSyntax;

namespace CodeGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rootDir = "D:\\compilers\\wasm_test\\roslyn\\CodeGen";

            Parser parser = new Parser();
            CppRootSyntaxNode cppTree = parser.ParseFile(Path.Combine(rootDir, "ParseThisClass.cs"));

            CodeBuilder builder = new CodeBuilder(cppTree);
            builder.Emit(Path.Combine(rootDir, "CppClass.h"), FileType.Header);
            builder.Emit(Path.Combine(rootDir, "CppClass.cpp"), FileType.Source);

            Console.ReadLine();
        }
    }
}
