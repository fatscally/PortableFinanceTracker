using System.Windows;
using PFT.Data;
using PFT.Pages;

namespace PFT
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
        //    InitializeComponent();
        //    DataContext = App.ViewModel;
        //     _NavigationFrame.Navigate(new Transactions());

        }


        private void mnuTransactionNew_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Transactions());
        }
        private void mnuTags_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Tags());
        }
        private void mnuItems_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new ItemsPage());
        }
        private void mnuSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Suppliers());
        }
        private void mnuTagGroups_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new TagGroups());
        }
        private void mnuPaymentTypes_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new PaymentTypes());
        }

        private void mnuCreateTable_Click(object sender, RoutedEventArgs e)
        {
            SQLiteUtilities s = new SQLiteUtilities();

            //PFT.Data.SqlCeUtilities s = new SqlCeUtilities();
            s.CreateTransactionsTable(true);
            s.CreateTransactionAutoRepeatTable(true);
            s.CreateItemsTable(true);
            s.CreateTagsTable(true);
            s.CreateSuppliersTable(true);
            s.CreatePaymentTypesTable(true);
            s.CreateTransaction_TagsTable();
            s.CreateSettingsTable(true);
            s.CreateItemDefaultTagsTable(true);
            s.CreateItemDefaultPaymentTypeTable(true);
            s.CreateRepeatTypesTable(true);
            s.CreateDateRanges(true);

            //Metadata Inserts
            s.Insert_DateRanges_Rows();
            s.Insert_Tags_Rows();
            s = null;

        }


    }
}
