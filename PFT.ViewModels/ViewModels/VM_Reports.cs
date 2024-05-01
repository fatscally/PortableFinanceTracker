using System;
using System.ComponentModel;
using PFT.Data;
using PFT.Interfaces;

namespace PFT.ViewModels
{
    public partial class VM_Main : INotifyPropertyChanged
    {





        private DateTime _startDate = DateTime.Now.AddDays(-7);
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged("StartDate");
                getAllTransactions();  //the list
                GetSumOfTransactions(); // the Grand Totals
                GetSumOfTransactionsBySupplier(); //
                GetSumOfTransactionsByPaymentType();
            }
        }

        private DateTime _endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value;
                NotifyPropertyChanged("EndDate");
                getAllTransactions();
                GetSumOfTransactionsBySupplier();
                GetSumOfTransactionsByPaymentType();
            }
        }


        public string ReportRange
        {
            set
            {

                switch (value)
                {
                    case "Since Monday":
                        int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
                        StartDate = DateTime.Today.AddDays(delta);
                        break;

                    case "Last 7 days":
                        StartDate = DateTime.Today.AddDays(-7);
                        break;

                    case "This month":
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        break;

                    case "Last 28 days":
                        StartDate = DateTime.Today.AddDays(-28);
                        break;

                    case "This year":
                        StartDate = DateTime.Today.AddDays(-(DateTime.Now.DayOfYear - 1));
                        break;

                    case "Today":
                    default:
                        StartDate = DateTime.Now;
                        break;
                }

                EndDate = DateTime.Now;

                //getAllTransactions();
                //GetSumOfTransactions();
            }
        }



        private double _totalMoneyOut;
        public double TotalMoneyOut
        {
            get
            { return _totalMoneyOut; }
            set { _totalMoneyOut = value;
                NotifyPropertyChanged("TotalMoneyOut");
            }
        }


        private double _totalMoneyIn;
        public double TotalMoneyIn
        {
            get { return _totalMoneyIn; }
            set {
                if (_totalMoneyIn == value)
                    return;

                _totalMoneyIn = value;
                NotifyPropertyChanged("TotalMoneyIn");
            }
        }


        /// <summary>
        /// Gets the total of transactions for the given date range
        /// </summary>
        /// <param name="isIncome">Set to true if tracking income like salary.  False if spending money on items</param>
        private void GetSumOfTransactions()
        {
            try
            {
                ITransactionColData d = new TransactionColData();
                TotalMoneyIn = d.GetTransactionBalance(StartDate, EndDate, false);
                TotalMoneyOut = d.GetTransactionBalance(StartDate, EndDate, true);
            }
            catch (System.Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Gets the total of transactions by Supplier for the given date range
        /// </summary>
        private void GetSumOfTransactionsBySupplier()
        {
            try
            {
                ISupplierData d = new SupplierData();
                
                foreach (PFT.Base.Supplier itm in Suppliers)
                {
                    itm.TotalSpend = d.GetSupplierSpendTotal(itm.Id, StartDate, EndDate, false);
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Gets the total of transactions by PaymentType for the given date range
        /// </summary>
        private void GetSumOfTransactionsByPaymentType()
        {
            try
            {
                IPaymentTypeData d = new PaymentTypeData();

                foreach (PFT.Base.PaymentType itm in PaymentTypes)
                {
                    itm.TotalSpend = d.GetPaymentTypeSpendTotal(itm.Id, StartDate, EndDate, false);
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
