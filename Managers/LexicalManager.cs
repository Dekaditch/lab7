using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab1.LexicalAnalysis;

namespace lab1.Managers
{
    public class LexicalManager
    {
        private Scanner scanner;
        private DataGridView dataGridView;
        private RichTextBox editorTextBox;

        public LexicalManager(DataGridView dataGridView, RichTextBox editorTextBox)
        {
            this.scanner = new Scanner();
            this.dataGridView = dataGridView;
            this.editorTextBox = editorTextBox;

            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            if (dataGridView == null) return;

            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;

            DataGridViewTextBoxColumn codeColumn = new DataGridViewTextBoxColumn();
            codeColumn.Name = "Code";
            codeColumn.HeaderText = "Условный код";
            codeColumn.Width = 80;
            dataGridView.Columns.Add(codeColumn);

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.Name = "Type";
            typeColumn.HeaderText = "Тип лексемы";
            typeColumn.Width = 200;
            dataGridView.Columns.Add(typeColumn);

            DataGridViewTextBoxColumn valueColumn = new DataGridViewTextBoxColumn();
            valueColumn.Name = "Value";
            valueColumn.HeaderText = "Лексема";
            valueColumn.Width = 150;
            dataGridView.Columns.Add(valueColumn);

            DataGridViewTextBoxColumn locationColumn = new DataGridViewTextBoxColumn();
            locationColumn.Name = "Location";
            locationColumn.HeaderText = "Местоположение";
            locationColumn.Width = 200;
            dataGridView.Columns.Add(locationColumn);

            dataGridView.CellClick += DataGridView_CellClick;
        }

        public void RunAnalysis()
        {
            try
            {
                string text = editorTextBox.Text;

                dataGridView.Rows.Clear();

                List<Token> tokens = scanner.Analyze(text);

                foreach (Token token in tokens)
                {

                    int rowIndex = dataGridView.Rows.Add();
                    DataGridViewRow row = dataGridView.Rows[rowIndex];

                    row.Cells["Code"].Value = token.GetCode();

                    string typeDesc = token.GetTypeDescription();
                    if (token.IsError)
                        typeDesc = $"ОШИБКА: {token.ErrorMessage}";
                    row.Cells["Type"].Value = typeDesc;

                    string displayValue = token.Value;
                    if (token.Type == TokenType.SPACE)
                        displayValue = "[пробел]";
                    else if (token.Type == TokenType.TAB)
                        displayValue = "[табуляция]";
                    else if (token.Type == TokenType.NEWLINE)
                        displayValue = "[перевод строки]";
                    row.Cells["Value"].Value = displayValue;

                    string location;
                    if (token.StartPosition == token.EndPosition)
                        location = $"строка {token.Line}, {token.StartPosition}";
                    else
                        location = $"строка {token.Line}, {token.StartPosition}-{token.EndPosition}";
                    row.Cells["Location"].Value = location;

                    row.Tag = token;
                }

                if (tokens.Count == 0)
                {
                    dataGridView.Rows.Add(new object[] { "-", "-", "Нет лексем", "-" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при анализе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (row.Tag is Token token)
            {
                NavigateToToken(token);
            }
        }

        private void NavigateToToken(Token token)
        {
            try
            {
                int charIndex = GetCharIndexFromPosition(token.Line, token.StartPosition);
                if (charIndex >= 0)
                {
                    editorTextBox.SelectionStart = charIndex;
                    editorTextBox.SelectionLength = token.EndPosition - token.StartPosition + 1;
                    editorTextBox.ScrollToCaret();
                    editorTextBox.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private int GetCharIndexFromPosition(int line, int position)
        {
            string[] lines = editorTextBox.Text.Split('\n');
            if (line <= lines.Length)
            {
                int charIndex = 0;
                for (int i = 0; i < line - 1; i++)
                {
                    charIndex += lines[i].Length + 1; 
                }
                return charIndex + (position - 1);
            }
            return -1;
        }

        public void ClearResults()
        {
            dataGridView.Rows.Clear();
        }
    }
}