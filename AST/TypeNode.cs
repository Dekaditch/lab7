using System.Collections.Generic;

namespace lab1.AST
{
    public class TypeNode : AstNode
    {
        private string _typeName = "";

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public override string GetNodeTypeName()
        {
            return TypeName == "Int" ? "IntType" : "FloatType";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "name", TypeName }
            };
        }
    }
}