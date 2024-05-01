using PFT.Base;
using PFT.Data;
using PFT.Interfaces;
//using PFT.Utils;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {

        //private ICommand _saveSupplier_ClickCommand;
        //public ICommand SaveSupplier_ClickCommand
        //{
        //    get
        //    {
        //        return _saveSupplier_ClickCommand ?? (_saveSupplier_ClickCommand = new CommandHandler(() => SaveSupplier(CurrentSupplier), _canExecute));
        //    }
        //}
        public void SaveSupplier(Supplier supplier)
        {
            ISupplierData td = new SupplierData();
            td.Save(supplier);
            getAllSuppliers();

            //System.Windows.MessageBox.Show("Saved Supplier");            
        }

        //private ICommand _deleteSupplier_ClickCommand;
        //public ICommand DeleteSupplier_ClickCommand
        //{
        //    get
        //    {
        //        return _deleteSupplier_ClickCommand ?? (_deleteSupplier_ClickCommand = new CommandHandler(() => DeleteSupplier(CurrentSupplier), _canExecute));
        //    }
        //}
        public void DeleteSupplier(Supplier supplier)
        {
            ISupplierData td = new SupplierData();
            td.Delete(supplier);
            getAllSuppliers();

            //System.Windows.MessageBox.Show("Deleted Supplier");
        }

        //private ICommand _newSupplier_ClickCommand;
        //public ICommand NewSupplier_ClickCommand
        //{
        //    get
        //    {
        //        return _newSupplier_ClickCommand ?? (_newSupplier_ClickCommand = new CommandHandler(() => CreateNewSupplier(), _canExecute));
        //    }
        //}
        public void CreateNewSupplier()
        {
            CurrentSupplier.Id = -1;
            CurrentSupplier.Name = string.Empty;
            CurrentSupplier.Description = string.Empty; 
            //System.Windows.MessageBox.Show("New Supplier");
        }
        
        private Supplier _currentSupplier;
        public Supplier CurrentSupplier
        {
            get {
                if (_currentSupplier == null)
                    _currentSupplier = new Supplier();

                return _currentSupplier; }
            set { 
                _currentSupplier = value;
            }
        }
        
        private SupplierCol _suppliers;
        public SupplierCol Suppliers
        {
            get {
                if (_suppliers == null)
                    _suppliers = new SupplierCol();

                return _suppliers; 
            }
            set {
                _suppliers = value;
            }
        }

        private void getAllSuppliers()
        {
            Suppliers.Clear();

            ISupplierColData tcd = new SupplierColData();
            foreach (Supplier t in tcd.LoadAll())
            {
                Suppliers.Add(t);
            }
        }
    
    
    }
}
