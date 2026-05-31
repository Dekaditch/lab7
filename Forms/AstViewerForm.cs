using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using lab1.AST;
using lab1.Visualization;

namespace lab1.Forms
{
    public partial class AstViewerForm : Form
    {
        private TreeView treeView;
        private Button btnClose;
        private Button btnExportText;
        private Button btnExportJson;
        private SplitContainer splitContainer;
        private RichTextBox txtTextRepresentation;
        private TabControl tabControl;

        private AstNode _root;

        public AstViewerForm(AstNode root)
        {
            _root = root;
            InitializeComponent();
            BuildTree(root);
            ShowTextRepresentation(root);
        }

        private void InitializeComponent()
        {
            this.Text = "Абстрактное синтаксическое дерево (AST)";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(800, 500);

            // Создаем TabControl для переключения между представлениями
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Вкладка с графическим деревом
            TabPage treeTab = new TabPage();
            treeTab.Text = "Дерево AST";

            treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Font = new Font("Consolas", 10);
            treeView.AfterSelect += TreeView_AfterSelect;
            treeTab.Controls.Add(treeView);

            // Вкладка с текстовым представлением
            TabPage textTab = new TabPage();
            textTab.Text = "Текстовое представление";

            txtTextRepresentation = new RichTextBox();
            txtTextRepresentation.Dock = DockStyle.Fill;
            txtTextRepresentation.Font = new Font("Consolas", 10);
            txtTextRepresentation.ReadOnly = true;
            txtTextRepresentation.WordWrap = false;
            textTab.Controls.Add(txtTextRepresentation);

            tabControl.TabPages.Add(treeTab);
            tabControl.TabPages.Add(textTab);

            // Панель с кнопками внизу
            Panel bottomPanel = new Panel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 45;
            bottomPanel.BackColor = SystemColors.Control;

            btnExportText = new Button();
            btnExportText.Text = "Экспорт в текст";
            btnExportText.Size = new Size(120, 30);
            btnExportText.Location = new Point(10, 8);
            btnExportText.Click += BtnExportText_Click;

            btnExportJson = new Button();
            btnExportJson.Text = "Экспорт в JSON";
            btnExportJson.Size = new Size(120, 30);
            btnExportJson.Location = new Point(140, 8);
            btnExportJson.Click += BtnExportJson_Click;

            btnClose = new Button();
            btnClose.Text = "Закрыть";
            btnClose.Size = new Size(100, 30);
            btnClose.Location = new Point(this.ClientSize.Width - 120, 8);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Click += (s, e) => this.Close();

            bottomPanel.Controls.Add(btnExportText);
            bottomPanel.Controls.Add(btnExportJson);
            bottomPanel.Controls.Add(btnClose);

            // Статусбар для информации о выбранном узле
            StatusStrip statusStrip = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Выберите узел для просмотра информации";
            statusStrip.Items.Add(statusLabel);

            // Добавляем все элементы на форму
            this.Controls.Add(tabControl);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(statusStrip);

            // Обработчик изменения размера окна
            this.Resize += AstViewerForm_Resize;
        }

        private void AstViewerForm_Resize(object sender, EventArgs e)
        {
            // Перемещаем кнопку закрытия при изменении размера
            if (btnClose != null)
            {
                btnClose.Location = new Point(this.ClientSize.Width - 120, 8);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Показываем информацию о выбранном узле в статусбаре
            if (e.Node != null && e.Node.Tag != null)
            {
                AstNode node = (AstNode)e.Node.Tag;
                string info = string.Format("Узел: {0}", node.GetNodeTypeName());

                foreach (var attr in node.GetAttributes())
                {
                    info += string.Format(" | {0}: {1}", attr.Key, attr.Value);
                }

                ToolStripStatusLabel statusLabel = (ToolStripStatusLabel)((StatusStrip)this.Controls[this.Controls.Count - 1]).Items[0];
                statusLabel.Text = info;
            }
        }

        private void BuildTree(AstNode node)
        {
            treeView.Nodes.Clear();
            if (node == null)
            {
                treeView.Nodes.Add("AST пуст");
                return;
            }

            TreeNode rootNode = CreateTreeNode(node);
            treeView.Nodes.Add(rootNode);
            treeView.ExpandAll();
        }

        private TreeNode CreateTreeNode(AstNode node)
        {
            if (node == null)
                return new TreeNode("null");

            // Формируем текст узла
            string nodeText = node.GetNodeTypeName();
            Dictionary<string, object> attrs = node.GetAttributes();

            if (attrs.Count > 0)
            {
                nodeText += " (";
                bool first = true;
                foreach (var attr in attrs)
                {
                    if (!first)
                        nodeText += ", ";
                    nodeText += string.Format("{0}={1}", attr.Key, attr.Value);
                    first = false;
                }
                nodeText += ")";
            }

            TreeNode treeNode = new TreeNode(nodeText);
            treeNode.Tag = node; // Сохраняем ссылку на узел

            // Добавляем дочерние узлы
            foreach (var child in GetChildren(node))
            {
                if (child != null)
                {
                    treeNode.Nodes.Add(CreateTreeNode(child));
                }
            }

            return treeNode;
        }

        private List<AstNode> GetChildren(AstNode node)
        {
            List<AstNode> children = new List<AstNode>();

            if (node is ProgramNode)
            {
                ProgramNode program = node as ProgramNode;
                if (program.Functions != null)
                {
                    foreach (var func in program.Functions)
                        children.Add(func);
                }
            }
            else if (node is FunctionDeclarationNode)
            {
                FunctionDeclarationNode function = node as FunctionDeclarationNode;
                if (function.ReturnType != null)
                    children.Add(function.ReturnType);
                if (function.Parameters != null)
                {
                    foreach (var param in function.Parameters)
                        children.Add(param);
                }
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

        private void ShowTextRepresentation(AstNode node)
        {
            if (node == null)
            {
                txtTextRepresentation.Text = "AST пуст";
                return;
            }

            string textRep = AstVisualizer.VisualizeToText(node);
            txtTextRepresentation.Text = textRep;
        }

        private void BtnExportText_Click(object sender, EventArgs e)
        {
            string text = AstVisualizer.VisualizeToText(_root);
            ShowExportDialog(text, "ast_export.txt", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void BtnExportJson_Click(object sender, EventArgs e)
        {
            string json = AstVisualizer.VisualizeToJson(_root);
            ShowExportDialog(json, "ast_export.json", "JSON files (*.json)|*.json|All files (*.*)|*.*");
        }

        private void ShowExportDialog(string content, string defaultFileName, string filter)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Сохранить AST";
            saveDialog.FileName = defaultFileName;
            saveDialog.Filter = filter;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveDialog.FileName, content);
                MessageBox.Show(string.Format("AST сохранён в файл:\n{0}", saveDialog.FileName),
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}