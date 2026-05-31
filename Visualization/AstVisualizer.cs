using System.Collections.Generic;
using System.Text;
using lab1.AST;

namespace lab1.Visualization
{
    public static class AstVisualizer
    {
        public static string VisualizeToText(AstNode node, int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            string indentStr = new string(' ', indent * 2);

            sb.AppendLine($"{indentStr}├── {node.GetNodeTypeName()}");

            Dictionary<string, object> attrs = node.GetAttributes();
            foreach (KeyValuePair<string, object> attr in attrs)
            {
                sb.AppendLine($"{indentStr}│   ├── {attr.Key}: {attr.Value}");
            }

            List<AstNode> children = GetChildren(node);
            foreach (AstNode child in children)
            {
                if (child != null)
                {
                    string childText = VisualizeToText(child, indent + 1);
                    sb.Append(childText);
                }
            }

            return sb.ToString();
        }

        private static List<AstNode> GetChildren(AstNode node)
        {
            List<AstNode> children = new List<AstNode>();

            if (node is ProgramNode)
            {
                ProgramNode program = node as ProgramNode;
                children.AddRange(program.Functions);
            }
            else if (node is FunctionDeclarationNode)
            {
                FunctionDeclarationNode function = node as FunctionDeclarationNode;
                if (function.ReturnType != null)
                    children.Add(function.ReturnType);
                children.AddRange(function.Parameters);
                if (function.Body != null)
                    children.Add(function.Body);
            }
            else if (node is ReturnStatementNode)
            {
                ReturnStatementNode returnStmt = node as ReturnStatementNode;
                if (returnStmt.ReturnValue != null)
                    children.Add(returnStmt.ReturnValue);
            }
            else if (node is BinaryExpressionNode)
            {
                BinaryExpressionNode binary = node as BinaryExpressionNode;
                if (binary.Left != null)
                    children.Add(binary.Left);
                if (binary.Right != null)
                    children.Add(binary.Right);
            }
            else if (node is ParameterNode)
            {
                ParameterNode parameter = node as ParameterNode;
                if (parameter.Type != null)
                    children.Add(parameter.Type);
            }

            return children;
        }

        public static string VisualizeToJson(AstNode node)
        {
            return SerializeToJson(node, 0);
        }

        private static string SerializeToJson(AstNode node, int indent)
        {
            if (node == null) return "null";

            string indentStr = new string(' ', indent * 2);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{indentStr}{{");
            sb.AppendLine($"{indentStr}  \"type\": \"{node.GetNodeTypeName()}\",");

            sb.AppendLine($"{indentStr}  \"attributes\": {{");
            Dictionary<string, object> attrs = node.GetAttributes();
            int attrIndex = 0;
            foreach (KeyValuePair<string, object> attr in attrs)
            {
                string comma = attrIndex < attrs.Count - 1 ? "," : "";
                sb.AppendLine($"{indentStr}    \"{attr.Key}\": \"{attr.Value}\"{comma}");
                attrIndex++;
            }
            sb.AppendLine($"{indentStr}  }},");

            sb.AppendLine($"{indentStr}  \"children\": [");
            List<AstNode> children = GetChildren(node);
            for (int i = 0; i < children.Count; i++)
            {
                string comma = i < children.Count - 1 ? "," : "";
                string childJson = SerializeToJson(children[i], indent + 2);
                sb.Append(childJson);
                if (!string.IsNullOrEmpty(comma))
                    sb.AppendLine(comma);
            }
            sb.AppendLine($"{indentStr}  ]");
            sb.Append($"{indentStr}}}");

            return sb.ToString();
        }
    }
}