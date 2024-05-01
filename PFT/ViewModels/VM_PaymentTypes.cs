using System.Windows.Input;
using PFT.Base;
using PFT.Data;
using PFT.Interfaces;
using PFT.Utils;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {

        private ICommand _savePaymentType_ClickCommand;
        public ICommand SavePaymentType_ClickCommand
        {
            get
            {
                return _savePaymentType_ClickCommand ?? (_savePaymentType_ClickCommand = new CommandHandler(() => SavePaymentType(CurrentPaymentType), _canExecute));
            }
        }
        public void SavePaymentType(PaymentType paymentType)
        {
            IPaymentTypeData td = new PaymentTypeData();
            td.Save(paymentType);
            getAllPaymentTypes();       
        }

        private ICommand _deletePaymentType_ClickCommand;
        public ICommand DeletePaymentType_ClickCommand
        {
            get
            {
                return _deletePaymentType_ClickCommand ?? (_deletePaymentType_ClickCommand = new CommandHandler(() => DeletePaymentType(CurrentPaymentType), _canExecute));
            }
        }
        public void DeletePaymentType(PaymentType paymentType)
        {
            IPaymentTypeData td = new PaymentTypeData();
            td.Delete(paymentType);
            getAllPaymentTypes();

            System.Windows.MessageBox.Show("Deleted Payment Type");
        }

        private ICommand _newPaymentType_ClickCommand;
        public ICommand NewPaymentType_ClickCommand
        {
            get
            {
                return _newPaymentType_ClickCommand ?? (_newPaymentType_ClickCommand = new CommandHandler(() => NewPaymentType(), _canExecute));
            }
        }
        public void NewPaymentType()
        {
            CurrentPaymentType = new PaymentType();
            CurrentPaymentType.Id = -1;
            CurrentPaymentType.Name = string.Empty;
            CurrentPaymentType.Description = string.Empty; 
        }
        




        private PaymentType _currentPaymentType;
        public PaymentType CurrentPaymentType
        {
            get {
                if (_currentPaymentType == null)
                    _currentPaymentType = new PaymentType();

                return _currentPaymentType; }
            set {
                _currentPaymentType = value;
                NotifyPropertyChanged("CurrentPaymentType");
            }
        }


        private PaymentTypeCol _paymentTypes;
        public PaymentTypeCol PaymentTypes
        {
            get {
                if (_paymentTypes == null)
                    _paymentTypes = new PaymentTypeCol();

                return _paymentTypes; 
            }
            set {
                _paymentTypes = value;
            }
        }

        private void getAllPaymentTypes()
        {
            PaymentTypes.Clear();

            IPaymentTypeColData tcd = new PaymentTypeColData();
            foreach (PaymentType t in tcd.LoadAll())
            {
                PaymentTypes.Add(t);
            }
        }
    }
}
