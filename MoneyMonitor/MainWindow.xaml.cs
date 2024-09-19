using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;



namespace MoneyMonitor
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            boxcategory.ItemsSource = Categoryinfo.category.Values;
            boxcategoryfilter.ItemsSource = Categoryinfo.category.Values;
            datePicker1.SelectedDate = DateTime.Now;
            datePicker2.SelectedDate = DateTime.Now;
            datePicker3.SelectedDate = DateTime.Now;
            FinanceGrid.ItemsSource = GetFiltredDataSQL();
        }
        Filter SQLfilter = new();
        Finance addcost = new ();
        private void calendar_dadebChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker1.SelectedDate != null)
            {
                SQLfilter.dateb = datePicker1.SelectedDate.Value.Date;
            }
        }
        private void calendar_dadeeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker2.SelectedDate != null)
            {
                SQLfilter.datee = datePicker2.SelectedDate.Value.Date.AddDays(1);
            }
        }
        private void calendar_adddadeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker3.SelectedDate != null)
            {
                addcost.date = datePicker3.SelectedDate.Value.Date.AddDays(1);
            }

        }
        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addcost.category = boxcategory.SelectedIndex;
            Categoryinfo.subcategory.TryGetValue(boxcategory.SelectedIndex, out var subcategory);
            if (subcategory != null)
            {
                boxsubcategory.ItemsSource = subcategory.Values;
            }
            
        }
        private void subcategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addcost.subcategory = boxsubcategory.SelectedIndex;
        }
        private void boxcategoryfilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLfilter.category = boxcategoryfilter.SelectedIndex;
            Categoryinfo.subcategory.TryGetValue(boxcategoryfilter.SelectedIndex, out var subcategory);
            if (subcategory != null)
            {
                boxsubcategoryfilter.ItemsSource = subcategory.Values;
            }
        }
        private void boxsubcategoryfilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLfilter.subcategory = boxsubcategoryfilter.SelectedIndex;
        }
        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            FinanceGrid.ItemsSource = GetFiltredDataSQL();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

            if (datePicker3.SelectedDate != null)
            {
                Finance buy = new Finance { date = addcost.date.ToUniversalTime(), Money = addcost.Money, category = addcost.category, subcategory = addcost.subcategory, comment = addcost.comment};
                WriteDataSQL(buy);
                FinanceGrid.ItemsSource = GetFiltredDataSQL();
                TextBox_Comment.Text = string.Empty;
                TextBox_Money.Text = string.Empty;
                boxsubcategory.SelectedIndex = 0;
                boxcategory.SelectedIndex = 0;
            }

        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
                // получаем объекты из бд
                FinanceGrid.ItemsSource = GetDataSQL();
        }
        private void textBox_KeyPress(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(TextBox_Money.Text, out double a))
            {
                TextBox_Money.Text = "";
            }
            addcost.Money = a;
        }
        private void TextBoxComment_TextChanged(object sender, RoutedEventArgs e)
        {
            addcost.comment = TextBox_Comment.Text;
        }
        private void Moneyfilter_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(Moneyfilter.Text, out double a))
            {
                Moneyfilter.Text = "";
            }
            
        }
        private List<FinanceRu> GetDataSQL()
        {

            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд 
                List<Finance> data = db.Finance.ToList();
                return FinanceToFinanceRu(data);
            }
            

            
        }
        private List<FinanceRu> GetFiltredDataSQL()
        {
            if (SQLfilter.dateb == SQLfilter.datee)
            {
                SQLfilter.datee.AddDays(1);
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                List<Finance> data = [];
                if (SQLfilter.category !=0 & SQLfilter.money != 0)
                {
                    data = db.Finance.Where(p => p.date > SQLfilter.dateb.ToUniversalTime() & p.date <= SQLfilter.datee.ToUniversalTime() & p.category == SQLfilter.category & p.subcategory == SQLfilter.subcategory & p.Money == SQLfilter.money).ToList();
                    return FinanceToFinanceRu(data);
                }
                if (SQLfilter.category != 0 & SQLfilter.money == 0)
                {
                    data = db.Finance.Where(p => p.date > SQLfilter.dateb.ToUniversalTime() & p.date <= SQLfilter.datee.ToUniversalTime() & p.category == SQLfilter.category & p.subcategory == SQLfilter.subcategory).ToList();
                    return FinanceToFinanceRu(data);
                }
                if (SQLfilter.category == 0 & SQLfilter.money != 0)
                {
                    data = db.Finance.Where(p => p.date > SQLfilter.dateb.ToUniversalTime() & p.date <= SQLfilter.datee.ToUniversalTime() & p.Money == SQLfilter.money).ToList();
                    return FinanceToFinanceRu(data);
                }
                data = db.Finance.Where(p => p.date > SQLfilter.dateb.ToUniversalTime() & p.date <= SQLfilter.datee.ToUniversalTime()).ToList();
                return FinanceToFinanceRu(data);
            }

        }
        private void WriteDataSQL(Finance buy)
        {
            // добавление данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // добавляем их в бд
                db.Finance.Add(buy);
                db.SaveChanges();

            }
        }
        private List<FinanceRu> FinanceToFinanceRu(List<Finance> x)
        {
            List<FinanceRu> result = new List<FinanceRu>();
            foreach (var op in x)
            {
                Categoryinfo.category.TryGetValue(op.category, out var categorydata);
                Categoryinfo.subcategory.TryGetValue(op.category, out var subcategorydata_tmp);
                subcategorydata_tmp.TryGetValue(op.subcategory, out var subcategorydata);


                FinanceRu Res = new();
                Res.Id = op.Id;
                Res.date = op.date;
                Res.Money = op.Money;
                Res.category = categorydata;
                Res.subcategory = subcategorydata;
                Res.comment = op.comment;

                result.Add(Res);
            }

            return result;

        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(LoginPage));
        }
    }

}