using System;
using System.ComponentModel;
//using PFT.Data;

namespace PFT.Base
{

    

    public class Transaction : INotifyPropertyChanged
    {
        public Transaction()
        {
            _date = DateTime.Now;
        }
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private Item _item;
        public Item Item
        {
            get {
                if (_item == null)
                    _item = new Base.Item();
                return _item; 
            }
            set {

                //if (_item == value)
                //    return;

                //if(_item == null)
                //    _item = new Base.Item();

                //if (value != null)
                //{//auto load the Item object
                //    if (_item.Id > 0 && _item.Id != value.Id)
                //    {
                //        ItemData itemData = new ItemData();
                //        _item = itemData.Select(value);
                //    }
                //}
                _item = value;

                NotifyPropertyChanged("Item");
            }
        }


        private PaymentType _paymentType;
        public PaymentType PaymentType
        {
            get
            {
                if (_paymentType == null)
                    _paymentType = new PaymentType();

                return _paymentType;

            }
            set
            {
                //if (_paymentType == value)
                //    return;

                //if (_paymentType == null)
                //    _paymentType = new PaymentType();

                //if (value != null)
                //{ //auto load the PaymentType object
                //    if (_paymentType.Id > 0 && _paymentType.Id != value.Id)
                //    {
                //        PaymentTypeData ptData = new PaymentTypeData();
                //        _paymentType = ptData.Select(value);
                //    }
                //}

                _paymentType = value;

                NotifyPropertyChanged("PaymentType");
            }
        }


        private float _price;
        public float Price
        {
            get { return _price; }
            set {
                _price = value;

                NotifyPropertyChanged("Price");
            }
        }

        private bool _isIncome;
        public bool IsIncome
        {
            get { return _isIncome; }
            set { _isIncome = value;
            NotifyPropertyChanged("IsIncome");
            }
        }
        
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { 
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                NotifyPropertyChanged("Comment");
            }
        }

        private Supplier _supplier;
        public Supplier Supplier
        {
            get {
                if (_supplier == null)
                    _supplier = new Base.Supplier();

                return _supplier; 
            }
            set {
                if (_supplier == null)
                    _supplier = new Base.Supplier();

                _supplier = value;
                NotifyPropertyChanged("Supplier");
            }
        }

        private TagCol _tags;
        public TagCol Tags
        {
            get {
                if (_tags == null)
                    _tags = new TagCol();

                return _tags; }
            set { _tags = value; }
        }



        public string NoTagIconVisibility
        {
            get {
                if (_tags.Count <= 0)
                    return "Visible";

                return "Hidden";
            }

        }
        


        private string _insertType;
        /// <summary>
        /// A - Auto, M - Manual, V - Verified
        /// </summary>
        public string InsertType
        {
            get { return _insertType; }
            set { _insertType = value;
            NotifyPropertyChanged("InsertType");
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
