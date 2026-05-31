using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.LexicalAnalysis
{
    public enum TokenType
    {
        KW_INT = 1,
        KW_RETURN = 4,
        KW_FLOAT = 2,

        IDENTIFIER = 3,

        INTEGER = 21,
        FLOAT = 22,

        ASSIGN = 31,
        PLUS = 32,
        MINUS = 33,
        MULTIPLY = 34,
        DIVIDE = 35,
        EQUAL = 36,
        NOT_EQUAL = 37,
        LESS = 38,
        GREATER = 39,

        LPAREN = 41,
        RPAREN = 42,
        LBRACE = 43,
        RBRACE = 44,
        SEMICOLON = 45,
        COMMA = 46,
        SPACE = 47,
        TAB = 48,
        NEWLINE = 49,
        EOF = 50,
        COMMENT = 51,
        UNKNOWN = 99
    }
}