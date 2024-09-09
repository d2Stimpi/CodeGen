using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CSharp;

using CodeGen.CppSyntax;

namespace CodeGen
{
    internal class Parser
    {
        public CppRootSyntaxNode ParseFile(String filePath)
        {
            SyntaxTree tree = null;
            CppRootSyntaxNode cppTree = null;

            try
            {
                StreamReader sr = new StreamReader(filePath);
                tree = CSharpSyntaxTree.ParseText(sr.ReadToEnd());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            if (tree != null)
            {
                var root = (CompilationUnitSyntax)tree.GetRoot();
                Console.WriteLine("AST:");

                CompilationUnitConverter converter = new CompilationUnitConverter();
                // Combine to one method
                converter.LoadAST(root);
                cppTree = converter.CppSyntaxTree;
            }

            return cppTree;
        }
    }
}
