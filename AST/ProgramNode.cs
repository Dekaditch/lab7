using System.Collections.Generic;

namespace lab1.AST
{
    public class ProgramNode : AstNode
    {
        private List<FunctionDeclarationNode> _functions = new List<FunctionDeclarationNode>();

        public List<FunctionDeclarationNode> Functions
        {
            get { return _functions; }
            set { _functions = value; }
        }

        public override string GetNodeTypeName()
        {
            return "Program";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "functionCount", Functions.Count }
            };
        }
    }
}