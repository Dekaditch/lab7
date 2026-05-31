using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.IR
{
    public class OptimizationResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> AppliedOptimizations { get; set; }

        public OptimizationResult()
        {
            AppliedOptimizations = new List<string>();
        }
    }

    public class Optimizer
    {
        private List<OptimizationResult> _optimizationResults;

        public List<OptimizationResult> OptimizationResults => _optimizationResults;

        public Optimizer()
        {
            _optimizationResults = new List<OptimizationResult>();
        }

        public List<TacInstruction> ApplyConstantFolding(List<TacInstruction> instructions)
        {
            var result = new List<TacInstruction>();
            var appliedOptimizations = new List<string>();
            var constValues = new Dictionary<string, string>(); // временная -> значение константы

            foreach (var instr in instructions)
            {
                // Если это присваивание константы временной переменной
                if (instr.Opcode == TacOpcode.ASSIGN && IsConstant(instr.Arg1) && instr.Result != null && instr.Result.StartsWith("t"))
                {
                    constValues[instr.Result] = instr.Arg1;
                    result.Add(instr); // сохраняем как есть
                    continue;
                }

                // Арифметическая операция
                if (IsArithmeticOp(instr.Opcode))
                {
                    string left = instr.Arg1;
                    string right = instr.Arg2;

                    // Подстановка известных констант
                    if (constValues.ContainsKey(left)) left = constValues[left];
                    if (constValues.ContainsKey(right)) right = constValues[right];

                    if (IsConstant(left) && IsConstant(right))
                    {
                        double val1 = ParseConstant(left);
                        double val2 = ParseConstant(right);
                        double computed = ComputeOperation(instr.Opcode, val1, val2);
                        string constResult = computed.ToString();

                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, constResult);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Свёртка констант: {instr} → {newInstr}");
                        constValues[instr.Result] = constResult;
                        continue;
                    }
                }

                result.Add(instr);
            }

            if (appliedOptimizations.Count > 0)
            {
                _optimizationResults.Add(new OptimizationResult
                {
                    Name = "Свёртка констант (Constant Folding)",
                    Description = "Вычисление константных выражений на этапе компиляции",
                    AppliedOptimizations = appliedOptimizations
                });
            }

            return result;
        }


        public List<TacInstruction> ApplyArithmeticSimplification(List<TacInstruction> instructions)
        {
            var result = new List<TacInstruction>();
            var appliedOptimizations = new List<string>();
            var constValues = new Dictionary<string, string>();

            foreach (var instr in instructions)
            {
                // Обновляем словарь констант из предыдущих присваиваний
                if (instr.Opcode == TacOpcode.ASSIGN && IsConstant(instr.Arg1) && instr.Result != null && instr.Result.StartsWith("t"))
                {
                    constValues[instr.Result] = instr.Arg1;
                }

                bool optimized = false;

                if (instr.Opcode == TacOpcode.MUL)
                {
                    string left = instr.Arg1;
                    string right = instr.Arg2;
                    if (constValues.ContainsKey(left)) left = constValues[left];
                    if (constValues.ContainsKey(right)) right = constValues[right];

                    if (right == "0" || left == "0")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, "0");
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        constValues[instr.Result] = "0";
                    }
                    else if (right == "1")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, left);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        if (IsConstant(left)) constValues[instr.Result] = left;
                    }
                    else if (left == "1")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, right);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        if (IsConstant(right)) constValues[instr.Result] = right;
                    }
                }
                else if (instr.Opcode == TacOpcode.ADD)
                {
                    string left = instr.Arg1;
                    string right = instr.Arg2;
                    if (constValues.ContainsKey(left)) left = constValues[left];
                    if (constValues.ContainsKey(right)) right = constValues[right];

                    if (right == "0")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, left);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        if (IsConstant(left)) constValues[instr.Result] = left;
                    }
                    else if (left == "0")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, right);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        if (IsConstant(right)) constValues[instr.Result] = right;
                    }
                }
                else if (instr.Opcode == TacOpcode.SUB)
                {
                    string left = instr.Arg1;
                    string right = instr.Arg2;
                    if (constValues.ContainsKey(left)) left = constValues[left];
                    if (constValues.ContainsKey(right)) right = constValues[right];

                    if (right == "0")
                    {
                        var newInstr = new TacInstruction(TacOpcode.ASSIGN, instr.Result, left);
                        result.Add(newInstr);
                        appliedOptimizations.Add($"Упрощение: {instr} → {newInstr}");
                        optimized = true;
                        if (IsConstant(left)) constValues[instr.Result] = left;
                    }
                }

                if (!optimized)
                {
                    result.Add(instr);
                }
            }

            if (appliedOptimizations.Count > 0)
            {
                _optimizationResults.Add(new OptimizationResult
                {
                    Name = "Упрощение арифметики",
                    Description = "Упрощение выражений с 0 и 1 (x*0=0, x*1=x, x+0=x, x-0=x)",
                    AppliedOptimizations = appliedOptimizations
                });
            }

            return result;
        }


        public List<TacInstruction> ApplyConstantPropagation(List<TacInstruction> instructions)
        {
            var result = new List<TacInstruction>();
            var appliedOptimizations = new List<string>();
            var constValues = new Dictionary<string, string>();

            foreach (var instr in instructions)
            {
                // Копируем инструкцию для возможной замены
                var newInstr = new TacInstruction(instr.Opcode, instr.Result, instr.Arg1, instr.Arg2);
                newInstr.CallArgs = new List<string>(instr.CallArgs);
                newInstr.FuncName = instr.FuncName;

                // Заменяем аргументы, если они известные константы
                if (newInstr.Arg1 != null && constValues.ContainsKey(newInstr.Arg1))
                {
                    newInstr.Arg1 = constValues[newInstr.Arg1];
                    appliedOptimizations.Add($"Propagation: {instr.Arg1} → {newInstr.Arg1}");
                }
                if (newInstr.Arg2 != null && constValues.ContainsKey(newInstr.Arg2))
                {
                    newInstr.Arg2 = constValues[newInstr.Arg2];
                    appliedOptimizations.Add($"Propagation: {instr.Arg2} → {newInstr.Arg2}");
                }

                // Если это присваивание константы временной переменной, запоминаем
                if (newInstr.Opcode == TacOpcode.ASSIGN && IsConstant(newInstr.Arg1) && newInstr.Result != null && newInstr.Result.StartsWith("t"))
                {
                    constValues[newInstr.Result] = newInstr.Arg1;
                }

                result.Add(newInstr);
            }

            if (appliedOptimizations.Count > 0)
            {
                _optimizationResults.Add(new OptimizationResult
                {
                    Name = "Распространение констант (Constant Propagation)",
                    Description = "Замена переменных, хранящих константы, на сами константы",
                    AppliedOptimizations = appliedOptimizations
                });
            }

            return result;
        }

        public List<TacInstruction> ApplyAllOptimizations(List<TacInstruction> instructions)
        {
            var result = instructions;
            // Сначала распространение констант, чтобы раскрыть константные переменные
            result = ApplyConstantPropagation(result);
            // Затем свёртка констант
            result = ApplyConstantFolding(result);
            // Потом арифметическое упрощение
            result = ApplyArithmeticSimplification(result);
            // Ещё раз распространение, чтобы убрать лишние присваивания (опционально)
            result = ApplyConstantPropagation(result);
            return result;
        }

        private bool IsArithmeticOp(TacOpcode opcode)
        {
            return opcode == TacOpcode.ADD || opcode == TacOpcode.SUB ||
                   opcode == TacOpcode.MUL || opcode == TacOpcode.DIV;
        }

        private bool IsConstant(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return double.TryParse(value, out _);
        }

        private double ParseConstant(string value)
        {
            double.TryParse(value, out double result);
            return result;
        }

        private double ComputeOperation(TacOpcode opcode, double a, double b)
        {
            switch (opcode)
            {
                case TacOpcode.ADD: return a + b;
                case TacOpcode.SUB: return a - b;
                case TacOpcode.MUL: return a * b;
                case TacOpcode.DIV: return b != 0 ? a / b : 0;
                default: return a;
            }
        }
    }
}