using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab1.LexicalAnalysis;

namespace lab1.SyntaxAnalysis
{
    public class TokenStream
    {
        private List<Token> tokens;
        private int position;
        private List<SyntaxError> errors;

        public TokenStream(List<Token> tokens)
        {
            this.tokens = tokens;
            this.position = 0;
            this.errors = new List<SyntaxError>();
        }


        public Token Current
        {
            get
            {
                if (position < tokens.Count)
                    return tokens[position];
                return null;
            }
        }


        public bool IsEnd
        {
            get { return position >= tokens.Count; }
        }


        public void Advance()
        {
            if (position < tokens.Count)
                position++;
        }


        public Token Peek(int offset = 1)
        {
            if (position + offset < tokens.Count)
                return tokens[position + offset];
            return null;
        }


        public bool Check(TokenType expectedType)
        {
            return Current != null && Current.Type == expectedType;
        }


        public bool CheckAny(params TokenType[] expectedTypes)
        {
            if (Current == null) return false;
            return expectedTypes.Contains(Current.Type);
        }


        public bool Match(TokenType expectedType)
        {
            if (Check(expectedType))
            {
                Advance();
                return true;
            }
            return false;
        }


        public void AddError(string expected, int line, int position, string fragment, int startIndex)
        {
            string description = $"Ожидалось: {expected}";
            errors.Add(new SyntaxError(fragment, line, position, description, startIndex));
        }

        public List<SyntaxError> GetErrors()
        {
            return errors;
        }
        public int GetCurrentStartIndex()
        {
            if (Current != null)
            {
                return CalculateIndexFromPosition(Current.Line, Current.StartPosition);
            }
            return -1;
        }


        private int CalculateIndexFromPosition(int line, int position)
        {
            return -1;
        }
    }
}