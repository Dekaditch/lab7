using System;
using System.Collections.Generic;
using System.Windows.Forms;
using lab1.AST;
using lab1.LexicalAnalysis;
using lab1.SemanticAnalysis;
using lab1.SyntaxAnalysis;
using lab1.Visualization;
using lab1.Forms;

namespace lab1.Managers
{
    public class SyntaxManager
    {
        private DataGridView _errorGrid;
        private RichTextBox _editor;
        private Label _errorCountLabel;
        private AstNode _currentAST;

        public SyntaxManager(DataGridView errorGrid, RichTextBox editor, Label errorCountLabel)
        {
            _errorGrid = errorGrid;
            _editor = editor;
            _errorCountLabel = errorCountLabel;
            _currentAST = null;
        }

        public void RunAnalysis()
        {
            ClearErrors();
            string sourceCode = _editor.Text;
            if (string.IsNullOrWhiteSpace(sourceCode))
            {
                AddErrorToGrid("Пустой ввод", 0, 0);
                _errorCountLabel.Text = "Ошибок: " + GetRealErrorCount();
                return;
            }

            Scanner scanner = new Scanner();
            List<Token> tokens = scanner.Analyze(sourceCode);

            if (tokens == null || tokens.Count == 0)
            {
                AddErrorToGrid("Не удалось распознать токены", 0, 0);
                _errorCountLabel.Text = "Ошибок: " + GetRealErrorCount();
                return;
            }

            bool hasLexicalErrors = false;
            foreach (Token token in tokens)
            {
                if (token.IsError)
                {
                    AddErrorToGrid(token.ErrorMessage, token.Line, token.StartPosition);
                    hasLexicalErrors = true;
                }
            }

            if (hasLexicalErrors)
            {
                _errorCountLabel.Text = "Ошибок: " + GetRealErrorCount();
                return;
            }

            Parser parser = new Parser();
            ProgramNode programAST = parser.ParseProgramWithAST(tokens);

            if (parser.Errors != null)
            {
                foreach (SyntaxError error in parser.Errors)
                {
                    AddErrorToGrid(error.Description, error.Line, error.Position);
                }
            }

            if (parser.Errors != null && parser.Errors.Count > 0 || programAST == null)
            {
                _errorCountLabel.Text = "Ошибок: " + GetRealErrorCount();

                if (parser.Errors != null && parser.Errors.Count > 0)
                {
                    MessageBox.Show("Обнаружены синтаксические ошибки!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            bool isValid = semanticAnalyzer.Analyze(programAST);

            foreach (SemanticError error in semanticAnalyzer.Errors)
            {
                AddErrorToGrid(error.Message, error.Line, error.Position);
            }

            _errorCountLabel.Text = "Ошибок: " + GetRealErrorCount();

            if (isValid && semanticAnalyzer.Errors.Count == 0)
            {
                _currentAST = programAST;
                MessageBox.Show("Семантический анализ успешно завершён!\nПрограмма синтаксически и семантически корректна.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (semanticAnalyzer.Errors.Count > 0)
            {
                _currentAST = null;
                MessageBox.Show("Обнаружены семантические ошибки!\nПроверьте таблицу ошибок.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _currentAST = null;
                MessageBox.Show("Анализ завершён, но обнаружены некритичные ошибки.",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ShowAST()
        {
            if (_currentAST == null)
            {
                MessageBox.Show("Сначала выполните анализ (кнопка 'Запуск').\nУбедитесь, что программа синтаксически и семантически корректна.",
                    "AST не построено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AstViewerForm viewer = new AstViewerForm(_currentAST);
            viewer.ShowDialog();
        }

        public void ShowASTGraph()
        {
            if (_currentAST == null)
            {
                MessageBox.Show("Сначала выполните анализ (кнопка 'Запуск').\nУбедитесь, что программа синтаксически и семантически корректна.",
                    "AST не построено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AstGraphForm graphForm = new AstGraphForm(_currentAST);
            graphForm.ShowDialog();
        }

        public AstNode GetCurrentAST()
        {
            return _currentAST;
        }

        private void ClearErrors()
        {
            _errorGrid.Rows.Clear();
            _errorCountLabel.Text = "Ошибок: 0";
        }

        private void AddErrorToGrid(string message, int line, int position)
        {
            try
            {
                // Безопасно удаляем только если есть строки и последняя является новой
                if (_errorGrid.Rows.Count > 0)
                {
                    DataGridViewRow lastRow = _errorGrid.Rows[_errorGrid.Rows.Count - 1];
                    if (lastRow.IsNewRow)
                    {
                        _errorGrid.Rows.RemoveAt(_errorGrid.Rows.Count - 1);
                    }
                }
                _errorGrid.Rows.Add(message, line, position);
            }
            catch (InvalidOperationException)
            {
                // Если не удалось удалить новую строку, просто добавляем ошибку
                _errorGrid.Rows.Add(message, line, position);
            }
        }

        private int GetRealErrorCount()
        {
            int count = 0;
            try
            {
                foreach (DataGridViewRow row in _errorGrid.Rows)
                {
                    // Игнорируем пустую строку для ввода
                    if (!row.IsNewRow)
                    {
                        count++;
                    }
                }
            }
            catch
            {
                // В случае ошибки возвращаем количество строк
                return _errorGrid.Rows.Count;
            }
            return count;
        }
    }
}
