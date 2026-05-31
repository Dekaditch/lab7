using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.LexicalAnalysis
{

    public class Token
    {
        public TokenType Type { get; set; }         
        public string Value { get; set; }          
        public int Line { get; set; }                
        public int StartPosition { get; set; }       
        public int EndPosition { get; set; }         
        public bool IsError { get; set; }            
        public string ErrorMessage { get; set; }     

        public Token()
        {
            IsError = false;
        }

        public override string ToString()
        {
            return $"[{Line}:{StartPosition}-{EndPosition}] {Type}: '{Value}'";
        }

        public string GetTypeDescription()
        {
            switch (Type)
            {
                case TokenType.KW_INT: return "ключевое слово int";
                case TokenType.KW_RETURN: return "ключевое слово return";
                case TokenType.KW_FLOAT: return "ключевое слово float";

                case TokenType.IDENTIFIER: return "идентификатор";

                case TokenType.INTEGER: return "целое число";
                case TokenType.FLOAT: return "число с плавающей точкой";

                case TokenType.ASSIGN: return "оператор присваивания";
                case TokenType.PLUS: return "оператор сложения";
                case TokenType.MINUS: return "оператор вычитания";
                case TokenType.MULTIPLY: return "оператор умножения";
                case TokenType.DIVIDE: return "оператор деления";
                case TokenType.EQUAL: return "оператор сравнения ==";
                case TokenType.NOT_EQUAL: return "оператор сравнения !=";
                case TokenType.LESS: return "оператор меньше";
                case TokenType.GREATER: return "оператор больше";

                case TokenType.LPAREN: return "открывающая скобка (";
                case TokenType.RPAREN: return "закрывающая скобка )";
                case TokenType.LBRACE: return "открывающая фигурная скобка {";
                case TokenType.RBRACE: return "закрывающая фигурная скобка }";
                case TokenType.SEMICOLON: return "конец оператора ;";
                case TokenType.COMMA: return "разделитель ,";
                case TokenType.SPACE: return "пробел";
                case TokenType.TAB: return "табуляция";
                case TokenType.NEWLINE: return "перевод строки";

                case TokenType.COMMENT: return "комментарий";

                case TokenType.UNKNOWN: return "недопустимый символ";

                default: return Type.ToString();
            }
        }

        public int GetCode()
        {
            return (int)Type;
        }
    }
}