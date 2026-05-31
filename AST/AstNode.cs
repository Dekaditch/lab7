using lab1.LexicalAnalysis;
using System.Collections.Generic;

namespace lab1.AST
{
    public abstract class AstNode
    {
        private Token _token;

        public Token Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public abstract string GetNodeTypeName();

        public virtual Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>();
        }
    }
}