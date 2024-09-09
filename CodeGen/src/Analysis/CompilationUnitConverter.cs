using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using CodeGen.CppSyntax;

namespace CodeGen
{
    internal class CompilationUnitConverter
    {
        private CompilationUnitSyntax _activeAST;
        private CppRootSyntaxNode _cppSyntaxTree;

        public CppRootSyntaxNode CppSyntaxTree => _cppSyntaxTree;

        public void LoadAST(CompilationUnitSyntax ast)
        {
            _activeAST = ast;

            ProcessCompilationUnit();
        }

        private void ProcessCompilationUnit()
        {
            LoggingSyntaxWalker logger = new LoggingSyntaxWalker();
            logger.Visit(_activeAST);

            ConverterSyntaxWalker converterWalker = new ConverterSyntaxWalker();
            converterWalker.Visit(_activeAST);

            Console.WriteLine("\n\n==== Following is AST converted to Cpp Syntax Nodes ====");

            CppASTWalker astWalker = new CppASTWalker();
            astWalker.Visit(converterWalker.SyntaxTree);

            _cppSyntaxTree = converterWalker.SyntaxTree as CppRootSyntaxNode;
        }



        /**
         *  Conversion methods
         */


    }
}
