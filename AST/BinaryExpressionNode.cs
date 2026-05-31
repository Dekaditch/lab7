using System.Collections.Generic;

namespace lab1.AST
{
    public class BinaryExpressionNode : ExpressionNode
    {
        private ExpressionNode _left = null;
        private string _operator = "";
        private ExpressionNode _right = null;

        public ExpressionNode Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public ExpressionNode Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public override string GetNodeTypeName()
        {
            return "BinaryExpression";
        }

        public override string GetExpressionType()
        {
            string leftType = Left.GetExpressionType();
            string rightType = Right.GetExpressionType();

            if (leftType == "Float" || rightType == "Float")
                return "Float";
            return "Int";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "operator", Operator }
            };
        }
    }
}