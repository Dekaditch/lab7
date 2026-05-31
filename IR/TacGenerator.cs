using System;
using System.Collections.Generic;
using lab1.AST;

namespace lab1.IR
{
    public class TacGenerator
    {
        private List<TacInstruction> _instructions;
        private int _tempCounter;

        public List<TacInstruction> Instructions => _instructions;

        public TacGenerator()
        {
            _instructions = new List<TacInstruction>();
            _tempCounter = 0;
        }

        private string NewTemp()
        {
            return $"t{++_tempCounter}";
        }

        public void Generate(ProgramNode program)
        {
            _instructions.Clear();
            _tempCounter = 0;

            if (program?.Functions == null) return;

            foreach (var function in program.Functions)
            {
                GenerateFunction(function);
            }
        }

        private void GenerateFunction(FunctionDeclarationNode function)
        {
            if (function == null) return;

            _instructions.Add(new TacInstruction(TacOpcode.LABEL, null, function.Name));

            if (function.Parameters != null)
            {
                foreach (var param in function.Parameters)
                {
                    string paramTemp = NewTemp();
                    _instructions.Add(new TacInstruction(TacOpcode.ASSIGN, paramTemp, param.Name));
                }
            }

            if (function.Body is ReturnStatementNode returnStmt)
            {
                string result = GenerateExpression(returnStmt.ReturnValue);
                _instructions.Add(new TacInstruction(TacOpcode.RETURN, null, result));
            }
        }

        private string GenerateExpression(ExpressionNode expr)
        {
            if (expr == null) return null;

            if (expr is BinaryExpressionNode binary)
            {
                string leftTemp = GenerateExpression(binary.Left);
                string rightTemp = GenerateExpression(binary.Right);
                string result = NewTemp();
                TacOpcode opcode = GetOpcode(binary.Operator);
                _instructions.Add(new TacInstruction(opcode, result, leftTemp, rightTemp));
                return result;
            }

            if (expr is LiteralNode literal)
            {
                // Возвращаем значение литерала как строку (константу)
                return literal.Value.ToString();
            }

            if (expr is IdentifierNode identifier)
            {
                // Для идентификатора возвращаем имя переменной
                return identifier.Name;
            }

            return null;
        }

        private TacOpcode GetOpcode(string op)
        {
            switch (op)
            {
                case "+": return TacOpcode.ADD;
                case "-": return TacOpcode.SUB;
                case "*": return TacOpcode.MUL;
                case "/": return TacOpcode.DIV;
                default: return TacOpcode.ASSIGN;
            }
        }

        public string GetTextRepresentation()
        {
            if (_instructions.Count == 0) return "Нет сгенерированного TAC";

            var result = new System.Text.StringBuilder();
            int lineNum = 1;

            foreach (var instr in _instructions)
            {
                if (instr.Opcode == TacOpcode.LABEL)
                {
                    result.AppendLine($"\n{instr.Arg1}:");
                }
                else
                {
                    result.AppendLine($"{lineNum,3}: {instr}");
                    lineNum++;
                }
            }

            return result.ToString();
        }
    }
}