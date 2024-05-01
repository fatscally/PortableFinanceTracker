using PFT.Base;
using PFT.Data;
using PFT.Interfaces;
//using PFT.Utils;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {
        //private ICommand _saveItem_ClickCommand;
        //public ICommand SaveItem_ClickCommand
        //{
        //    get
        //    {
        //        return _saveItem_ClickCommand ?? (_saveItem_ClickCommand = new CommandHandler(() => SaveItem(CurrentItem), _canExecute));
        //    }
        //}
        public void SaveItem(Item item)
        {
            IItemData id = new ItemData();
            id.Save(item);
            GetAllItems();
            //System.Windows.MessageBox.Show("Saved Item");
            //TODO Navigate back to Transaction page with this item selected
            CurrentItem = item;

        }


        //private ICommand _newItem_ClickCommand;
        //public ICommand NewItem_ClickCommand
        //{
        //    get
        //    {
        //        return _newItem_ClickCommand ?? (_newItem_ClickCommand = new CommandHandler(() => NewItem(), _canExecute));
        //    }
        //}
        public void NewItem()
        {
            //CurrentItem = new Item();
            CurrentItem.Id = -1;
            CurrentItem.Name = "New";  //just to trigger INotifyProperty
            CurrentItem.Description = string.Empty;
            CurrentItem.DefaultPrice = 0;
        }


        private ItemCol _items;

        public ItemCol Items
        {
            get
            {
                if (_items == null)
                    _items = new ItemCol();

                return _items;
            }
            set { _items = value; }
        }


        private Item _currentItem;
        public Item CurrentItem
        {
            get
            {
                if (_currentItem == null)
                    _currentItem = new Item();

                return _currentItem;
            }
            set { _currentItem = value; }
        }

        private void GetAllItems()
        {
            try
            {
                IItemColData icd = new ItemColData();
                Items.Clear();
                foreach (Item s in icd.LoadAll())
                    Items.Add(s);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
