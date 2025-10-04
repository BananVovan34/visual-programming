namespace visualprogramming.Lab7
{
    partial class LengthDBForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            showAllButton = new Button();
            filterLessThanKm = new Button();
            filterMoreThanMile = new Button();
            groupButton = new Button();
            minMaxButton = new Button();
            sortButton = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(326, 424);
            listBox1.TabIndex = 0;
            // 
            // showAllButton
            // 
            showAllButton.Location = new Point(344, 12);
            showAllButton.Name = "showAllButton";
            showAllButton.Size = new Size(181, 23);
            showAllButton.TabIndex = 1;
            showAllButton.Text = "Показать всё";
            showAllButton.UseVisualStyleBackColor = true;
            showAllButton.Click += showAllButton_Click;
            // 
            // filterLessThanKm
            // 
            filterLessThanKm.Location = new Point(344, 70);
            filterLessThanKm.Name = "filterLessThanKm";
            filterLessThanKm.Size = new Size(181, 23);
            filterLessThanKm.TabIndex = 2;
            filterLessThanKm.Text = "Фильтр < 1 км";
            filterLessThanKm.UseVisualStyleBackColor = true;
            filterLessThanKm.Click += filterLessThanKm_Click;
            // 
            // filterMoreThanMile
            // 
            filterMoreThanMile.Location = new Point(344, 99);
            filterMoreThanMile.Name = "filterMoreThanMile";
            filterMoreThanMile.Size = new Size(181, 23);
            filterMoreThanMile.TabIndex = 3;
            filterMoreThanMile.Text = "Фильтр > 1 миля";
            filterMoreThanMile.UseVisualStyleBackColor = true;
            filterMoreThanMile.Click += filterMoreThanMile_Click;
            // 
            // groupButton
            // 
            groupButton.Location = new Point(344, 128);
            groupButton.Name = "groupButton";
            groupButton.Size = new Size(181, 23);
            groupButton.TabIndex = 4;
            groupButton.Text = "Группировка";
            groupButton.UseVisualStyleBackColor = true;
            groupButton.Click += groupButton_Click;
            // 
            // minMaxButton
            // 
            minMaxButton.Location = new Point(344, 157);
            minMaxButton.Name = "minMaxButton";
            minMaxButton.Size = new Size(181, 23);
            minMaxButton.TabIndex = 5;
            minMaxButton.Text = "Мин/Макс";
            minMaxButton.UseVisualStyleBackColor = true;
            minMaxButton.Click += minMaxButton_Click;
            // 
            // sortButton
            // 
            sortButton.BackgroundImageLayout = ImageLayout.None;
            sortButton.Location = new Point(344, 41);
            sortButton.Name = "sortButton";
            sortButton.Size = new Size(181, 23);
            sortButton.TabIndex = 6;
            sortButton.Text = "Сортировать по возрастанию";
            sortButton.UseVisualStyleBackColor = true;
            sortButton.Click += sortButton_Click;
            // 
            // LengthDBForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 450);
            Controls.Add(sortButton);
            Controls.Add(minMaxButton);
            Controls.Add(groupButton);
            Controls.Add(filterMoreThanMile);
            Controls.Add(filterLessThanKm);
            Controls.Add(showAllButton);
            Controls.Add(listBox1);
            Name = "LengthDBForm";
            Text = "LengthDBForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button showAllButton;
        private Button filterLessThanKm;
        private Button filterMoreThanMile;
        private Button groupButton;
        private Button minMaxButton;
        private Button sortButton;
    }
}