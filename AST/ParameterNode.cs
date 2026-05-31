using System.Collections.Generic;

namespace lab1.AST
{
    public class ParameterNode : AstNode
    {
        private string _name = "";
        private TypeNode _type = null;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public TypeNode Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public override string GetNodeTypeName()
        {
            return "Parameter";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "name", Name },
                { "type", Type != null ? Type.TypeName : "unknown" }
            };
        }
    }
}