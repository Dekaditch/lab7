using System;
using System.IO;
using System.Windows.Forms;
using TextEditor.Managers;

namespace lab1.Managers
{
    public class DocumentManager
    {
        private RichTextBox textBox;
        private FileService fileService;
        private MainForm mainForm;
        private string currentFilePath;
        private bool hasChanges;

        public event EventHandler DocumentSaved;
        public event EventHandler DocumentOpened;
        public event EventHandler DocumentCreated;

        public DocumentManager(RichTextBox textBox, FileService fileService, MainForm mainForm)
        {
            this.textBox = textBox;
            this.fileService = fileService;
            this.mainForm = mainForm;
        }

        public void NewDocument()
        {
            if (PromptSaveChanges())
            {
                textBox.Clear();
                currentFilePath = null;
                hasChanges = false;
                UpdateFormTitle();
                DocumentCreated?.Invoke(this, EventArgs.Empty);
            }
        }

        public void OpenDocument()
        {
            if (!PromptSaveChanges())
                return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string testsDir = Path.GetFullPath(Path.Combine(baseDir, "tests"));

                if (Directory.Exists(testsDir))
                {
                    openFileDialog.InitialDirectory = testsDir;
                }

                openFileDialog.Filter = fileService.GetFileFilter();
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string content = fileService.ReadFile(openFileDialog.FileName);
                        textBox.Text = content;
                        currentFilePath = openFileDialog.FileName;
                        hasChanges = false;
                        UpdateFormTitle();
                        DocumentOpened?.Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии файла: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool SaveDocument()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                return SaveDocumentAs();
            }
            else
            {
                return SaveFile(currentFilePath);
            }
        }

        public bool SaveDocumentAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = fileService.GetFileFilter();
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bool result = SaveFile(saveFileDialog.FileName);
                    if (result)
                    {
                        currentFilePath = saveFileDialog.FileName;
                        UpdateFormTitle();
                    }
                    return result;
                }
                return false;
            }
        }

        private bool SaveFile(string filePath)
        {
            try
            {
                fileService.WriteFile(filePath, textBox.Text);
                hasChanges = false;
                UpdateFormTitle();
                DocumentSaved?.Invoke(this, EventArgs.Empty);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool PromptSaveChanges()
        {
            if (hasChanges)
            {
                DialogResult result = MessageBox.Show(
                    "Сохранить изменения в файле?",
                    "Сохранение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    return SaveDocument();
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetHasChanges(bool changes)
        {
            hasChanges = changes;
            UpdateFormTitle();
        }

        private void UpdateFormTitle()
        {
            string fileName = string.IsNullOrEmpty(currentFilePath) ?
                "Новый документ" : Path.GetFileName(currentFilePath);
            mainForm.UpdateFormTitle($"lab1 - {fileName}{(hasChanges ? "*" : "")}");
        }
    }
}