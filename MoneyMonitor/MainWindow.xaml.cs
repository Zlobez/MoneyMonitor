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
            
        }
        Filter datefilter = new();

        private void calendar_dadebChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = datePicker1.SelectedDate;
            datefilter.dateb = selectedDate.Value.Date;
        }
        private void calendar_dadeeChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = datePicker2.SelectedDate;
            datefilter.datee = selectedDate.Value.Date;
        }



        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            FinanceGrid.ItemsSource = GetFiltredDataSQL();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Finance buy = new Finance { date = DateTime.UtcNow, Money = 9000.0, category = 2, subcategory = 1, comment = "comentariy1" };
            WriteDataSQL(buy);
            ButtonUpdate_Click(Update, e);
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
                // получаем объекты из бд
                FinanceGrid.ItemsSource = GetDataSQL();
        }

        private List<Finance> GetDataSQL()
        {
            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                return db.Finance.ToList();
            }
        }
        private List<Finance> GetFiltredDataSQL()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Finance.Where(p => p.date >= datefilter.dateb.ToUniversalTime() & p.date <= datefilter.datee.ToUniversalTime()).ToList();
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

    }

}