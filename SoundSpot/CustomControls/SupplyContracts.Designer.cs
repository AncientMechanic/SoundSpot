namespace SoundSpot
{
    partial class SupplyContracts
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
            button3 = new Button();
            comboBox1 = new ComboBox();
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
            button2.Text = "+ Add";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
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
            ClientsGridView.CellContentClick += ClientsGridView_CellContentClick;
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
            label3.Location = new Point(262, 0);
            label3.MinimumSize = new Size(200, 50);
            label3.Name = "label3";
            label3.Size = new Size(291, 50);
            label3.TabIndex = 8;
            label3.Text = "Supply Contracts";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.BackColor = Color.Peru;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = SystemColors.ControlText;
            button3.Location = new Point(617, 456);
            button3.Name = "button3";
            button3.Size = new Size(85, 32);
            button3.TabIndex = 12;
            button3.Text = "Save As";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Оплачено", "Не оплачено", "Все" });
            comboBox1.Location = new Point(656, 407);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 13;
            comboBox1.Text = "Все";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // SupplyContracts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.NavajoWhite;
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(ClientsGridView);
            Controls.Add(button1);
            Controls.Add(label3);
            Name = "SupplyContracts";
            Size = new Size(815, 515);
            Load += SupplyContracts_Load;
            VisibleChanged += SupplyContracts_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)ClientsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private DataGridView ClientsGridView;
        private Button button1;
        private Label label3;
        private Button button3;
        private ComboBox comboBox1;
    }
}
