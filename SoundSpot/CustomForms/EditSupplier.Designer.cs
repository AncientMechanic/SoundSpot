namespace SoundSpot.CustomForms
{
    partial class EditSupplier
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
            btnOK = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.None;
            btnOK.BackColor = Color.Peru;
            btnOK.FlatStyle = FlatStyle.Popup;
            btnOK.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.ForeColor = SystemColors.ControlText;
            btnOK.Location = new Point(293, 372);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 27);
            btnOK.TabIndex = 23;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(56, 169);
            label2.Name = "label2";
            label2.Size = new Size(81, 18);
            label2.TabIndex = 28;
            label2.Text = "Address:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(143, 169);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(180, 23);
            textBox2.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(75, 90);
            label1.Name = "label1";
            label1.Size = new Size(62, 18);
            label1.TabIndex = 26;
            label1.Text = "Name:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(143, 90);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(180, 23);
            textBox1.TabIndex = 25;
            // 
            // EditSupplier
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            ClientSize = new Size(399, 411);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnOK);
            Name = "EditSupplier";
            Text = "EditSupplier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnOK;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
    }
}