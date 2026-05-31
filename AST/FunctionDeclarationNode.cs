using System.Collections.Generic;

namespace lab1.AST
{
    public class FunctionDeclarationNode : AstNode
    {
        private string _name = "";
        private TypeNode _returnType = null;
        private List<ParameterNode> _parameters = new List<ParameterNode>();
        private ReturnStatementNode _body = null;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public TypeNode ReturnType
        {
            get { return _returnType; }
            set { _returnType = value; }
        }

        public List<ParameterNode> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public ReturnStatementNode Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public override string GetNodeTypeName()
        {
            return "FunctionDeclaration";
        }

        public override Dictionary<string, object> GetAttributes()
        {
            return new Dictionary<string, object>
            {
                { "name", Name },
                { "returnType", ReturnType != null ? ReturnType.TypeName : "void" }
            };
        }
    }
}