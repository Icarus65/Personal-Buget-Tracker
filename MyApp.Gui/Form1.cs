using Personal_Buget_Tracker;

namespace MyApp.Gui
{
    public partial class Form1 : Form
    {
        public BudgetManager budgetManager { get; private set; }
        public ReportService reportService { get; private set; }
        public IncomeManager IncomeManager { get; private set; }
        public ExpenseManager ExpenseManager { get; private set; }
        public Form1()
        {
            InitializeComponent();
            budgetManager = new BudgetManager();
            reportService = new ReportService(budgetManager);

            LoadControl(new MainMenu(this, budgetManager, reportService));
        }

        public void LoadControl(UserControl control)
        {
            Controls.Clear();
            control.Dock = DockStyle.Fill;
            Controls.Add(control);
        }
        public void button1_Click(object sender, EventArgs e)
        {
        }
        public void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
