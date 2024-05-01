using System.Windows;
using PFT.ViewModels;

namespace PFT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var view = new HomeWindow();

            view.InitializeComponent();
            view.DataContext = ViewModel;
            //CommandRouter.WireMainView(view, ViewModel);
            view._NavigationFrame.Navigate(new Pages.Transactions());
            view.Show();
        }


        private static VM_Main _viewModel;

        public static VM_Main ViewModel
        {
            get
            {
                if (_viewModel == null)
                    _viewModel = new PFT.ViewModels.VM_Main();

                return _viewModel;
            }
            set { _viewModel = value; }
        }

    }
}
