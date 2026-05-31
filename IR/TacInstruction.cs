using System;
using System.Collections.Generic;

namespace lab1.IR
{
    /// <summary>
    /// Типы инструкций трехадресного кода (TAC)
    /// </summary>
    public enum TacOpcode
    {
        ADD,    // t1 = t2 + t3
        SUB,    // t1 = t2 - t3
        MUL,    // t1 = t2 * t3
        DIV,    // t1 = t2 / t3
        ASSIGN, // t1 = t2
        CALL,   // t1 = call function(args)
        RETURN, // return t1
        LABEL,  // метка функции
        PARAM   // параметр функции
    }

    /// <summary>
    /// Инструкция трехадресного кода
    /// </summary>
    public class TacInstruction
    {
        public TacOpcode Opcode { get; set; }
        public string Result { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public string FuncName { get; set; }
        public List<string> CallArgs { get; set; }

        public TacInstruction()
        {
            CallArgs = new List<string>();
        }

        public TacInstruction(TacOpcode opcode, string result = null, string arg1 = null, string arg2 = null)
        {
            Opcode = opcode;
            Result = result;
            Arg1 = arg1;
            Arg2 = arg2;
            CallArgs = new List<string>();
        }

        public override string ToString()
        {
            switch (Opcode)
            {
                case TacOpcode.ADD: return $"{Result} = {Arg1} + {Arg2}";
                case TacOpcode.SUB: return $"{Result} = {Arg1} - {Arg2}";
                case TacOpcode.MUL: return $"{Result} = {Arg1} * {Arg2}";
                case TacOpcode.DIV: return $"{Result} = {Arg1} / {Arg2}";
                case TacOpcode.ASSIGN: return $"{Result} = {Arg1}";
                case TacOpcode.CALL:
                    string args = string.Join(", ", CallArgs);
                    return $"{Result} = call {FuncName}({args})";
                case TacOpcode.RETURN: return $"return {Arg1}";
                case TacOpcode.PARAM: return $"param {Arg1}";
                case TacOpcode.LABEL: return $"{Arg1}:";
                default: return Opcode.ToString();
            }
        }
    }
}
