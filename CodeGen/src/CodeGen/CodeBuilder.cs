using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeGen.CppSyntax;

namespace CodeGen
{
    public enum FileType { Header, Source };

    internal class CodeBuilder
    {
        private CppFileWriter _sourceFileWriter;
        private CppFileWriter _headerFileWriter;
        private CppRootSyntaxNode _tree;

        public CodeBuilder(CppRootSyntaxNode tree)
        {
            _tree = tree;
        }

        public void Emit(string filePath, FileType type)
        {
            if (type == FileType.Header)
                BuildHeaderFile(filePath);

            if (type == FileType.Source)
                BuildSourceFile(filePath);

        }

        private void BuildHeaderFile(string filePath)
        {
            _headerFileWriter = new CppFileWriter(filePath);
            // Walk the tree and build code text and output to file
            foreach (var cppNode in _tree.Members)
            {
                _headerFileWriter.Write(cppNode.GetHeaderText(0));
            }
        }

        private void BuildSourceFile(string filePath)
        {
            _sourceFileWriter = new CppFileWriter(filePath);
            // Walk the tree and build code text and output to file
            foreach (var cppNode in _tree.Members)
            {
                _sourceFileWriter.Write(cppNode.GetSourceText(0));
            }
        }
    }
}
