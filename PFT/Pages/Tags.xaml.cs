using System.Windows.Controls;

namespace PFT.Pages
{
    /// <summary>
    /// Interaction logic for Tags.xaml
    /// </summary>
    public partial class Tags : Page
    {
        public Tags()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }
    }
}
