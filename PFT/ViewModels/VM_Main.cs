
namespace PFT.ViewModels 
{
    public partial class VM_Main 
    {

       private bool _canExecute;        

        //Constructor
        public VM_Main()
        {
            _canExecute = true;
            //load settings

            GetAllSettings();
            GetAllTags();
            //GetAllItems();//
            getAllSuppliers();
            getAllPaymentTypes();
            getAllTransactions();
            

            GetSumOfTransactions();
            GetSumOfTransactionsBySupplier();
            GetPaymentTypeTotalSpend();
            GetTagTotalSpend();
            GetItemTotalSpend();


            CurrentTransaction.Date = System.DateTime.Now;
        }
        



    }
}
