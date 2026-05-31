namespace lab1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblErrorCount;
        private System.Windows.Forms.ToolStripMenuItem showTACMenuItem;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblErrorCount = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.saveHowStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.editStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.undoStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.redoStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.cutStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.copyStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.textStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.taskDescStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.grammarStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.gramClassStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.parsingMethod = new System.Windows.Forms.ToolStripMenuItem();
            this.testStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.literatStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.codeStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.startStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.infoStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.infoShowStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.createToolStrip = new System.Windows.Forms.ToolStripButton();
            this.browseToolStrip = new System.Windows.Forms.ToolStripButton();
            this.saveToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStrip = new System.Windows.Forms.ToolStripButton();
            this.redoToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStrip = new System.Windows.Forms.ToolStripButton();
            this.cutToolStrip = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.runToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStrip = new System.Windows.Forms.ToolStripButton();
            this.infoToolStrip = new System.Windows.Forms.ToolStripButton();
            this.btnShowAST = new System.Windows.Forms.ToolStripButton();
            this.btnShowASTGraph = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnShowTAC = new System.Windows.Forms.ToolStripButton();
            this.toolStripTAC = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.Location = new System.Drawing.Point(12, 262);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(156, 13);
            this.lblErrorCount.TabIndex = 17;
            this.lblErrorCount.Text = "Общее количество ошибок: 0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 106);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 150);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStrip,
            this.editStrip,
            this.textStrip,
            this.startStrip,
            this.infoStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileStrip
            // 
            this.fileStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFileStrip,
            this.openFileStrip,
            this.saveFileStrip,
            this.saveHowStrip,
            this.exitStrip});
            this.fileStrip.Name = "fileStrip";
            this.fileStrip.Size = new System.Drawing.Size(48, 20);
            this.fileStrip.Text = "Файл";
            // 
            // createFileStrip
            // 
            this.createFileStrip.Name = "createFileStrip";
            this.createFileStrip.Size = new System.Drawing.Size(153, 22);
            this.createFileStrip.Text = "Создать";
            this.createFileStrip.Click += new System.EventHandler(this.createFileStrip_Click);
            // 
            // openFileStrip
            // 
            this.openFileStrip.Name = "openFileStrip";
            this.openFileStrip.Size = new System.Drawing.Size(153, 22);
            this.openFileStrip.Text = "Открыть";
            this.openFileStrip.Click += new System.EventHandler(this.openFileStrip_Click);
            // 
            // saveFileStrip
            // 
            this.saveFileStrip.Name = "saveFileStrip";
            this.saveFileStrip.Size = new System.Drawing.Size(153, 22);
            this.saveFileStrip.Text = "Сохранить";
            this.saveFileStrip.Click += new System.EventHandler(this.saveFileStrip_Click);
            // 
            // saveHowStrip
            // 
            this.saveHowStrip.Name = "saveHowStrip";
            this.saveHowStrip.Size = new System.Drawing.Size(153, 22);
            this.saveHowStrip.Text = "Сохранить как";
            this.saveHowStrip.Click += new System.EventHandler(this.saveHowStrip_Click);
            // 
            // exitStrip
            // 
            this.exitStrip.Name = "exitStrip";
            this.exitStrip.Size = new System.Drawing.Size(153, 22);
            this.exitStrip.Text = "Выход";
            this.exitStrip.Click += new System.EventHandler(this.exitStrip_Click);
            // 
            // editStrip
            // 
            this.editStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoStrip,
            this.redoStrip,
            this.cutStrip,
            this.copyStrip,
            this.pasteStrip,
            this.deleteStrip,
            this.selectAllStrip});
            this.editStrip.Name = "editStrip";
            this.editStrip.Size = new System.Drawing.Size(59, 20);
            this.editStrip.Text = "Правка";
            // 
            // undoStrip
            // 
            this.undoStrip.Name = "undoStrip";
            this.undoStrip.Size = new System.Drawing.Size(148, 22);
            this.undoStrip.Text = "Отменить";
            this.undoStrip.Click += new System.EventHandler(this.undoStrip_Click);
            // 
            // redoStrip
            // 
            this.redoStrip.Name = "redoStrip";
            this.redoStrip.Size = new System.Drawing.Size(148, 22);
            this.redoStrip.Text = "Повторить";
            this.redoStrip.Click += new System.EventHandler(this.redoStrip_Click);
            // 
            // cutStrip
            // 
            this.cutStrip.Name = "cutStrip";
            this.cutStrip.Size = new System.Drawing.Size(148, 22);
            this.cutStrip.Text = "Вырезать";
            this.cutStrip.Click += new System.EventHandler(this.cutStrip_Click);
            // 
            // copyStrip
            // 
            this.copyStrip.Name = "copyStrip";
            this.copyStrip.Size = new System.Drawing.Size(148, 22);
            this.copyStrip.Text = "Копировать";
            this.copyStrip.Click += new System.EventHandler(this.copyStrip_Click);
            // 
            // pasteStrip
            // 
            this.pasteStrip.Name = "pasteStrip";
            this.pasteStrip.Size = new System.Drawing.Size(148, 22);
            this.pasteStrip.Text = "Вставить";
            this.pasteStrip.Click += new System.EventHandler(this.pasteStrip_Click);
            // 
            // deleteStrip
            // 
            this.deleteStrip.Name = "deleteStrip";
            this.deleteStrip.Size = new System.Drawing.Size(148, 22);
            this.deleteStrip.Text = "Удалить";
            this.deleteStrip.Click += new System.EventHandler(this.deleteStrip_Click);
            // 
            // selectAllStrip
            // 
            this.selectAllStrip.Name = "selectAllStrip";
            this.selectAllStrip.Size = new System.Drawing.Size(148, 22);
            this.selectAllStrip.Text = "Выделить все";
            this.selectAllStrip.Click += new System.EventHandler(this.selectAllStrip_Click);
            // 
            // textStrip
            // 
            this.textStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskDescStrip,
            this.grammarStrip,
            this.gramClassStrip,
            this.parsingMethod,
            this.testStrip,
            this.literatStrip,
            this.codeStrip});
            this.textStrip.Name = "textStrip";
            this.textStrip.Size = new System.Drawing.Size(49, 20);
            this.textStrip.Text = "Текст";
            // 
            // taskDescStrip
            // 
            this.taskDescStrip.Name = "taskDescStrip";
            this.taskDescStrip.Size = new System.Drawing.Size(222, 22);
            this.taskDescStrip.Text = "Постановка задачи";
            this.taskDescStrip.Click += new System.EventHandler(this.taskDescStrip_Click);
            // 
            // grammarStrip
            // 
            this.grammarStrip.Name = "grammarStrip";
            this.grammarStrip.Size = new System.Drawing.Size(222, 22);
            this.grammarStrip.Text = "Грамматика";
            this.grammarStrip.Click += new System.EventHandler(this.grammarStrip_Click);
            // 
            // gramClassStrip
            // 
            this.gramClassStrip.Name = "gramClassStrip";
            this.gramClassStrip.Size = new System.Drawing.Size(222, 22);
            this.gramClassStrip.Text = "Классификация граматики";
            this.gramClassStrip.Click += new System.EventHandler(this.gramClassStrip_Click);
            // 
            // parsingMethod
            // 
            this.parsingMethod.Name = "parsingMethod";
            this.parsingMethod.Size = new System.Drawing.Size(222, 22);
            this.parsingMethod.Text = "Метод анализа";
            this.parsingMethod.Click += new System.EventHandler(this.parsingMethod_Click);
            // 
            // testStrip
            // 
            this.testStrip.Name = "testStrip";
            this.testStrip.Size = new System.Drawing.Size(222, 22);
            this.testStrip.Text = "Тестовый пример";
            this.testStrip.Click += new System.EventHandler(this.testStrip_Click);
            // 
            // literatStrip
            // 
            this.literatStrip.Name = "literatStrip";
            this.literatStrip.Size = new System.Drawing.Size(222, 22);
            this.literatStrip.Text = "Список литературы";
            this.literatStrip.Click += new System.EventHandler(this.literatStrip_Click);
            // 
            // codeStrip
            // 
            this.codeStrip.Name = "codeStrip";
            this.codeStrip.Size = new System.Drawing.Size(222, 22);
            this.codeStrip.Text = "Исходный код программы";
            this.codeStrip.Click += new System.EventHandler(this.codeStrip_Click);
            // 
            // startStrip
            // 
            this.startStrip.Name = "startStrip";
            this.startStrip.Size = new System.Drawing.Size(46, 20);
            this.startStrip.Text = "Пуск";
            this.startStrip.Click += new System.EventHandler(this.startStrip_Click);
            this.showTACMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTACMenuItem.Name = "showTACMenuItem";
            this.showTACMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showTACMenuItem.Text = "Показать TAC";
            this.showTACMenuItem.Click += new System.EventHandler(this.showTACMenuItem_Click);
            this.startStrip.DropDownItems.Add(this.showTACMenuItem);
            // 
            // infoStrip
            // 
            this.infoStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoShowStrip,
            this.aboutStrip});
            this.infoStrip.Name = "infoStrip";
            this.infoStrip.Size = new System.Drawing.Size(65, 20);
            this.infoStrip.Text = "Справка";
            // 
            // infoShowStrip
            // 
            this.infoShowStrip.Name = "infoShowStrip";
            this.infoShowStrip.Size = new System.Drawing.Size(156, 22);
            this.infoShowStrip.Text = "Вызов справки";
            this.infoShowStrip.Click += new System.EventHandler(this.infoShowStrip_Click);
            // 
            // aboutStrip
            // 
            this.aboutStrip.Name = "aboutStrip";
            this.aboutStrip.Size = new System.Drawing.Size(156, 22);
            this.aboutStrip.Text = "О программе";
            this.aboutStrip.Click += new System.EventHandler(this.aboutStrip_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStrip,
            this.browseToolStrip,
            this.saveToolStrip,
            this.toolStripSeparator1,
            this.undoToolStrip,
            this.redoToolStrip,
            this.toolStripSeparator2,
            this.copyToolStrip,
            this.cutToolStrip,
            this.pasteToolStrip,
            this.toolStripSeparator4,
            this.runToolStrip,
            this.toolStripSeparator3,
            this.aboutToolStrip,
            this.infoToolStrip,
            this.toolStripTAC,
            this.btnShowAST,
            this.btnShowASTGraph});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // createToolStrip
            // 
            this.createToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createToolStrip.Image = global::lab1.Properties.Resources.icons8_плюс_30;
            this.createToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createToolStrip.Name = "createToolStrip";
            this.createToolStrip.Size = new System.Drawing.Size(23, 22);
            this.createToolStrip.Text = "toolStripButton1";
            this.createToolStrip.Click += new System.EventHandler(this.createToolStrip_Click);
            // 
            // browseToolStrip
            // 
            this.browseToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browseToolStrip.Image = global::lab1.Properties.Resources.icons8_папка_30;
            this.browseToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browseToolStrip.Name = "browseToolStrip";
            this.browseToolStrip.Size = new System.Drawing.Size(23, 22);
            this.browseToolStrip.Text = "browseToolStrip";
            this.browseToolStrip.Click += new System.EventHandler(this.browseToolStrip_Click);
            // 
            // saveToolStrip
            // 
            this.saveToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStrip.Image = global::lab1.Properties.Resources.icons8_дискета_30;
            this.saveToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStrip.Name = "saveToolStrip";
            this.saveToolStrip.Size = new System.Drawing.Size(23, 22);
            this.saveToolStrip.Text = "saveToolStrip";
            this.saveToolStrip.Click += new System.EventHandler(this.saveToolStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // undoToolStrip
            // 
            this.undoToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoToolStrip.Image = global::lab1.Properties.Resources.icons8_налево_30;
            this.undoToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStrip.Name = "undoToolStrip";
            this.undoToolStrip.Size = new System.Drawing.Size(23, 22);
            this.undoToolStrip.Text = "toolStripButton4";
            this.undoToolStrip.ToolTipText = "undoToolStrip";
            this.undoToolStrip.Click += new System.EventHandler(this.undoToolStrip_Click);
            // 
            // redoToolStrip
            // 
            this.redoToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoToolStrip.Image = global::lab1.Properties.Resources.icons8_направо_30;
            this.redoToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoToolStrip.Name = "redoToolStrip";
            this.redoToolStrip.Size = new System.Drawing.Size(23, 22);
            this.redoToolStrip.Text = "toolStripButton5";
            this.redoToolStrip.Click += new System.EventHandler(this.redoToolStrip_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // copyToolStrip
            // 
            this.copyToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStrip.Image = global::lab1.Properties.Resources.icons8_скопировать_30;
            this.copyToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStrip.Name = "copyToolStrip";
            this.copyToolStrip.Size = new System.Drawing.Size(23, 22);
            this.copyToolStrip.Text = "toolStripButton6";
            this.copyToolStrip.Click += new System.EventHandler(this.copyToolStrip_Click);
            // 
            // cutToolStrip
            // 
            this.cutToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStrip.Image = global::lab1.Properties.Resources.icons8_вырезать_30;
            this.cutToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStrip.Name = "cutToolStrip";
            this.cutToolStrip.Size = new System.Drawing.Size(23, 22);
            this.cutToolStrip.Text = "toolStripButton7";
            this.cutToolStrip.Click += new System.EventHandler(this.cutToolStrip_Click);
            // 
            // pasteToolStrip
            // 
            this.pasteToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStrip.Image = global::lab1.Properties.Resources.icons8_вставить_30;
            this.pasteToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStrip.Name = "pasteToolStrip";
            this.pasteToolStrip.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStrip.Text = "toolStripButton8";
            this.pasteToolStrip.Click += new System.EventHandler(this.pasteToolStrip_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // runToolStrip
            // 
            this.runToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runToolStrip.Image = global::lab1.Properties.Resources.icons8_начало_30;
            this.runToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runToolStrip.Name = "runToolStrip";
            this.runToolStrip.Size = new System.Drawing.Size(23, 22);
            this.runToolStrip.Text = "toolStripButton9";
            this.runToolStrip.Click += new System.EventHandler(this.runToolStrip_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // aboutToolStrip
            // 
            this.aboutToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutToolStrip.Image = global::lab1.Properties.Resources.icons8_вопрос_30;
            this.aboutToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStrip.Name = "aboutToolStrip";
            this.aboutToolStrip.Size = new System.Drawing.Size(23, 22);
            this.aboutToolStrip.Text = "toolStripButton10";
            this.aboutToolStrip.Click += new System.EventHandler(this.aboutToolStrip_Click);
            // 
            // infoToolStrip
            // 
            this.infoToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.infoToolStrip.Image = global::lab1.Properties.Resources.icons8_информация_30;
            this.infoToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoToolStrip.Name = "infoToolStrip";
            this.infoToolStrip.Size = new System.Drawing.Size(23, 22);
            this.infoToolStrip.Text = "toolStripButton11";
            this.infoToolStrip.Click += new System.EventHandler(this.infoToolStrip_Click);
            // 
            // btnShowAST
            // 
            this.btnShowAST.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAST.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAST.Image")));
            this.btnShowAST.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAST.Name = "btnShowAST";
            this.btnShowAST.Size = new System.Drawing.Size(23, 22);
            this.btnShowAST.Text = "Показать AST";
            this.btnShowAST.ToolTipText = "Показать абстрактное синтаксическое дерево";
            this.btnShowAST.Click += new System.EventHandler(this.btnShowAST_Click);
            // 
            // btnShowASTGraph
            // 
            this.btnShowASTGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowASTGraph.Image = ((System.Drawing.Image)(resources.GetObject("btnShowASTGraph.Image")));
            this.btnShowASTGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowASTGraph.Name = "btnShowASTGraph";
            this.btnShowASTGraph.Size = new System.Drawing.Size(23, 22);
            this.btnShowASTGraph.Text = "AST (граф)";
            this.btnShowASTGraph.ToolTipText = "Показать графическое дерево AST";
            this.btnShowASTGraph.Click += new System.EventHandler(this.btnShowASTGraph_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MessageColumn,
            this.LineColumn,
            this.PositionColumn});
            this.dataGridView1.Location = new System.Drawing.Point(13, 284);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(775, 154);
            this.dataGridView1.TabIndex = 16;
            // 
            // MessageColumn
            // 
            this.MessageColumn.HeaderText = "Сообщение";
            this.MessageColumn.Name = "MessageColumn";
            this.MessageColumn.ReadOnly = true;
            // 
            // LineColumn
            // 
            this.LineColumn.HeaderText = "Строка";
            this.LineColumn.Name = "LineColumn";
            this.LineColumn.ReadOnly = true;
            // 
            // PositionColumn
            // 
            this.PositionColumn.HeaderText = "Позиция";
            this.PositionColumn.Name = "PositionColumn";
            this.PositionColumn.ReadOnly = true;
            // 
            // btnShowTAC
            // 
            this.btnShowTAC.Image = global::lab1.Properties.Resources.icons8_информация_30;
            this.btnShowTAC.Name = "btnShowTAC";
            this.btnShowTAC.Size = new System.Drawing.Size(50, 22);
            this.btnShowTAC.Text = "TAC";
            this.btnShowTAC.ToolTipText = "Показать TAC и оптимизации";
            this.btnShowTAC.Click += new System.EventHandler(this.btnShowTAC_Click);
            // 
            // toolStripTAC
            // 
            this.toolStripTAC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTAC.Image = ((System.Drawing.Image)(resources.GetObject("toolStripTAC.Image")));
            this.toolStripTAC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTAC.Name = "toolStripTAC";
            this.toolStripTAC.Size = new System.Drawing.Size(23, 22);
            this.toolStripTAC.Text = "toolStripTAC";
            this.toolStripTAC.Click += new System.EventHandler(this.toolStripTAC_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblErrorCount);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "lab1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileStrip;
        private System.Windows.Forms.ToolStripMenuItem editStrip;
        private System.Windows.Forms.ToolStripMenuItem textStrip;
        private System.Windows.Forms.ToolStripMenuItem startStrip;
        private System.Windows.Forms.ToolStripMenuItem infoStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton createToolStrip;
        private System.Windows.Forms.ToolStripButton browseToolStrip;
        private System.Windows.Forms.ToolStripButton saveToolStrip;
        private System.Windows.Forms.ToolStripButton undoToolStrip;
        private System.Windows.Forms.ToolStripButton redoToolStrip;
        private System.Windows.Forms.ToolStripButton copyToolStrip;
        private System.Windows.Forms.ToolStripButton cutToolStrip;
        private System.Windows.Forms.ToolStripButton pasteToolStrip;
        private System.Windows.Forms.ToolStripButton runToolStrip;
        private System.Windows.Forms.ToolStripButton aboutToolStrip;
        private System.Windows.Forms.ToolStripButton infoToolStrip;
        private System.Windows.Forms.ToolStripMenuItem createFileStrip;
        private System.Windows.Forms.ToolStripMenuItem openFileStrip;
        private System.Windows.Forms.ToolStripMenuItem saveFileStrip;
        private System.Windows.Forms.ToolStripMenuItem saveHowStrip;
        private System.Windows.Forms.ToolStripMenuItem exitStrip;
        private System.Windows.Forms.ToolStripMenuItem undoStrip;
        private System.Windows.Forms.ToolStripMenuItem redoStrip;
        private System.Windows.Forms.ToolStripMenuItem cutStrip;
        private System.Windows.Forms.ToolStripMenuItem copyStrip;
        private System.Windows.Forms.ToolStripMenuItem pasteStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllStrip;
        private System.Windows.Forms.ToolStripMenuItem taskDescStrip;
        private System.Windows.Forms.ToolStripMenuItem grammarStrip;
        private System.Windows.Forms.ToolStripMenuItem gramClassStrip;
        private System.Windows.Forms.ToolStripMenuItem parsingMethod;
        private System.Windows.Forms.ToolStripMenuItem testStrip;
        private System.Windows.Forms.ToolStripMenuItem literatStrip;
        private System.Windows.Forms.ToolStripMenuItem codeStrip;
        private System.Windows.Forms.ToolStripMenuItem infoShowStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton btnShowAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositionColumn;
        private System.Windows.Forms.ToolStripButton btnShowASTGraph;
        private System.Windows.Forms.ToolStripButton btnShowTAC;
        private System.Windows.Forms.ToolStripButton toolStripTAC;
    }
}