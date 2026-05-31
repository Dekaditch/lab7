using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab1.AST;

namespace lab1.IR
{
    public class TacViewerForm : Form
    {
        private TabControl _tabControl;
        private RichTextBox _txtOriginalIR;
        private RichTextBox _txtFolding;
        private RichTextBox _txtSimplification;
        private RichTextBox _txtCombined;
        private Button _btnClose;
        private ProgramNode _astRoot;

        public TacViewerForm(ProgramNode astRoot)
        {
            _astRoot = astRoot;
            InitializeComponent();
            GenerateAndDisplay();
        }

        private void InitializeComponent()
        {
            this.Text = "Трёхадресный код и оптимизации";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(700, 400);

            _tabControl = new TabControl();
            _tabControl.Dock = DockStyle.Fill;
            _tabControl.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Вкладка 0: исходный IR
            TabPage tabOriginal = new TabPage();
            tabOriginal.Text = "Исходный IR";
            _txtOriginalIR = CreateRichTextBox();
            tabOriginal.Controls.Add(_txtOriginalIR);

            // Вкладка 1: свёртка констант
            TabPage tabFolding = new TabPage();
            tabFolding.Text = "Свёртка констант";
            _txtFolding = CreateRichTextBox();
            tabFolding.Controls.Add(_txtFolding);

            // Вкладка 2: упрощение арифметики
            TabPage tabSimplification = new TabPage();
            tabSimplification.Text = "Упрощение арифметики";
            _txtSimplification = CreateRichTextBox();
            tabSimplification.Controls.Add(_txtSimplification);

            // Вкладка 3: объединённая оптимизация
            TabPage tabCombined = new TabPage();
            tabCombined.Text = "Объединённая оптимизация";
            _txtCombined = CreateRichTextBox();
            tabCombined.Controls.Add(_txtCombined);

            _tabControl.TabPages.Add(tabOriginal);
            _tabControl.TabPages.Add(tabFolding);
            _tabControl.TabPages.Add(tabSimplification);
            _tabControl.TabPages.Add(tabCombined);

            // Нижняя панель с кнопкой закрытия
            Panel bottomPanel = new Panel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 45;
            bottomPanel.BackColor = SystemColors.Control;

            _btnClose = new Button();
            _btnClose.Text = "Закрыть";
            _btnClose.Size = new Size(100, 30);
            _btnClose.Location = new Point(this.ClientSize.Width - 120, 8);
            _btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnClose.Click += (s, e) => this.Close();

            bottomPanel.Controls.Add(_btnClose);
            this.Controls.Add(_tabControl);
            this.Controls.Add(bottomPanel);

            this.Resize += (s, e) =>
            {
                if (_btnClose != null)
                    _btnClose.Location = new Point(this.ClientSize.Width - 120, 8);
            };
        }

        private RichTextBox CreateRichTextBox()
        {
            var rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            rtb.Font = new Font("Consolas", 10);
            rtb.ReadOnly = true;
            rtb.WordWrap = false;
            rtb.BackColor = Color.White;
            return rtb;
        }

        private void GenerateAndDisplay()
        {
            if (_astRoot == null)
            {
                string msg = "AST не построено. Сначала выполните анализ (кнопка «Пуск»).";
                _txtOriginalIR.Text = msg;
                _txtFolding.Text = msg;
                _txtSimplification.Text = msg;
                _txtCombined.Text = msg;
                return;
            }

            var generator = new TacGenerator();
            generator.Generate(_astRoot);
            var originalInstructions = new List<TacInstruction>(generator.Instructions);

            _txtOriginalIR.Text = FormatTac(originalInstructions, "Исходный IR");

            var optimizer = new Optimizer();

            // Свёртка констант
            var foldedInstructions = optimizer.ApplyConstantFolding(originalInstructions);
            _txtFolding.Text = FormatTac(foldedInstructions, "Свёртка констант");

            // Упрощение арифметики
            var simplifiedInstructions = optimizer.ApplyArithmeticSimplification(originalInstructions);
            _txtSimplification.Text = FormatTac(simplifiedInstructions, "Упрощение арифметики");

            // Объединённая оптимизация (сначала свёртка, потом упрощение)
            var combined = optimizer.ApplyAllOptimizations(originalInstructions);
            _txtCombined.Text = FormatTac(combined, "Объединённая оптимизация");
        }

        private string FormatTac(List<TacInstruction> instructions, string title)
        {
            if (instructions == null || instructions.Count == 0)
                return "Нет сгенерированных инструкций TAC";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"=== {title} ===");
            sb.AppendLine();

            int lineNum = 1;
            foreach (var instr in instructions)
            {
                if (instr.Opcode == TacOpcode.LABEL)
                {
                    sb.AppendLine();
                    sb.AppendLine($"{instr.Arg1}:");
                }
                else
                {
                    sb.AppendLine($"{lineNum,3}: {instr}");
                    lineNum++;
                }
            }
            return sb.ToString();
        }
    }
}