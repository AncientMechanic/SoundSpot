namespace SoundSpot
{
    partial class Contracts
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
            tableLayoutPanel1 = new TableLayoutPanel();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button3, 0, 1);
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Location = new Point(33, 150);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.3487549F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 57.6512451F));
            tableLayoutPanel1.Size = new Size(634, 281);
            tableLayoutPanel1.TabIndex = 19;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top;
            button3.BackColor = Color.Peru;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(120, 122);
            button3.Name = "button3";
            button3.Size = new Size(394, 106);
            button3.TabIndex = 1;
            button3.Text = "Supplied Products";
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
            button2.Size = new Size(394, 106);
            button2.TabIndex = 0;
            button2.Text = "Product Type";
            button2.UseVisualStyleBackColor = false;
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
            button1.TabIndex = 18;
            button1.Text = "< Back";
            button1.UseVisualStyleBackColor = false;
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
            label3.TabIndex = 17;
            label3.Text = "Contracts";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.BackColor = Color.Black;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(17, 434);
            label2.Name = "label2";
            label2.Size = new Size(650, 2);
            label2.TabIndex = 16;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.BackColor = Color.Black;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(17, 103);
            label1.Name = "label1";
            label1.Size = new Size(650, 2);
            label1.TabIndex = 15;
            // 
            // Contracts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Contracts";
            Size = new Size(685, 515);
            Load += Contracts_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button button3;
        private Button button2;
        private Button button1;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
