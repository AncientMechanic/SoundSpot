namespace SoundSpot.CustomForms
{
    partial class EditStorage
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
            textBox1 = new TextBox();
            comboBox1 = new TextBox();
            btnOK = new Button();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(144, 81);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(180, 23);
            textBox1.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.Location = new Point(144, 161);
            comboBox1.Name = "comboBox1";
            comboBox1.ReadOnly = true;
            comboBox1.Size = new Size(180, 23);
            comboBox1.TabIndex = 1;
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
            btnOK.TabIndex = 18;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(30, 161);
            label3.Name = "label3";
            label3.Size = new Size(108, 18);
            label3.TabIndex = 20;
            label3.Text = "Instrument:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(59, 81);
            label4.Name = "label4";
            label4.Size = new Size(79, 18);
            label4.TabIndex = 19;
            label4.Text = "Amount:";
            // 
            // EditStorage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            ClientSize = new Size(399, 411);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(btnOK);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Name = "EditStorage";
            Text = "EditStorage";
            Load += EditStorage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox comboBox1;
        private Button btnOK;
        private Label label3;
        private Label label4;
    }
}