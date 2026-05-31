using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.LexicalAnalysis
{
    public class Scanner
    {
        private string input;
        private int position;
        private int line;
        private int linePosition;
        private List<Token> tokens;

        private HashSet<string> keywords = new HashSet<string>
        {
            "int", "return", "void", "char", "float",
            "if", "else", "while", "for", "switch",
            "case", "break", "continue", "default"
        };

        public Scanner()
        {
            tokens = new List<Token>();
        }

        public List<Token> Analyze(string text)
        {
            input = text;
            position = 0;
            line = 1;
            linePosition = 1;
            tokens.Clear();

            while (position < input.Length)
            {
                char current = input[position];

                if (current == ' ' || current == '\t')
                {
                    position++;
                    linePosition++;
                    continue;
                }
                else if (current == '\n')
                {
                    line++;
                    linePosition = 1;
                    position++;
                    continue;
                }
                else if (current == '\r')
                {
                    position++;
                    if (position < input.Length && input[position] == '\n')
                    {
                        line++;
                        linePosition = 1;
                        position++;
                    }
                    continue;
                }
                else if (char.IsLetter(current) || current == '_')
                {
                    ProcessIdentifierOrKeyword();
                }
                else if (char.IsDigit(current))
                {
                    ProcessNumber();
                }
                else
                {
                    ProcessSymbol();
                }
            }
            tokens.Add(new Token
            {
                Type = TokenType.EOF,
                Value = "EOF",
                Line = line,
                StartPosition = linePosition,
                EndPosition = linePosition
            });
            return tokens;
        }

        private void ProcessIdentifierOrKeyword()
        {
            int startPos = linePosition;
            int startIndex = position;
            int startLine = line;

            bool hasInvalidChar = false;

            while (position < input.Length &&
                   !char.IsWhiteSpace(input[position]) &&
                   !IsSeparator(input[position]))
            {
                if (!char.IsLetterOrDigit(input[position]) &&
                    input[position] != '_')
                {
                    hasInvalidChar = true;
                }

                position++;
                linePosition++;
            }

            string value = input.Substring(startIndex, position - startIndex);

            Token token = new Token
            {
                Value = value,
                Line = startLine,
                StartPosition = startPos,
                EndPosition = linePosition - 1
            };

            if (hasInvalidChar)
            {
                token.Type = TokenType.UNKNOWN;
                token.IsError = true;
                token.ErrorMessage = $"Недопустимая последовательность '{value}'";
            }
            else if (keywords.Contains(value))
            {
                switch (value)
                {
                    case "int":
                        token.Type = TokenType.KW_INT;
                        break;

                    case "return":
                        token.Type = TokenType.KW_RETURN;
                        break;

                    case "float":
                        token.Type = TokenType.KW_FLOAT;
                        break;

                    default:
                        token.Type = TokenType.IDENTIFIER;
                        break;
                }
            }
            else
            {
                token.Type = TokenType.IDENTIFIER;
            }

            tokens.Add(token);
        }
        private bool IsSeparator(char c)
        {
            return c == '=' ||
                   c == '+' ||
                   c == '-' ||
                   c == '*' ||
                   c == '/' ||
                   c == '(' ||
                   c == ')' ||
                   c == '{' ||
                   c == '}' ||
                   c == ';' ||
                   c == ',' ||
                   c == '<' ||
                   c == '>' ||
                   c == '!';
        }
        private void ProcessNumber()
        {
            int startPos = linePosition;
            int startIndex = position;
            int startLine = line;
            bool hasDecimalPoint = false;

            while (position < input.Length && char.IsDigit(input[position]))
            {
                position++;
                linePosition++;
            }

            if (position < input.Length && input[position] == '.')
            {
                hasDecimalPoint = true;
                position++;
                linePosition++;

                while (position < input.Length && char.IsDigit(input[position]))
                {
                    position++;
                    linePosition++;
                }
            }

            string value = input.Substring(startIndex, position - startIndex);

            Token token = new Token
            {
                Value = value,
                Line = startLine,
                StartPosition = startPos,
                EndPosition = linePosition - 1
            };

            token.Type = hasDecimalPoint ? TokenType.FLOAT : TokenType.INTEGER;
            tokens.Add(token);
        }

        private void ProcessSymbol()
        {
            char current = input[position];
            int startPos = linePosition;
            int startLine = line;

            Token token = new Token
            {
                Line = startLine,
                StartPosition = startPos,
                EndPosition = startPos
            };

            if (position + 1 < input.Length)
            {
                string twoChar = current.ToString() + input[position + 1].ToString();

                if (twoChar == "==" || twoChar == "!=" || twoChar == "<=" || twoChar == ">=")
                {
                    token.Value = twoChar;
                    token.EndPosition = startPos + 1;

                    switch (twoChar)
                    {
                        case "==": token.Type = TokenType.EQUAL; break;
                        case "!=": token.Type = TokenType.NOT_EQUAL; break;
                        case "<=": token.Type = TokenType.LESS; break;
                        case ">=": token.Type = TokenType.GREATER; break;
                    }

                    position += 2;
                    linePosition += 2;
                    tokens.Add(token);
                    return;
                }
            }

            token.Value = current.ToString();

            switch (current)
            {
                case '=': token.Type = TokenType.ASSIGN; break;
                case '+': token.Type = TokenType.PLUS; break;
                case '-': token.Type = TokenType.MINUS; break;
                case '*': token.Type = TokenType.MULTIPLY; break;
                case '/':
                    if (position + 1 < input.Length && input[position + 1] == '/')
                    {
                        ProcessComment();
                        return;
                    }
                    token.Type = TokenType.DIVIDE;
                    break;
                case '(': token.Type = TokenType.LPAREN; break;
                case ')': token.Type = TokenType.RPAREN; break;
                case '{': token.Type = TokenType.LBRACE; break;
                case '}': token.Type = TokenType.RBRACE; break;
                case ';': token.Type = TokenType.SEMICOLON; break;
                case ',': token.Type = TokenType.COMMA; break;
                case '<': token.Type = TokenType.LESS; break;
                case '>': token.Type = TokenType.GREATER; break;
                default:
                    token.Type = TokenType.UNKNOWN;
                    token.IsError = true;
                    token.ErrorMessage = $"Недопустимый символ '{current}'";
                    break;
            }

            position++;
            linePosition++;
            tokens.Add(token);
        }

        private void ProcessComment()
        {
            int startPos = linePosition;
            int startIndex = position;
            int startLine = line;

            position += 2;
            linePosition += 2;

            while (position < input.Length && input[position] != '\n')
            {
                position++;
                linePosition++;
            }

            string value = input.Substring(startIndex, position - startIndex);

            Token token = new Token
            {
                Type = TokenType.COMMENT,
                Value = value,
                Line = startLine,
                StartPosition = startPos,
                EndPosition = linePosition - 1
            };

            tokens.Add(token);
        }
    }
}
