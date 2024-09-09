using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    internal class CppFileWriter
    {
        private FileStream _fileStream;

        public CppFileWriter(string filePath)
        {
            _fileStream = File.Create(filePath);
        }

        ~CppFileWriter()
        {
            _fileStream.Close();
        }

        public void Write(string text)
        {
            byte[] data = new UTF8Encoding(true).GetBytes(text);
            _fileStream.Write(data, 0, data.Length);
        }

        public void InsertWrite(string text, long position = -1)
        {
            long tempPos = Position;
            if (position != -1)
            {
                Position = position;
                Write(text);
            }
            Position = tempPos;
        }

        long Position
        {
            get { return _fileStream.Position; }
            set { _fileStream.Position = value; }
        }
    }

}
