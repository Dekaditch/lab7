using System.Collections.Generic;

namespace lab1.AST
{
    public class ReturnStatementNode : AstNode
    {
        private ExpressionNode _returnValue = null;

        public ExpressionNode ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; }
        }

        public override string GetNodeTypeName()
        {
            return "ReturnStatement";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "hasValue", ReturnValue != null }
            };
        }
    }
}