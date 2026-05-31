namespace lab1.Forms
{
    partial class HelpForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox richTextBoxHelp;
        private System.Windows.Forms.Button buttonClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.richTextBoxHelp = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // richTextBoxHelp
            this.richTextBoxHelp.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxHelp.Name = "richTextBoxHelp";
            this.richTextBoxHelp.ReadOnly = true;
            this.richTextBoxHelp.Size = new System.Drawing.Size(560, 400);
            this.richTextBoxHelp.TabIndex = 0;
            this.richTextBoxHelp.Text = "СПРАВКА ПО ПРОГРАММЕ\n" +
                "====================\n\n" +
                "МЕНЮ \"ФАЙЛ\":\n" +
                "- Создать (Ctrl+N) - создание нового документа\n" +
                "- Открыть (Ctrl+O) - открытие существующего файла\n" +
                "- Сохранить (Ctrl+S) - сохранение текущего документа\n" +
                "- Сохранить как - сохранение документа под новым именем\n" +
                "- Выход (Alt+F4) - выход из программы\n\n" +
                "МЕНЮ \"ПРАВКА\":\n" +
                "- Отменить (Ctrl+Z) - отмена последнего действия\n" +
                "- Повторить (Ctrl+Y) - повтор отмененного действия\n" +
                "- Вырезать (Ctrl+X) - вырезать выделенный текст\n" +
                "- Копировать (Ctrl+C) - копировать выделенный текст\n" +
                "- Вставить (Ctrl+V) - вставить текст из буфера\n" +
                "- Удалить (Del) - удалить выделенный текст\n" +
                "- Выделить все (Ctrl+A) - выделить весь текст\n\n" +
                "МЕНЮ \"СПРАВКА\":\n" +
                "- Вызов справки (F1) - открыть данное окно справки\n" +
                "- О программе - информация о программе\n\n" +
                "ПАНЕЛЬ ИНСТРУМЕНТОВ:\n" +
                "Кнопки на панели инструментов дублируют основные функции меню.";

            // buttonClose
            this.buttonClose.Location = new System.Drawing.Point(250, 420);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 30);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);

            // HelpForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.richTextBoxHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справка";
            this.ResumeLayout(false);
        }

        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}