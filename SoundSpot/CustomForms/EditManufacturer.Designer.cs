namespace SoundSpot.CustomForms
{
    partial class EditManufacturer
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
            textBox4 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            textBox5 = new TextBox();
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
            // textBox4
            // 
            textBox4.Location = new Point(157, 180);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(180, 23);
            textBox4.TabIndex = 43;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(26, 229);
            label4.Name = "label4";
            label4.Size = new Size(125, 18);
            label4.TabIndex = 42;
            label4.Text = "Bank Account:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(157, 130);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(180, 23);
            textBox3.TabIndex = 41;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(72, 180);
            label3.Name = "label3";
            label3.Size = new Size(79, 18);
            label3.TabIndex = 40;
            label3.Text = "Director:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(157, 77);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(180, 23);
            textBox2.TabIndex = 39;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(70, 130);
            label5.Name = "label5";
            label5.Size = new Size(81, 18);
            label5.TabIndex = 38;
            label5.Text = "Address:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(89, 77);
            label6.Name = "label6";
            label6.Size = new Size(62, 18);
            label6.TabIndex = 37;
            label6.Text = "Name:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(157, 229);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(180, 23);
            textBox5.TabIndex = 36;
            // 
            // EditManufacturer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            ClientSize = new Size(399, 411);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(textBox5);
            Controls.Add(btnOK);
            Name = "EditManufacturer";
            Text = "EditManufacturer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnOK;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label5;
        private Label label6;
        private TextBox textBox5;
    }
}