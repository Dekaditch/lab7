using lab1.LexicalAnalysis;
using System.Collections.Generic;

namespace lab1.SemanticAnalysis
{
    public class SymbolTable
    {
        private Dictionary<string, SymbolInfo> _symbols = new Dictionary<string, SymbolInfo>();

        public bool AddSymbol(string name, SymbolInfo info)
        {
            if (_symbols.ContainsKey(name))
                return false;
            _symbols.Add(name, info);
            return true;
        }

        public SymbolInfo GetSymbol(string name)
        {
            if (_symbols.ContainsKey(name))
                return _symbols[name];
            return null;
        }

        public bool Contains(string name)
        {
            return _symbols.ContainsKey(name);
        }

        public void Clear()
        {
            _symbols.Clear();
        }
    }

    public class SymbolInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Token Token { get; set; }
        public object Value { get; set; }

        public SymbolInfo(string name, string type, Token token = null, object value = null)
        {
            Name = name;
            Type = type;
            Token = token;
            Value = value;
        }
    }
}