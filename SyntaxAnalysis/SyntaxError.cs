using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.SyntaxAnalysis
{
    public class SyntaxError
    {
        public string InvalidFragment { get; set; }     
        public int Line { get; set; }                    
        public int Position { get; set; }                
        public string Description { get; set; }          
        public int StartIndex { get; set; }              

        public SyntaxError(string fragment, int line, int position, string description, int startIndex)
        {
            InvalidFragment = fragment;
            Line = line;
            Position = position;
            Description = description;
            StartIndex = startIndex;
        }

        public override string ToString()
        {
            return $"Ошибка в строке {Line}, позиция {Position}: '{InvalidFragment}' - {Description}";
        }
    }
}