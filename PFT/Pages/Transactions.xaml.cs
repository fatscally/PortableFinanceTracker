using System.Windows;
using System.Windows.Controls;

namespace PFT.Pages
{
    /// <summary>
    /// Interaction logic for TransactionNew.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        public Transactions()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void btnTags_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ItemsPage());
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Suppliers());
        }

        private void btnPaymentType_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PaymentTypes());
        }





    }
}
