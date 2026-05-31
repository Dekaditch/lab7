using System.Windows.Forms;

namespace lab1.Managers
{
    public class ClipboardManager
    {
        public void CopyText(RichTextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                Clipboard.SetText(textBox.SelectedText);
            }
        }

        public void CutText(ref RichTextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                Clipboard.SetText(textBox.SelectedText);
                textBox.SelectedText = "";
            }
        }

        public void PasteText(RichTextBox textBox)
        {
            if (Clipboard.ContainsText())
            {
                textBox.SelectedText = Clipboard.GetText();
            }
        }

        public bool CanPaste()
        {
            return Clipboard.ContainsText();
        }
    }
}