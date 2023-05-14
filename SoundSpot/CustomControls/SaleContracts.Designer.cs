namespace SoundSpot
{
    partial class SaleContracts
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
            button2 = new Button();
            ClientsGridView = new DataGridView();
            button1 = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)ClientsGridView).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.BackColor = Color.Peru;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = SystemColors.ControlText;
            button2.Location = new Point(708, 456);
            button2.Name = "button2";
            button2.Size = new Size(85, 32);
            button2.TabIndex = 11;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = false;
            // 
            // ClientsGridView
            // 
            ClientsGridView.Anchor = AnchorStyles.None;
            ClientsGridView.BackgroundColor = SystemColors.Window;
            ClientsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ClientsGridView.Location = new Point(21, 67);
            ClientsGridView.Name = "ClientsGridView";
            ClientsGridView.RowTemplate.Height = 25;
            ClientsGridView.Size = new Size(772, 383);
            ClientsGridView.TabIndex = 10;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.Peru;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(21, 16);
            button1.Name = "button1";
            button1.Size = new Size(85, 32);
            button1.TabIndex = 9;
            button1.Text = "< Back";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = Color.NavajoWhite;
            label3.Font = new Font("Verdana", 21.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label3.Location = new Point(283, 0);
            label3.MinimumSize = new Size(200, 50);
            label3.Name = "label3";
            label3.Size = new Size(249, 50);
            label3.TabIndex = 8;
            label3.Text = "Sale Contracts";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SaleContracts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            Controls.Add(button2);
            Controls.Add(ClientsGridView);
            Controls.Add(button1);
            Controls.Add(label3);
            Name = "SaleContracts";
            Size = new Size(815, 515);
            ((System.ComponentModel.ISupportInitialize)ClientsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private DataGridView ClientsGridView;
        private Button button1;
        private Label label3;
    }
}
