namespace SoundSpot
{
    partial class MainMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            label4 = new Label();
            label5 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button8 = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.BackColor = Color.Black;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(17, 72);
            label1.Name = "label1";
            label1.Size = new Size(650, 2);
            label1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.BackColor = Color.Black;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(17, 447);
            label2.Name = "label2";
            label2.Size = new Size(650, 2);
            label2.TabIndex = 2;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.NavajoWhite;
            label3.Font = new Font("Verdana", 21.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label3.Location = new Point(242, 0);
            label3.MinimumSize = new Size(200, 50);
            label3.Name = "label3";
            label3.Size = new Size(200, 50);
            label3.TabIndex = 3;
            label3.Text = "Main Menu";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.Peru;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(17, 11);
            button1.Name = "button1";
            button1.Size = new Size(85, 32);
            button1.TabIndex = 4;
            button1.Text = "< Back";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.BackColor = Color.Black;
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(17, 83);
            label4.Name = "label4";
            label4.Size = new Size(2, 350);
            label4.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.BackColor = Color.Black;
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(665, 83);
            label5.Name = "label5";
            label5.Size = new Size(2, 350);
            label5.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button7, 0, 5);
            tableLayoutPanel1.Controls.Add(button6, 0, 4);
            tableLayoutPanel1.Controls.Add(button5, 0, 3);
            tableLayoutPanel1.Controls.Add(button4, 0, 2);
            tableLayoutPanel1.Controls.Add(button3, 0, 1);
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Location = new Point(25, 77);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.Size = new Size(634, 367);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top;
            button7.BackColor = Color.Peru;
            button7.FlatStyle = FlatStyle.Popup;
            button7.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(120, 308);
            button7.Name = "button7";
            button7.Size = new Size(394, 47);
            button7.TabIndex = 8;
            button7.Text = "Contracts";
            button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top;
            button6.BackColor = Color.Peru;
            button6.FlatStyle = FlatStyle.Popup;
            button6.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(120, 247);
            button6.Name = "button6";
            button6.Size = new Size(394, 47);
            button6.TabIndex = 4;
            button6.Text = "Instruments";
            button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top;
            button5.BackColor = Color.Peru;
            button5.FlatStyle = FlatStyle.Popup;
            button5.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(120, 186);
            button5.Name = "button5";
            button5.Size = new Size(394, 47);
            button5.TabIndex = 3;
            button5.Text = "Suppliers";
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top;
            button4.BackColor = Color.Peru;
            button4.FlatStyle = FlatStyle.Popup;
            button4.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(120, 125);
            button4.Name = "button4";
            button4.Size = new Size(394, 47);
            button4.TabIndex = 2;
            button4.Text = "Manufacturers";
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top;
            button3.BackColor = Color.Peru;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(120, 64);
            button3.Name = "button3";
            button3.Size = new Size(394, 47);
            button3.TabIndex = 1;
            button3.Text = "Sellers";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top;
            button2.BackColor = Color.Peru;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(120, 3);
            button2.Name = "button2";
            button2.Size = new Size(394, 47);
            button2.TabIndex = 0;
            button2.Text = "Clients";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.None;
            button8.BackColor = Color.Peru;
            button8.FlatStyle = FlatStyle.Popup;
            button8.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button8.ForeColor = SystemColors.ControlText;
            button8.Location = new Point(582, 476);
            button8.Name = "button8";
            button8.Size = new Size(85, 26);
            button8.TabIndex = 8;
            button8.Text = "Exit";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            Controls.Add(button8);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainMenu";
            Size = new Size(685, 515);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Label label4;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button8;
    }
}
