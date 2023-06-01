namespace SoundSpot.CustomForms
{
    partial class EditShipOrder
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
            label4 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label6 = new Label();
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(60, 254);
            label4.Name = "label4";
            label4.Size = new Size(108, 18);
            label4.TabIndex = 31;
            label4.Text = "Instrument:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(113, 156);
            label3.Name = "label3";
            label3.Size = new Size(55, 18);
            label3.TabIndex = 30;
            label3.Text = "Total:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(174, 156);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(180, 23);
            textBox2.TabIndex = 29;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(24, 206);
            label2.Name = "label2";
            label2.Size = new Size(144, 18);
            label2.TabIndex = 28;
            label2.Text = "Supply Contract:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(89, 109);
            label1.Name = "label1";
            label1.Size = new Size(79, 18);
            label1.TabIndex = 27;
            label1.Text = "Amount:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(174, 109);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(180, 23);
            textBox1.TabIndex = 26;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(174, 206);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(180, 23);
            textBox3.TabIndex = 32;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(174, 254);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(180, 23);
            textBox4.TabIndex = 33;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(174, 60);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(180, 23);
            textBox5.TabIndex = 71;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(82, 60);
            label6.Name = "label6";
            label6.Size = new Size(86, 18);
            label6.TabIndex = 70;
            label6.Text = "Order ID:";
            // 
            // EditShipOrder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            ClientSize = new Size(399, 411);
            Controls.Add(textBox5);
            Controls.Add(label6);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnOK);
            Name = "EditShipOrder";
            Text = "EditShipOrder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnOK;
        private Label label4;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label6;
    }
}