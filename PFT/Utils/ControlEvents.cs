using PFT.Utils;

namespace PFT.Clicks
{
    public partial class VM_Main 
    {

        private bool _canExecute;        

        //New Transaction
        private ICommand _newTransaction_ClickCommand;
        public ICommand NewTransaction_ClickCommand
        {
            get
            {
                return _newTransaction_ClickCommand ?? (_newTransaction_ClickCommand = new CommandHandler(() => App.ViewModel.NewTransaction(App.ViewModel.CurrentTransaction), _canExecute));
            }
        }


        //Delete Transaction
        private ICommand _deleteTransaction_ClickCommand;
        public ICommand DeleteTransaction_ClickCommand
        {
            get
            {
                return _deleteTransaction_ClickCommand ?? (_deleteTransaction_ClickCommand = new CommandHandler(() => App.ViewModel.DeleteTransaction(App.ViewModel.CurrentTransaction), _canExecute));
            }
        }
        

        //Save Transaction
        private ICommand _saveTransaction_ClickCommand;
        public ICommand SaveTransaction_ClickCommand
        {
            get
            {
                return _saveTransaction_ClickCommand ?? (_saveTransaction_ClickCommand = new CommandHandler(() => App.ViewModel.SaveTransaction(App.ViewModel.CurrentTransaction), _canExecute));
            }
        }


    }
}
