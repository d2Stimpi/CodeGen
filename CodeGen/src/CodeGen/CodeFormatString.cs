﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    internal sealed class CodeFormatString
    {
        private int _tabCount;
        private string _tabs;
        private string _formatedText;

        public int TabCount { get => _tabCount; }

        public override string ToString()
        {
            return _formatedText;
        }

        public CodeFormatString(int tabCount)
        {
            _tabCount = tabCount;
            _tabs = String.Concat(Enumerable.Repeat("\t", _tabCount));
            _formatedText = _tabs;
        }

        public void Clear()
        {
            _formatedText = "";
        }

        public void AddTabs(int tabCount)
        {
            SetTabs(_tabCount + tabCount);
            Write(String.Concat(Enumerable.Repeat("\t", tabCount)));
        }

        public void SetTabs(int tabCount)
        {
            _tabs = String.Concat(Enumerable.Repeat("\t", tabCount));

            // remove extra tabs on current formated text
            if (_tabCount > tabCount)
            {
                _formatedText = _formatedText.TrimEnd('\t');
                _formatedText += _tabs;
            }

            _tabCount = tabCount;
        }

        public void Write(string text)
        {
            _formatedText += text;
        }

        public void WriteLine(string line)
        {
            _formatedText += line + "\n" + _tabs;
        }
    }
}
