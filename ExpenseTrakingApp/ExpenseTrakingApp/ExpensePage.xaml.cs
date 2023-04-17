using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrakingApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace ExpenseTrakingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
        {
            InitializeComponent();
        }
        static string category1;
        protected override void OnAppearing()
        {
            var expense=(ExpenseDetail)BindingContext;
            var categoryList = new List<string>();
            categoryList.Add("Groceries");
            categoryList.Add("Shopping");
            categoryList.Add("Rent");
            categoryList.Add("Auto");
            categoryList.Add("Education");
            categoryList.Add("Others");

            var CategoryPicker = new Xamarin.Forms.Picker { Title = "Select a Category", TitleColor = Color.Blue };
            CategoryPicker.ItemsSource = categoryList;
            if (expense!=null&&!string.IsNullOrEmpty(expense.FileName))
            {
               var content = (File.ReadAllText(expense.FileName));
                var contentarray = content.Split('|');
                Name.Text = contentarray[0];
                Amount.Text = contentarray[1];
                Category.Text= contentarray[2];
                ExpenseDate.Date = DateTime.Parse(contentarray[3]);
                //Name.Text = File.ReadAllText(expense.FileName);
                //Name.Text=
            }

       

            

            //var categoryNameLabel=new Label();
          //categoryNameLabel.SetBinding(Label.TextProperty,new Binding("SelectedItem",source: CategoryPicker));
           





        }
        private void ExpenseSaveButtonClicked(object sender, EventArgs e)
        {
            var expense = (ExpenseDetail)BindingContext;
            expense.Name = (Name.Text).Trim();
            expense.Amount = Amount.Text;
            expense.Category= Category.Text;
           //var decimal checked= Convert.ToDecimal(Amount.Text);
            
            //var categoryNameLabel = new Label();
            
            //categoryNameLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: CategoryPicker));
            //expense.Category = categoryNameLabel.Text;
            
            expense.Date = ExpenseDate.Date;
            
            if (string.IsNullOrEmpty(expense.FileName))
            {
                
                expense.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.notes.txt");
            }
            File.WriteAllText(expense.FileName, expense.Name+"|"+expense.Amount+"|"+expense.Category+"|"+(expense.Date).ToShortDateString());
            Navigation.PopModalAsync();

        }
        private void ExpenseCancelButtonClicked(object sender, EventArgs e)
        {
          
            var expense=(ExpenseDetail)BindingContext;
            if(File.Exists(expense.FileName))
            {
                File.Delete(expense.FileName);
            }
            Name.Text = String.Empty;
            Navigation.PopModalAsync();
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var categoryNameLabel = new Label();

            categoryNameLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: CategoryPicker));

            Category.Text = categoryNameLabel.Text;


        }

        
    }
}