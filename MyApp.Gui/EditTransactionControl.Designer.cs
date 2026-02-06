namespace MyApp.Gui
{
    partial class EditTransactionControl
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
            comboBox1 = new ComboBox();
            label2 = new Label();
            button1 = new Button();
            listViewTransactions = new ListView();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(250, 78);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 6;
            label1.Text = "Edit transactions";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Income", "Expense" });
            comboBox1.Location = new Point(583, 96);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(75, 23);
            comboBox1.TabIndex = 9;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(583, 78);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 10;
            label2.Text = "Options";
            // 
            // button1
            // 
            button1.Location = new Point(583, 246);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "Edit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnEdit_Click;
            // 
            // listViewTransactions
            // 
            listViewTransactions.Location = new Point(34, 96);
            listViewTransactions.Name = "listViewTransactions";
            listViewTransactions.Size = new Size(543, 202);
            listViewTransactions.TabIndex = 12;
            listViewTransactions.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            button2.Location = new Point(583, 217);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 13;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDelete_Click;
            // 
            // button3
            // 
            button3.Location = new Point(583, 275);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 14;
            button3.Text = "Back";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnBack_Click;
            // 
            // EditTransactionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(listViewTransactions);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Name = "EditTransactionControl";
            Size = new Size(744, 400);
            Load += EditTransactionControl_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private Button button1;
        private ListView listViewTransactions;
        private Button button2;
        private Button button3;
    }
}
