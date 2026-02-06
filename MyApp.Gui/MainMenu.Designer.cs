namespace MyApp.Gui
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
            addIncomeButton = new Button();
            personalBudgetTrackerLabel = new Label();
            addExpenseButton = new Button();
            viewAllIncomesButton = new Button();
            viewAllExpensesButton = new Button();
            editTransactionButton = new Button();
            reportsAndSummariesButton = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // addIncomeButton
            // 
            addIncomeButton.Location = new Point(274, 151);
            addIncomeButton.Name = "addIncomeButton";
            addIncomeButton.Size = new Size(150, 23);
            addIncomeButton.TabIndex = 1;
            addIncomeButton.Text = "Add income";
            addIncomeButton.UseVisualStyleBackColor = true;
            addIncomeButton.Click += button1_Click;
            // 
            // personalBudgetTrackerLabel
            // 
            personalBudgetTrackerLabel.AutoSize = true;
            personalBudgetTrackerLabel.Location = new Point(278, 108);
            personalBudgetTrackerLabel.Name = "personalBudgetTrackerLabel";
            personalBudgetTrackerLabel.Size = new Size(132, 15);
            personalBudgetTrackerLabel.TabIndex = 2;
            personalBudgetTrackerLabel.Text = "Personal budget tracker";
            personalBudgetTrackerLabel.Click += label1_Click;
            // 
            // addExpenseButton
            // 
            addExpenseButton.Location = new Point(274, 180);
            addExpenseButton.Name = "addExpenseButton";
            addExpenseButton.Size = new Size(150, 23);
            addExpenseButton.TabIndex = 3;
            addExpenseButton.Text = "Add expense";
            addExpenseButton.UseVisualStyleBackColor = true;
            addExpenseButton.Click += button2_Click;
            // 
            // viewAllIncomesButton
            // 
            viewAllIncomesButton.Location = new Point(274, 238);
            viewAllIncomesButton.Name = "viewAllIncomesButton";
            viewAllIncomesButton.Size = new Size(150, 23);
            viewAllIncomesButton.TabIndex = 4;
            viewAllIncomesButton.Text = "View all incomes";
            viewAllIncomesButton.UseVisualStyleBackColor = true;
            viewAllIncomesButton.Click += button3_Click;
            // 
            // viewAllExpensesButton
            // 
            viewAllExpensesButton.Location = new Point(278, 267);
            viewAllExpensesButton.Name = "viewAllExpensesButton";
            viewAllExpensesButton.Size = new Size(146, 23);
            viewAllExpensesButton.TabIndex = 5;
            viewAllExpensesButton.Text = "View all expenses";
            viewAllExpensesButton.UseVisualStyleBackColor = true;
            viewAllExpensesButton.Click += button4_Click;
            // 
            // editTransactionButton
            // 
            editTransactionButton.Location = new Point(274, 209);
            editTransactionButton.Name = "editTransactionButton";
            editTransactionButton.Size = new Size(150, 23);
            editTransactionButton.TabIndex = 6;
            editTransactionButton.Text = "Edit transaction";
            editTransactionButton.UseVisualStyleBackColor = true;
            editTransactionButton.Click += button5_Click;
            // 
            // reportsAndSummariesButton
            // 
            reportsAndSummariesButton.Location = new Point(278, 296);
            reportsAndSummariesButton.Name = "reportsAndSummariesButton";
            reportsAndSummariesButton.Size = new Size(146, 23);
            reportsAndSummariesButton.TabIndex = 7;
            reportsAndSummariesButton.Text = "Reports and summaries";
            reportsAndSummariesButton.UseVisualStyleBackColor = true;
            reportsAndSummariesButton.Click += button6_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(278, 325);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(146, 23);
            exitButton.TabIndex = 8;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += button7_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(exitButton);
            Controls.Add(reportsAndSummariesButton);
            Controls.Add(editTransactionButton);
            Controls.Add(viewAllExpensesButton);
            Controls.Add(viewAllIncomesButton);
            Controls.Add(addExpenseButton);
            Controls.Add(personalBudgetTrackerLabel);
            Controls.Add(addIncomeButton);
            Name = "MainMenu";
            Size = new Size(753, 421);
            Load += MainMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addIncomeButton;
        private Label personalBudgetTrackerLabel;
        private Button addExpenseButton;
        private Button viewAllIncomesButton;
        private Button viewAllExpensesButton;
        private Button editTransactionButton;
        private Button reportsAndSummariesButton;
        private Button exitButton;
    }
}
