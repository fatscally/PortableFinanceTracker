using System;
//using System.Windows.Input;
using PFT.Base;
using PFT.Data;
using PFT.Interfaces;
//using PFT.Utils;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {


        //New Transaction
        //private ICommand _newTransaction_ClickCommand;
        //public ICommand NewTransaction_ClickCommand
        //{
        //    get
        //    {
        //        return _newTransaction_ClickCommand ?? (_newTransaction_ClickCommand = new CommandHandler(() => NewTransaction(CurrentTransaction), _canExecute));
        //    }
        //}
        public void NewTransaction(Transaction transaction)
        {

            CurrentTransaction = new Transaction();
            CurrentItem.Id = -1;
            CurrentTransaction.Date = DateTime.Now;
            CurrentTransaction.Item = null;
            CurrentTransaction.Price = CurrentItem.DefaultPrice;
            CurrentTransaction.Supplier = null;
            CurrentTransaction.InsertType = "M";

            //td.Save(transaction);
            //getAllTransactions();

            //System.Windows.MessageBox.Show("new transaction button not working");
        }


        //Delete Transaction
        //private ICommand _deleteTransaction_ClickCommand;
        //public ICommand DeleteTransaction_ClickCommand
        //{
        //    get
        //    {
        //        return _deleteTransaction_ClickCommand ?? (_deleteTransaction_ClickCommand = new CommandHandler(() => DeleteTransaction(CurrentTransaction), _canExecute));
        //    }
        //}
        public void DeleteTransaction(Transaction transaction)
        {
            //!!!!!!!!!  DELETE TAGS FOR TRANSACTION  !!!!!!!!!!!!!!!!!!!!!
            
            ITransactionData td = new TransactionData();

            td.Delete(transaction);

            CurrentTransaction = new Transaction();
            //CurrentItem.Id = -1;
            //CurrentTransaction.Date = DateTime.Now;
            //CurrentTransaction.Item = null;
            //CurrentTransaction.Price = CurrentItem.DefaultPrice;
            //CurrentTransaction.Supplier = null;
            //CurrentTransaction.InsertType = "M";
            getAllTransactions();
            GetSumOfTransactions();
            GetSumOfTransactionsBySupplier();
            GetSumOfTransactionsByPaymentType();

        }



        //Save Transaction
        //private ICommand _saveTransaction_ClickCommand;
        //public ICommand SaveTransaction_ClickCommand
        //{
        //    get
        //    {
        //        return _saveTransaction_ClickCommand ?? (_saveTransaction_ClickCommand = new CommandHandler(() => SaveTransaction(CurrentTransaction), _canExecute));
        //    }
        //}
        public void SaveTransaction(Transaction transaction)
        {

            ApplyTagsToTransaction();

            ITransactionData td = new TransactionData();
            CurrentTransaction.InsertType = "M";

            td.Save(transaction);
            getAllTransactions();

            CurrentTransaction = transaction;
  

            GetSumOfTransactions();
            GetSumOfTransactionsBySupplier();
            GetSumOfTransactionsByPaymentType();

        }


        private void ApplyTagsToTransaction()
        {
            CurrentTransaction.Tags.Clear();

            foreach (Tag tag in Tags)
            {
                if (tag.IsChecked)
                    CurrentTransaction.Tags.Add(tag);
            }
        }


        //Check the tags relevant to the CurrentTransaction
        private void CheckTheCurrentTags()
        {

            foreach (Tag t1 in Tags)
            {
                t1.IsChecked = false;
            }
            if (CurrentTransaction.Tags.Count > 0)
            {
                foreach (Tag t in CurrentTransaction.Tags)
                {
                    foreach (Tag ts in Tags)
                    {

                        if (t.Id == ts.Id)
                        {
                            ts.IsChecked = t.IsChecked;
                            break;
                        }
                    }
                }
            }
        }


        //private DateTime _startTransactionRange = DateTime.Now.AddDays(-7);
        //private DateTime _endTransactionRange = DateTime.Now;


        



        

        private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get { return _transactionDate; 
            }
            set { _transactionDate = value; }
        }
        


        private Transaction _currentTransaction;
        public Transaction CurrentTransaction
        {
            get
            {
                if (_currentTransaction == null)
                    _currentTransaction = new Transaction();

                return _currentTransaction;

            }
            set
            {
                _currentTransaction = value;
                CheckTheCurrentTags();
                NotifyPropertyChanged("CurrentTransaction");
            }

        }

        

        private TransactionCol _transactions;
        public TransactionCol Transactions
        {
            get {
                if (_transactions == null)
                    _transactions = new TransactionCol();

                return _transactions; 
            }
            set {
                _transactions = value;
            }
        }

        private void getAllTransactions()
        {
            Transactions.Clear();

            ITransactionColData tcd = new TransactionColData();
            foreach (Transaction t in tcd.LoadAll(StartDate, DateTime.Now))
            {
                Transactions.Add(t);
            }
        }


        
    }
}
