namespace visualprogramming.Lab6
{
    partial class PlayingCard
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
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            comboBox2 = new ComboBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "6", "7", "8", "9", "10", "J", "Q", "K", "A" });
            comboBox1.Location = new Point(611, 41);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(177, 23);
            comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(611, 23);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "Rank";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(611, 74);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 3;
            label2.Text = "Suit";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Spades", "Hearts", "Diamonds", "Clubs" });
            comboBox2.Location = new Point(611, 92);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(177, 23);
            comboBox2.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(611, 137);
            button1.Name = "button1";
            button1.Size = new Size(177, 23);
            button1.TabIndex = 4;
            button1.Text = "Add card";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PlayingCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(comboBox2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Name = "PlayingCard";
            Text = "PlayingCard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private ComboBox comboBox2;
        private Button button1;
    }
}