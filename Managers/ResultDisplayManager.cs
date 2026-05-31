using System;
using System.Windows.Forms;

namespace TextEditor.Managers
{
    public class ResultDisplayManager
    {
        private ListBox resultArea;
        private RichTextBox editorArea;

        public ResultDisplayManager(ListBox resultArea, RichTextBox editorArea)
        {
            this.resultArea = resultArea;
            this.editorArea = editorArea;
            this.resultArea.SelectedIndexChanged += ResultArea_SelectedIndexChanged;
        }

        public void DisplayMessage(string message)
        {
            resultArea.Items.Add(message);
        }

        public void DisplayError(string error, int line, int position)
        {
            string errorMessage = $"Ошибка в строке {line}, позиция {position}: {error}";
            int index = resultArea.Items.Add(errorMessage);

            resultArea.Tag = new Tuple<int, int>(line, position);
        }

        public void ClearResults()
        {
            resultArea.Items.Clear();
        }

        private void ResultArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resultArea.SelectedItem != null && resultArea.Tag is Tuple<int, int> position)
            {
                int charIndex = FindPositionInText(position.Item1, position.Item2);
                if (charIndex >= 0)
                {
                    editorArea.SelectionStart = charIndex;
                    editorArea.SelectionLength = 0;
                    editorArea.ScrollToCaret();
                    editorArea.Focus();
                }
            }
        }

        private int FindPositionInText(int line, int position)
        {
            string[] lines = editorArea.Text.Split('\n');
            if (line <= lines.Length)
            {
                int charIndex = 0;
                for (int i = 0; i < line - 1; i++)
                {
                    charIndex += lines[i].Length + 1;
                }
                return charIndex + Math.Min(position, lines[line - 1].Length);
            }
            return -1;
        }
    }
}