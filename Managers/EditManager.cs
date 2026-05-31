using lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab1.Managers
{
    public class EditManager
    {
        private RichTextBox textBox;
        private Stack<EditAction> undoStack = new Stack<EditAction>();
        private Stack<EditAction> redoStack = new Stack<EditAction>();
        private bool isProcessingUndoRedo = false;
        private ClipboardManager clipboardManager;

        public EditManager(RichTextBox textBox)
        {
            this.textBox = textBox;
            this.clipboardManager = new ClipboardManager();
            this.textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isProcessingUndoRedo)
            {
                SaveStateForUndo();
                redoStack.Clear();
            }
        }

        public void SaveStateForUndo()
        {
            EditAction action = new EditAction
            {
                Text = textBox.Text,
                SelectionStart = textBox.SelectionStart,
                SelectionLength = textBox.SelectionLength,
                ActionType = ActionType.TextChange
            };
            undoStack.Push(action);

            if (undoStack.Count > 100)
            {
                var list = undoStack.ToList();
                list.RemoveAt(0);
                undoStack = new Stack<EditAction>(list);
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                isProcessingUndoRedo = true;

                EditAction action = undoStack.Pop();

                EditAction redoAction = new EditAction
                {
                    Text = textBox.Text,
                    SelectionStart = textBox.SelectionStart,
                    SelectionLength = textBox.SelectionLength,
                    ActionType = action.ActionType
                };

                textBox.Text = action.Text;
                textBox.SelectionStart = action.SelectionStart;
                textBox.SelectionLength = action.SelectionLength;

                redoStack.Push(redoAction);

                isProcessingUndoRedo = false;
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                isProcessingUndoRedo = true;

                EditAction action = redoStack.Pop();

                EditAction undoAction = new EditAction
                {
                    Text = textBox.Text,
                    SelectionStart = textBox.SelectionStart,
                    SelectionLength = textBox.SelectionLength,
                    ActionType = action.ActionType
                };

                textBox.Text = action.Text;
                textBox.SelectionStart = action.SelectionStart;
                textBox.SelectionLength = action.SelectionLength;

                undoStack.Push(undoAction);

                isProcessingUndoRedo = false;
            }
        }

        public void Cut()
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                clipboardManager.CutText(ref textBox);
                redoStack.Clear();
            }
        }

        public void Copy()
        {
            clipboardManager.CopyText(textBox);
        }

        public void Paste()
        {
            clipboardManager.PasteText(textBox);
            redoStack.Clear();
        }

        public void Delete()
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                int selectionStart = textBox.SelectionStart;
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.SelectionStart = selectionStart;
                redoStack.Clear();
            }
        }

        public void SelectAll()
        {
            textBox.SelectAll();
        }

        public bool CanUndo => undoStack.Count > 0;
        public bool CanRedo => redoStack.Count > 0;
        public bool CanCut => !string.IsNullOrEmpty(textBox.SelectedText);
        public bool CanCopy => !string.IsNullOrEmpty(textBox.SelectedText);
        public bool CanPaste => clipboardManager.CanPaste();
        public bool CanDelete => !string.IsNullOrEmpty(textBox.SelectedText);
    }
}