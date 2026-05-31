using System.Collections.Generic;

namespace lab1.AST
{
    public class LiteralNode : ExpressionNode
    {
        private object _value = 0;
        private string _literalType = "";

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string LiteralType
        {
            get { return _literalType; }
            set { _literalType = value; }
        }

        public override string GetNodeTypeName()
        {
            return LiteralType + "Literal";
        }

        public override string GetExpressionType()
        {
            return LiteralType;
        }
        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "value", Value }
            };
        }
    }
}