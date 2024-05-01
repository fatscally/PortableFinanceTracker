using System.Windows;
 
namespace PFT
{
    /// <summary>
    /// Interaction logic for TagMaintenance.xaml
    /// </summary>
    public partial class TagMaintenance : Window
    {
        public TagMaintenance()
        {
            DataContext = App.ViewModel;
            InitializeComponent();
        }

        private void btnNewTag_Click(object sender, RoutedEventArgs e)
        {
            lstTags.SelectedIndex = -1;
        }

    }
}
