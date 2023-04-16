using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ExpenseTrakingApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseTrakingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        static string budgetFileName1;
        protected override void OnAppearing()
        {   
            
            var TotalBudgetAmount = new List<decimal>();
            var budgetFiles = new List<Budget>();
            MonthlyGoal.Placeholder = "Update Budget";
            var budgetFile= Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.budget.txt");
            foreach (var file in budgetFile)
            {
                var budget = new Budget
                {
                    BudgetAmount = decimal.Parse(File.ReadAllText(file)),
                    BudgetDate = File.GetCreationTime(file),
                    BudgetFileName = file
                };
                DateTime dt = DateTime.Now;
                if (dt.Month == (budget.BudgetDate).Month)
                {

                }
                budgetFiles.Add(budget);
                decimal budgetcontent = decimal.Parse(File.ReadAllText(file));
                TotalBudgetAmount.Add(budgetcontent);
                

            }
            //decimal TotalBudgetAmount1 = TotalBudgetAmount.Sum();

           decimal TotalBudgetAmount1 = TotalBudgetAmount.Last();
            var b = TotalBudgetAmount1.ToString();
            Budget.Text = "BUDGET  "+ b;

                var expenses = new List<ExpenseDetail>();
            var TotalAmount=new List<int>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.notes.txt");
            int amount = 0;
            foreach(var file in files)
            {
                //var  totalAmount=new List<int>();
                var expense = new ExpenseDetail
                {
                    Text = File.ReadAllText(file),
                    Date = File.GetCreationTime(file),
                    FileName = file
                };
                expenses.Add(expense);

                var content=File.ReadAllText(file);
                var contentarray = content.Split(' ');
                amount = Int32.Parse(contentarray[1]);
               TotalAmount.Add(amount);
                

            }
            int TotalAmount1 = TotalAmount.Sum(x => Convert.ToInt32(x));
            ExpenseListView.ItemsSource = expenses.OrderByDescending(e => e.Date);
            var a =TotalAmount1 .ToString();
           
            AmountSpent.Text = "SPENT  "+ a; 
        }
    
        private void MonthlyGoalSaveButton_Clicked(object sender, EventArgs e)
        {
           var budget = new Budget();
            budget.BudgetAmount = decimal.Parse(MonthlyGoal.Text);
            budget.BudgetDate = DateTime.Now;
            budget.BudgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.budget.txt");

            File.WriteAllText(budget.BudgetFileName, budget.BudgetAmount.ToString());
            budgetFileName1=budget.BudgetFileName;


            Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = new ExpenseDetail()
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void NewExpense_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext=new ExpenseDetail()
            });
        }

        private void ExpenseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (ExpenseDetail)e.SelectedItem
            });
        }
    }
}
