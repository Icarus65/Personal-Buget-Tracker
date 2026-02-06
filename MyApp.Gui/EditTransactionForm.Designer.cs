namespace MyApp.Gui
{
    partial class EditTransactionForm
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
            txtDescription = new TextBox();
            txtAmount = new TextBox();
            dtpDate = new DateTimePicker();
            cmbCategory = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(266, 177);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(237, 23);
            txtDescription.TabIndex = 0;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(266, 206);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(237, 23);
            txtAmount.TabIndex = 1;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(266, 148);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(237, 23);
            dtpDate.TabIndex = 2;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(266, 235);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(237, 23);
            cmbCategory.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(266, 264);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(347, 264);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // EditTransactionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbCategory);
            Controls.Add(dtpDate);
            Controls.Add(txtAmount);
            Controls.Add(txtDescription);
            Name = "EditTransactionForm";
            Text = "EditIncomeForm";
            Load += this.EditTransactionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDescription;
        private TextBox txtAmount;
        private DateTimePicker dtpDate;
        private ComboBox cmbCategory;
        private Button btnSave;
        private Button btnCancel;
    }
}