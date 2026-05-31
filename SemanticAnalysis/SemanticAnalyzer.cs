using System;
using System.Collections.Generic;
using lab1.AST;
using lab1.LexicalAnalysis;

namespace lab1.SemanticAnalysis
{
    public class SemanticError
    {
        public string Message { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }

        public SemanticError(string message, int line, int position)
        {
            Message = message;
            Line = line;
            Position = position;
        }

        public override string ToString()
        {
            return $"Строка {Line}, позиция {Position}: {Message}";
        }
    }

    public class SemanticAnalyzer
    {
        private SymbolTable _symbolTable = new SymbolTable();
        private List<SemanticError> _errors = new List<SemanticError>();

        public List<SemanticError> Errors
        {
            get { return _errors; }
        }

        public ProgramNode AST { get; private set; }

        public bool Analyze(ProgramNode ast)
        {
            _errors.Clear();
            _symbolTable.Clear();
            AST = ast;

            foreach (var function in ast.Functions)
            {
                AnalyzeFunction(function);
            }

            return _errors.Count == 0;
        }

        private void AnalyzeFunction(FunctionDeclarationNode function)
        {
            // Правило 1: уникальность имени функции
            if (_symbolTable.Contains(function.Name))
            {
                _errors.Add(new SemanticError(
                    $"Идентификатор \"{function.Name}\" уже объявлен ранее",
                    function.Token != null ? function.Token.Line : 0,
                    function.Token != null ? function.Token.StartPosition : 0));
                return;
            }

            // Добавляем функцию в таблицу символов
            _symbolTable.AddSymbol(function.Name, new SymbolInfo(
                function.Name,
                function.ReturnType.TypeName,
                function.Token));

            // СОЗДАЁМ НОВУЮ ОБЛАСТЬ ВИДИМОСТИ ДЛЯ ПАРАМЕТРОВ И ТЕЛА ФУНКЦИИ
            // Сохраняем старую таблицу и создаём новую
            SymbolTable parentTable = _symbolTable;
            _symbolTable = new SymbolTable();

            // Добавляем параметры в таблицу символов
            foreach (ParameterNode param in function.Parameters)
            {
                if (_symbolTable.Contains(param.Name))
                {
                    _errors.Add(new SemanticError(
                        $"Параметр \"{param.Name}\" уже объявлен",
                        param.Token != null ? param.Token.Line : 0,
                        param.Token != null ? param.Token.StartPosition : 0));
                }
                else
                {
                    _symbolTable.AddSymbol(param.Name, new SymbolInfo(
                        param.Name,
                        param.Type.TypeName,
                        param.Token));
                }
            }

            // Анализируем тело функции
            AnalyzeReturnStatement(function.Body, function.ReturnType.TypeName);

            // Восстанавливаем родительскую таблицу
            _symbolTable = parentTable;
        }

        private void AnalyzeReturnStatement(ReturnStatementNode returnStmt, string expectedType)
        {
            if (returnStmt.ReturnValue != null)
            {
                string exprType = AnalyzeExpression(returnStmt.ReturnValue);

                // Правило 2: совместимость типов
                if (!IsTypeCompatible(expectedType, exprType))
                {
                    _errors.Add(new SemanticError(
                        $"Несовместимость типов: ожидается {expectedType}, получен {exprType}",
                        returnStmt.Token != null ? returnStmt.Token.Line : 0,
                        returnStmt.Token != null ? returnStmt.Token.StartPosition : 0));
                }

                // Правило 3: допустимые значения (для литералов)
                if (returnStmt.ReturnValue is LiteralNode)
                {
                    LiteralNode literal = returnStmt.ReturnValue as LiteralNode;
                    CheckValueBounds(literal);
                }
            }
        }

        private string AnalyzeExpression(ExpressionNode expr)
        {
            if (expr is LiteralNode)
            {
                LiteralNode literal = expr as LiteralNode;
                return literal.LiteralType;
            }
            else if (expr is IdentifierNode)
            {
                IdentifierNode identifier = expr as IdentifierNode;
                return AnalyzeIdentifier(identifier);
            }
            else if (expr is BinaryExpressionNode)
            {
                BinaryExpressionNode binary = expr as BinaryExpressionNode;
                return AnalyzeBinaryExpression(binary);
            }
            else
            {
                return "Unknown";
            }
        }

        private string AnalyzeIdentifier(IdentifierNode identifier)
        {
            // Правило 4: идентификатор должен быть объявлен
            SymbolInfo symbol = _symbolTable.GetSymbol(identifier.Name);
            if (symbol == null)
            {
                _errors.Add(new SemanticError(
                    $"Использование необъявленного идентификатора \"{identifier.Name}\"",
                    identifier.Token != null ? identifier.Token.Line : 0,
                    identifier.Token != null ? identifier.Token.StartPosition : 0));
                return "Unknown";
            }

            return symbol.Type;
        }

        private string AnalyzeBinaryExpression(BinaryExpressionNode binary)
        {
            string leftType = AnalyzeExpression(binary.Left);
            string rightType = AnalyzeExpression(binary.Right);

            // Правило 2: совместимость типов в бинарной операции
            if (!IsTypeCompatible(leftType, rightType))
            {
                _errors.Add(new SemanticError(
                    $"Несовместимость типов в операции {binary.Operator}: {leftType} и {rightType}",
                    binary.Token != null ? binary.Token.Line : 0,
                    binary.Token != null ? binary.Token.StartPosition : 0));
            }

            // Определяем результирующий тип
            if (leftType == "Float" || rightType == "Float")
                return "Float";
            return "Int";
        }

        private bool IsTypeCompatible(string expected, string actual)
        {
            if (expected == actual) return true;
            if (expected == "Float" && actual == "Int") return true;
            return false;
        }

        private void CheckValueBounds(LiteralNode literal)
        {
            if (literal == null) return;

            if (literal.LiteralType == "Int")
            {
                int value = Convert.ToInt32(literal.Value);
                if (value < -2147483648 || value > 2147483647)
                {
                    _errors.Add(new SemanticError(
                        $"Значение {value} выходит за пределы допустимого диапазона для Int",
                        literal.Token != null ? literal.Token.Line : 0,
                        literal.Token != null ? literal.Token.StartPosition : 0));
                }
            }
            else if (literal.LiteralType == "Float")
            {
                double value = Convert.ToDouble(literal.Value);
                if (Math.Abs(value) > 1.7e308)
                {
                    _errors.Add(new SemanticError(
                        $"Значение {value} выходит за пределы допустимого диапазона для Float",
                        literal.Token != null ? literal.Token.Line : 0,
                        literal.Token != null ? literal.Token.StartPosition : 0));
                }
            }
        }
    }
}