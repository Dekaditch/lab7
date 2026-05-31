using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab1.AST;

namespace lab1.Forms
{
    public partial class AstGraphForm : Form
    {
        private AstNode _root;
        private Panel canvasPanel;
        private TreeNodeLayout _treeLayout;
        private float zoom = 1.0f;
        private Point dragStart;
        private Point panelOffset = new Point(0, 0);
        private bool isDragging = false;

        public AstGraphForm(AstNode root)
        {
            _root = root;
            InitializeComponent();
            CalculateLayout();
        }

        private void InitializeComponent()
        {
            this.Text = "Абстрактное синтаксическое дерево (AST) - графическое представление";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(800, 600);

            // Панель для рисования
            canvasPanel = new Panel();
            canvasPanel.Dock = DockStyle.Fill;
            canvasPanel.BackColor = Color.White;
            canvasPanel.Paint += CanvasPanel_Paint;
            canvasPanel.MouseDown += CanvasPanel_MouseDown;
            canvasPanel.MouseMove += CanvasPanel_MouseMove;
            canvasPanel.MouseUp += CanvasPanel_MouseUp;
            canvasPanel.MouseWheel += CanvasPanel_MouseWheel;

            // Панель управления
            Panel controlPanel = new Panel();
            controlPanel.Dock = DockStyle.Top;
            controlPanel.Height = 40;
            controlPanel.BackColor = SystemColors.Control;

            Button btnZoomIn = new Button();
            btnZoomIn.Text = "+ Увеличить";
            btnZoomIn.Size = new Size(100, 30);
            btnZoomIn.Location = new Point(10, 5);
            btnZoomIn.Click += (s, e) => { zoom *= 1.2f; canvasPanel.Invalidate(); };

            Button btnZoomOut = new Button();
            btnZoomOut.Text = "- Уменьшить";
            btnZoomOut.Size = new Size(100, 30);
            btnZoomOut.Location = new Point(120, 5);
            btnZoomOut.Click += (s, e) => { zoom /= 1.2f; canvasPanel.Invalidate(); };

            Button btnReset = new Button();
            btnReset.Text = "Сбросить вид";
            btnReset.Size = new Size(100, 30);
            btnReset.Location = new Point(230, 5);
            btnReset.Click += (s, e) => { zoom = 1.0f; panelOffset = new Point(0, 0); canvasPanel.Invalidate(); };

            Button btnClose = new Button();
            btnClose.Text = "Закрыть";
            btnClose.Size = new Size(100, 30);
            btnClose.Location = new Point(this.Width - 120, 5);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Click += (s, e) => this.Close();

            Label lblHelp = new Label();
            lblHelp.Text = "Мышь: перетаскивание - перемещение | Колесико - масштаб";
            lblHelp.Size = new Size(350, 30);
            lblHelp.Location = new Point(350, 8);
            lblHelp.ForeColor = Color.Gray;

            controlPanel.Controls.Add(btnZoomIn);
            controlPanel.Controls.Add(btnZoomOut);
            controlPanel.Controls.Add(btnReset);
            controlPanel.Controls.Add(btnClose);
            controlPanel.Controls.Add(lblHelp);

            this.Controls.Add(canvasPanel);
            this.Controls.Add(controlPanel);

            this.Resize += (s, e) => canvasPanel.Invalidate();
        }

        private void CalculateLayout()
        {
            if (_root == null) return;
            _treeLayout = new TreeNodeLayout();
            _treeLayout.Calculate(_root);
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            if (_root == null)
            {
                e.Graphics.DrawString("AST не построено", new Font("Arial", 16), Brushes.Red, 100, 100);
                return;
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Применяем трансформации
            e.Graphics.TranslateTransform(panelOffset.X, panelOffset.Y);
            e.Graphics.ScaleTransform(zoom, zoom);

            // Рисуем связи (линии) сначала, чтобы они были под узлами
            DrawConnections(e.Graphics, _treeLayout.RootNode);

            // Рисуем узлы поверх линий
            DrawNodes(e.Graphics, _treeLayout.RootNode);
        }

        private void DrawNodes(Graphics g, LayoutNode node)
        {
            if (node == null) return;

            // Рисуем узел
            Rectangle bounds = node.Bounds;

            // Закругленный прямоугольник для узла
            using (SolidBrush fillBrush = new SolidBrush(GetNodeColor(node.AstNode)))
            using (Pen borderPen = new Pen(Color.Black, 2))
            {
                g.FillRoundRectangle(fillBrush, bounds, 10);
                g.DrawRoundRectangle(borderPen, bounds, 10);
            }

            // Рисуем текст
            string text = GetNodeText(node.AstNode);
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(text, font, Brushes.Black, bounds, sf);
            }

            // Рисуем атрибуты под узлом (если есть)
            var attrs = node.AstNode.GetAttributes();
            if (attrs.Count > 0)
            {
                int yOffset = bounds.Bottom - (int)(bounds.Height * 0.7);
                using (Font attrFont = new Font("Arial", 8))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    int i = 0;
                    foreach (var attr in attrs)
                    {
                        string attrText = $"{attr.Key}: {attr.Value}";
                        Rectangle attrBounds = new Rectangle(
                            bounds.X,
                            bounds.Bottom + 2 + (i * 14),
                            bounds.Width,
                            14);
                        g.DrawString(attrText, attrFont, Brushes.DarkBlue, attrBounds, sf);
                        i++;
                    }
                }
            }

            // Рекурсивно рисуем дочерние узлы
            foreach (var child in node.Children)
            {
                DrawNodes(g, child);
            }
        }

        private void DrawConnections(Graphics g, LayoutNode node)
        {
            if (node == null) return;

            Point startPoint = new Point(
                node.Bounds.X + node.Bounds.Width / 2,
                node.Bounds.Y + node.Bounds.Height);

            foreach (var child in node.Children)
            {
                Point endPoint = new Point(
                    child.Bounds.X + child.Bounds.Width / 2,
                    child.Bounds.Y);

                // Рисуем линию от родителя к ребенку
                using (Pen pen = new Pen(Color.FromArgb(100, 100, 200), 2))
                {
                    // Рисуем кривую Безье для плавных линий
                    Point ctrl1 = new Point(startPoint.X, startPoint.Y + 20);
                    Point ctrl2 = new Point(endPoint.X, endPoint.Y - 20);

                    g.DrawBezier(pen, startPoint, ctrl1, ctrl2, endPoint);

                    // Стрелочка на конце
                    DrawArrow(g, pen.Color, endPoint, ctrl2);
                }

                DrawConnections(g, child);
            }
        }

        private void DrawArrow(Graphics g, Color color, Point end, Point control)
        {
            float angle = (float)Math.Atan2(end.Y - control.Y, end.X - control.X);
            int arrowSize = 10;

            Point arrow1 = new Point(
                end.X - (int)(arrowSize * Math.Cos(angle - Math.PI / 6)),
                end.Y - (int)(arrowSize * Math.Sin(angle - Math.PI / 6)));
            Point arrow2 = new Point(
                end.X - (int)(arrowSize * Math.Cos(angle + Math.PI / 6)),
                end.Y - (int)(arrowSize * Math.Sin(angle + Math.PI / 6)));

            using (Pen pen = new Pen(color, 2))
            {
                g.DrawLine(pen, end, arrow1);
                g.DrawLine(pen, end, arrow2);
            }
        }

        private string GetNodeText(AstNode node)
        {
            string typeName = node.GetNodeTypeName();

            // Сокращаем длинные названия
            switch (typeName)
            {
                case "FunctionDeclaration": return "Функция";
                case "ReturnStatement": return "Return";
                case "BinaryExpression": return "Binary";
                case "IntType": return "Int";
                case "FloatType": return "Float";
                case "IntLiteral": return "Int";
                case "FloatLiteral": return "Float";
                default: return typeName;
            }
        }

        private Color GetNodeColor(AstNode node)
        {
            string type = node.GetNodeTypeName();

            if (type == "Program") return Color.LightSteelBlue;
            if (type == "FunctionDeclaration") return Color.LightGreen;
            if (type == "Parameter") return Color.LightYellow;
            if (type == "ReturnStatement") return Color.LightCoral;
            if (type == "BinaryExpression") return Color.LightBlue;
            if (type.Contains("Literal")) return Color.LightPink;
            if (type.Contains("Type")) return Color.LightGray;

            return Color.White;
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStart = new Point(e.X, e.Y);
                canvasPanel.Cursor = Cursors.Hand;
            }
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int dx = e.X - dragStart.X;
                int dy = e.Y - dragStart.Y;
                panelOffset = new Point(panelOffset.X + dx, panelOffset.Y + dy);
                dragStart = new Point(e.X, e.Y);
                canvasPanel.Invalidate();
            }
        }

        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            canvasPanel.Cursor = Cursors.Default;
        }

        private void CanvasPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                zoom *= 1.1f;
            else
                zoom /= 1.1f;

            zoom = Math.Max(0.2f, Math.Min(3.0f, zoom));
            canvasPanel.Invalidate();
        }

        // Вспомогательные методы для рисования закругленных прямоугольников
    }

    // Вспомогательный класс для рисования закругленных прямоугольников
    public static class GraphicsExtensions
    {
        public static void FillRoundRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundRectanglePath(rect, radius))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundRectanglePath(rect, radius))
            {
                g.DrawPath(pen, path);
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetRoundRectanglePath(Rectangle rect, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // Класс для расчета расположения узлов на дереве
    public class TreeNodeLayout
    {
        public LayoutNode RootNode { get; private set; }

        private const int NodeWidth = 120;
        private const int NodeHeight = 50;
        private const int HorizontalSpacing = 40;
        private const int VerticalSpacing = 80;

        public void Calculate(AstNode astRoot)
        {
            RootNode = BuildLayoutTree(astRoot);
            CalculatePositions(RootNode, 0, 0);
            AdjustPositions(RootNode);
        }

        private LayoutNode BuildLayoutTree(AstNode astNode)
        {
            if (astNode == null) return null;

            LayoutNode node = new LayoutNode();
            node.AstNode = astNode;
            node.Children = new List<LayoutNode>();

            // Добавляем дочерние узлы
            List<AstNode> children = GetChildren(astNode);
            foreach (var child in children)
            {
                LayoutNode childNode = BuildLayoutTree(child);
                if (childNode != null)
                    node.Children.Add(childNode);
            }

            return node;
        }

        private List<AstNode> GetChildren(AstNode node)
        {
            List<AstNode> children = new List<AstNode>();

            if (node is ProgramNode)
            {
                ProgramNode program = node as ProgramNode;
                if (program.Functions != null)
                    children.AddRange(program.Functions);
            }
            else if (node is FunctionDeclarationNode)
            {
                FunctionDeclarationNode func = node as FunctionDeclarationNode;
                if (func.ReturnType != null)
                    children.Add(func.ReturnType);
                if (func.Parameters != null)
                    children.AddRange(func.Parameters);
                if (func.Body != null)
                    children.Add(func.Body);
            }
            else if (node is ReturnStatementNode)
            {
                ReturnStatementNode ret = node as ReturnStatementNode;
                if (ret.ReturnValue != null)
                    children.Add(ret.ReturnValue);
            }
            else if (node is BinaryExpressionNode)
            {
                BinaryExpressionNode bin = node as BinaryExpressionNode;
                if (bin.Left != null)
                    children.Add(bin.Left);
                if (bin.Right != null)
                    children.Add(bin.Right);
            }
            else if (node is ParameterNode)
            {
                ParameterNode param = node as ParameterNode;
                if (param.Type != null)
                    children.Add(param.Type);
            }

            return children;
        }

        private void CalculatePositions(LayoutNode node, int x, int y)
        {
            if (node == null) return;

            node.Bounds = new Rectangle(x, y, NodeWidth, NodeHeight);

            int childX = x;
            int childY = y + VerticalSpacing;

            if (node.Children.Count > 0)
            {
                int totalWidth = node.Children.Count * NodeWidth + (node.Children.Count - 1) * HorizontalSpacing;
                childX = x + (NodeWidth - totalWidth) / 2;

                for (int i = 0; i < node.Children.Count; i++)
                {
                    CalculatePositions(node.Children[i], childX, childY);
                    childX += NodeWidth + HorizontalSpacing;
                }
            }
        }

        private void AdjustPositions(LayoutNode node)
        {
            if (node == null || node.Children.Count == 0) return;

            // Корректируем позиции детей по X, чтобы они были по центру родителя
            int minX = int.MaxValue;
            int maxX = int.MinValue;

            foreach (var child in node.Children)
            {
                minX = Math.Min(minX, child.Bounds.X);
                maxX = Math.Max(maxX, child.Bounds.Right);
                AdjustPositions(child);
            }

            int childrenCenter = (minX + maxX) / 2;
            int parentCenter = node.Bounds.X + NodeWidth / 2;
            int offset = parentCenter - childrenCenter;

            if (offset != 0)
            {
                foreach (var child in node.Children)
                {
                    OffsetNode(child, offset);
                }
            }
        }

        private void OffsetNode(LayoutNode node, int offsetX)
        {
            node.Bounds = new Rectangle(node.Bounds.X + offsetX, node.Bounds.Y, node.Bounds.Width, node.Bounds.Height);
            foreach (var child in node.Children)
            {
                OffsetNode(child, offsetX);
            }
        }
    }

    public class LayoutNode
    {
        public AstNode AstNode { get; set; }
        public Rectangle Bounds { get; set; }
        public List<LayoutNode> Children { get; set; }
    }
}