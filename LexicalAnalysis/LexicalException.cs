using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.LexicalAnalysis
{

    public class LexicalException : Exception
    {
        public int Line { get; set; }
        public int Position { get; set; }
        public char Character { get; set; }

        public LexicalException(string message, int line, int position, char character)
            : base(message)
        {
            Line = line;
            Position = position;
            Character = character;
        }
    }
}