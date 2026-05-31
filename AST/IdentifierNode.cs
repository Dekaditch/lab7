using System.Collections.Generic;

namespace lab1.AST
{
    public class IdentifierNode : ExpressionNode
    {
        private string _name = "";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string GetNodeTypeName()
        {
            return "Identifier";
        }

        public override string GetExpressionType()
        {
            return "Unknown";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "name", Name }
            };
        }
    }
}