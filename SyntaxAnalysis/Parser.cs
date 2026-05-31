using System;
using System.Collections.Generic;
using System.Linq;
using lab1.AST;
using lab1.LexicalAnalysis;

namespace lab1.SyntaxAnalysis
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _index;
        private bool _panicMode;
        private List<SyntaxError> _errors;
        private ProgramNode _astRoot;

        public List<SyntaxError> Errors
        {
            get { return _errors; }
        }

        public Parser()
        {
            _errors = new List<SyntaxError>();
            _index = 0;
            _panicMode = false;
            _astRoot = null;
        }

        private Token Current
        {
            get
            {
                if (_index < _tokens.Count)
                    return _tokens[_index];
                else if (_tokens.Count > 0)
                    return _tokens[_tokens.Count - 1];
                else
                    return null;
            }
        }

        public ProgramNode ParseProgramWithAST(List<Token> tokens)
        {
            ParseProgram(tokens);

            if (_errors.Count > 0)
                return null;

            return _astRoot;
        }

        public void ParseProgram(List<Token> tokens)
        {
            _errors.Clear();
            _index = 0;
            _panicMode = false;

            _tokens = new List<Token>();

            if (tokens == null || tokens.Count == 0)
            {
                AddError(null, "Пустой ввод");
                return;
            }

            // Фильтруем пробельные токены
            foreach (Token t in tokens)
            {
                if (t.Type != TokenType.SPACE &&
                    t.Type != TokenType.TAB &&
                    t.Type != TokenType.NEWLINE)
                {
                    _tokens.Add(t);
                }
            }

            if (_tokens.Count == 0)
            {
                AddError(null, "Пустой ввод");
                return;
            }

            if (_tokens[_tokens.Count - 1].Type != TokenType.EOF)
            {
                Token last = _tokens[_tokens.Count - 1];
                Token eofToken = new Token();
                eofToken.Type = TokenType.EOF;
                eofToken.Value = "EOF";
                eofToken.Line = last.Line;
                eofToken.StartPosition = last.EndPosition + 1;
                eofToken.EndPosition = last.EndPosition + 3;
                _tokens.Add(eofToken);
            }

            // Строим AST
            _astRoot = ParseFunctionWithAST();

            if (Current != null && Current.Type != TokenType.EOF)
            {
                AddError(Current, "Лишние токены после конца программы");
            }
            _panicMode = false;
        }

        private ProgramNode ParseFunctionWithAST()
        {
            ProgramNode program = new ProgramNode();
            program.Functions = new List<FunctionDeclarationNode>();

            FunctionDeclarationNode function = new FunctionDeclarationNode();

            // Парсим тип возврата
            TypeNode returnType = ParseTypeWithAST();
            if (returnType == null)
            {
                AddError(Current, "Ожидался тип возврата (int или float)");
                return program;
            }
            function.ReturnType = returnType;

            // Имя функции
            if (Current == null || Current.Type != TokenType.IDENTIFIER)
            {
                AddError(Current, "Ожидалось имя функции");
                return program;
            }
            Token nameToken = Current;
            function.Name = nameToken.Value;
            function.Token = nameToken;
            _index++;

            // (
            if (Current == null || Current.Type != TokenType.LPAREN)
            {
                AddError(Current, "Ожидалась '('");
                return program;
            }
            _index++;

            // Параметры
            function.Parameters = ParseParametersWithAST();

            // )
            if (Current == null || Current.Type != TokenType.RPAREN)
            {
                AddError(Current, "Ожидалась ')'");
                return program;
            }
            _index++;

            // {
            if (Current == null || Current.Type != TokenType.LBRACE)
            {
                AddError(Current, "Ожидалась '{'");
                return program;
            }
            _index++;

            // return
            if (Current == null || Current.Type != TokenType.KW_RETURN)
            {
                AddError(Current, "Ожидалось 'return'");
                return program;
            }
            _index++;

            // Выражение
            ExpressionNode expr = ParseExpressionWithAST();
            if (expr == null)
            {
                AddError(Current, "Ожидалось выражение");
                return program;
            }

            ReturnStatementNode returnStmt = new ReturnStatementNode();
            returnStmt.ReturnValue = expr;
            returnStmt.Token = Current;
            function.Body = returnStmt;

            // ;
            if (Current == null || Current.Type != TokenType.SEMICOLON)
            {
                AddError(Current, "Ожидалась ';'");
                return program;
            }
            _index++;

            // }
            if (Current == null || Current.Type != TokenType.RBRACE)
            {
                AddError(Current, "Ожидалась '}'");
                return program;
            }
            _index++;

            program.Functions.Add(function);
            return program;
        }

        private TypeNode ParseTypeWithAST()
        {
            if (Current == null)
                return null;

            TypeNode typeNode = new TypeNode();

            if (Current.Type == TokenType.KW_INT)
            {
                typeNode.TypeName = "Int";
                typeNode.Token = Current;
                _index++;
                return typeNode;
            }
            else if (Current.Type == TokenType.KW_FLOAT)
            {
                typeNode.TypeName = "Float";
                typeNode.Token = Current;
                _index++;
                return typeNode;
            }

            return null;
        }

        private List<ParameterNode> ParseParametersWithAST()
        {
            List<ParameterNode> parameters = new List<ParameterNode>();

            if (Current == null || Current.Type == TokenType.RPAREN)
                return parameters;

            while (true)
            {
                ParameterNode param = new ParameterNode();

                TypeNode paramType = ParseTypeWithAST();
                if (paramType == null)
                {
                    AddError(Current, "Ожидался тип параметра");
                    break;
                }
                param.Type = paramType;

                if (Current == null || Current.Type != TokenType.IDENTIFIER)
                {
                    AddError(Current, "Ожидалось имя параметра");
                    break;
                }
                param.Name = Current.Value;
                param.Token = Current;
                _index++;

                parameters.Add(param);

                if (Current != null && Current.Type == TokenType.COMMA)
                {
                    _index++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            return parameters;
        }

        private ExpressionNode ParseExpressionWithAST()
        {
            ExpressionNode left = ParseTermWithAST();
            if (left == null)
                return null;

            while (Current != null && (Current.Type == TokenType.PLUS || Current.Type == TokenType.MINUS))
            {
                Token opToken = Current;
                string op = Current.Value;
                _index++;

                ExpressionNode right = ParseTermWithAST();
                if (right == null)
                    return null;

                BinaryExpressionNode binary = new BinaryExpressionNode();
                binary.Left = left;
                binary.Operator = op;
                binary.Right = right;
                binary.Token = opToken;
                left = binary;
            }

            return left;
        }

        private ExpressionNode ParseTermWithAST()
        {
            ExpressionNode left = ParseFactorWithAST();
            if (left == null)
                return null;

            while (Current != null && (Current.Type == TokenType.MULTIPLY || Current.Type == TokenType.DIVIDE))
            {
                Token opToken = Current;
                string op = Current.Value;
                _index++;

                ExpressionNode right = ParseFactorWithAST();
                if (right == null)
                    return null;

                BinaryExpressionNode binary = new BinaryExpressionNode();
                binary.Left = left;
                binary.Operator = op;
                binary.Right = right;
                binary.Token = opToken;
                left = binary;
            }

            return left;
        }

        private ExpressionNode ParseFactorWithAST()
        {
            if (Current == null || Current.Type == TokenType.EOF)
                return null;

            if (Current.Type == TokenType.IDENTIFIER)
            {
                Token token = Current;
                _index++;

                IdentifierNode identifier = new IdentifierNode();
                identifier.Name = token.Value;
                identifier.Token = token;
                return identifier;
            }

            if (Current.Type == TokenType.INTEGER)
            {
                Token token = Current;
                _index++;

                LiteralNode literal = new LiteralNode();
                literal.Value = int.Parse(token.Value);
                literal.LiteralType = "Int";
                literal.Token = token;
                return literal;
            }

            if (Current.Type == TokenType.FLOAT)
            {
                Token token = Current;
                _index++;

                LiteralNode literal = new LiteralNode();
                literal.Value = double.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture);
                literal.LiteralType = "Float";
                literal.Token = token;
                return literal;
            }

            if (Current.Type == TokenType.LPAREN)
            {
                _index++;
                ExpressionNode expr = ParseExpressionWithAST();
                if (expr == null)
                    return null;

                if (Current == null || Current.Type != TokenType.RPAREN)
                {
                    AddError(Current, "Ожидалась ')'");
                    return null;
                }
                _index++;
                return expr;
            }

            AddError(Current, "Ожидалось выражение (идентификатор, число или '(')");
            return null;
        }

        private void AddError(Token token, string message)
        {
            if (token == null)
            {
                _errors.Add(new SyntaxError("EOF", 0, 0, message, 0));
            }
            else
            {
                _errors.Add(new SyntaxError(
                    token.Value ?? "EOF",
                    token.Line,
                    token.StartPosition,
                    message,
                    token.StartPosition));
            }
        }

        // Существующие методы синтаксического анализа без AST (для обратной совместимости)
        private void Match(TokenType expected, params TokenType[] followSet)
        {
            if (Current == null)
                return;

            if (_panicMode && Current.Type == TokenType.EOF)
                return;

            if (Current.Type == expected)
            {
                _index++;
                _panicMode = false;
                return;
            }

            AddError(
                Current,
                string.Format("Ожидался {0}, найден {1}", expected, Current.Type)
            );

            if (Current.Type == TokenType.EOF)
            {
                _panicMode = true;
                return;
            }

            if (followSet != null && followSet.Contains(Current.Type))
            {
                return;
            }

            if ((expected == TokenType.KW_RETURN || expected == TokenType.KW_INT ||
                expected == TokenType.KW_FLOAT || expected == TokenType.IDENTIFIER) &&
                Current.Type == TokenType.UNKNOWN &&
                !string.IsNullOrEmpty(Current.Value))
            {
                _index++;
                return;
            }

            Recover(expected, followSet);
        }

        private void Recover(TokenType expected, TokenType[] followSet)
        {
            if (Current == null || Current.Type == TokenType.EOF)
                return;

            Token current = Current;
            Token next = _index + 1 < _tokens.Count ? _tokens[_index + 1] : null;

            bool expectedKeyword = expected == TokenType.KW_INT ||
                expected == TokenType.KW_FLOAT ||
                expected == TokenType.KW_RETURN;

            if (expectedKeyword && current.Type == TokenType.IDENTIFIER)
            {
                _index++;
                return;
            }

            if (next != null && next.Type == expected)
            {
                _index += 2;
                return;
            }

            while (Current != null && Current.Type != TokenType.EOF)
            {
                if (followSet != null && followSet.Contains(Current.Type))
                    break;

                if (Current.Type == TokenType.UNKNOWN && !string.IsNullOrEmpty(Current.Value))
                {
                    string lowerValue = Current.Value.ToLower();
                    if (lowerValue.StartsWith("r") ||
                        lowerValue.Contains("ret") ||
                        lowerValue == "re" ||
                        lowerValue == "ret" ||
                        lowerValue == "retu" ||
                        lowerValue == "retur" ||
                        lowerValue == "return")
                    {
                        break;
                    }
                }

                _index++;
            }
        }

        private void ParseFunction()
        {
            ParseType();
            Match(TokenType.IDENTIFIER, TokenType.LPAREN, TokenType.LBRACE);
            Match(TokenType.LPAREN, TokenType.KW_INT, TokenType.KW_FLOAT, TokenType.RPAREN);
            ParseParameters();
            Match(TokenType.RPAREN, TokenType.LBRACE);
            Match(TokenType.LBRACE, TokenType.KW_RETURN);

            if (Current.Type == TokenType.UNKNOWN && !string.IsNullOrEmpty(Current.Value) && Current.Value.ToLower().Contains("ret"))
            {
                AddError(Current, string.Format("Ожидался KW_RETURN, найден {0}", Current.Type));
                _index++;
                _panicMode = false;
            }
            else
            {
                Match(TokenType.KW_RETURN, TokenType.IDENTIFIER, TokenType.INTEGER, TokenType.FLOAT, TokenType.LPAREN, TokenType.UNKNOWN);
            }

            ParseExpression();
            Match(TokenType.SEMICOLON, TokenType.RBRACE);
            Match(TokenType.RBRACE, TokenType.SEMICOLON);
            Match(TokenType.SEMICOLON, TokenType.EOF);
        }

        private bool ParseType()
        {
            if (Current != null && (Current.Type == TokenType.KW_INT || Current.Type == TokenType.KW_FLOAT))
            {
                _index++;
                return true;
            }
            else if (Current != null && Current.Type != TokenType.EOF)
            {
                AddError(Current, string.Format("Ожидался KW_INT, KW_FLOAT, найден {0}", Current.Type));
                _index++;
            }
            return false;
        }

        private void ParseParameters()
        {
            if (Current == null || Current.Type == TokenType.EOF)
                return;

            if (Current.Type == TokenType.RPAREN)
            {
                AddError(Current, "Функция должна иметь минимум 1 параметр");
                return;
            }

            ParseParameter();

            while (Current != null &&
                   Current.Type != TokenType.RPAREN &&
                   Current.Type != TokenType.LBRACE &&
                   Current.Type != TokenType.EOF)
            {
                if (Current.Type == TokenType.COMMA)
                {
                    Match(TokenType.COMMA);
                    ParseParameter();
                }
                else
                {
                    AddError(Current, string.Format("Ожидался COMMA, найден {0}", Current.Type));

                    while (Current != null &&
                           Current.Type != TokenType.COMMA &&
                           Current.Type != TokenType.RPAREN &&
                           Current.Type != TokenType.LBRACE &&
                           Current.Type != TokenType.EOF)
                    {
                        _index++;
                    }

                    if (Current != null && Current.Type == TokenType.COMMA)
                    {
                        Match(TokenType.COMMA);
                        ParseParameter();
                    }
                }
            }
        }

        private void ParseParameter()
        {
            ParseType();
            if (Current != null && Current.Type == TokenType.IDENTIFIER)
            {
                _index++;
            }
            else
            {
                AddError(Current, string.Format("Ожидался IDENTIFIER, найден {0}", Current?.Type));
            }
        }

        private void ParseExpression()
        {
            ParseTerm();
            while (Current != null && (Current.Type == TokenType.PLUS || Current.Type == TokenType.MINUS))
            {
                _index++;
                ParseTerm();
            }
        }

        private void ParseTerm()
        {
            ParseFactor();
            while (Current != null && (Current.Type == TokenType.MULTIPLY || Current.Type == TokenType.DIVIDE))
            {
                _index++;
                ParseFactor();
            }
        }

        private void ParseFactor()
        {
            if (Current == null || Current.Type == TokenType.EOF)
                return;

            if (Current.Type == TokenType.IDENTIFIER ||
                Current.Type == TokenType.INTEGER ||
                Current.Type == TokenType.FLOAT)
            {
                _index++;
                return;
            }

            if (Current.Type == TokenType.LPAREN)
            {
                Match(TokenType.LPAREN, TokenType.IDENTIFIER, TokenType.INTEGER, TokenType.FLOAT);
                ParseExpression();
                Match(TokenType.RPAREN, TokenType.MULTIPLY, TokenType.DIVIDE, TokenType.PLUS, TokenType.MINUS, TokenType.SEMICOLON);
                return;
            }

            AddError(Current, string.Format("Ожидалось выражение, найден {0}", Current.Type));
            Recover(TokenType.IDENTIFIER, new TokenType[] { TokenType.SEMICOLON, TokenType.RPAREN, TokenType.PLUS, TokenType.MINUS, TokenType.MULTIPLY, TokenType.DIVIDE });
        }
    }
}