// Help on moving the Viewmodels to a serperate assembly.
//http://stackoverflow.com/questions/503329/implementing-mvvm-in-wpf-without-using-system-windows-input-icommand


using System.ComponentModel;
namespace PFT.ViewModels 
{
    public partial class VM_Main 
    {

       private bool _canExecute;        

        //Constructor
        public VM_Main()
        {

            Model = new CounterModel();

            _canExecute = true;
            //load settings
            GetAllSettings();
            GetAllTags();
            GetAllItems();//
            getAllSuppliers();
            getAllTransactions();
            getAllPaymentTypes();

            GetSumOfTransactions();
            GetSumOfTransactionsBySupplier();
            GetSumOfTransactionsByPaymentType();


            CurrentTransaction.Date = System.DateTime.Now;
        }


        private CounterModel Model { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public int Counter
        {
            get { return Model.Data; }
        }

        public void IncrementCounter()
        {
            Model.IncrementCounter();

            PropertyChanged(this, new PropertyChangedEventArgs("Counter"));
        }

        public void DecrementCounter()
        {
            Model.DecrementCounter();

            PropertyChanged(this, new PropertyChangedEventArgs("Counter"));
        }


    }

    public class CounterModel
    {
        public int Data { get; private set; }

        public void IncrementCounter()
        {
            Data++;
        }

        public void DecrementCounter()
        {
            Data--;
        }
    }

}
