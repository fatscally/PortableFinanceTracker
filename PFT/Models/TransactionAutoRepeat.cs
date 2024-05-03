using System;
using System.ComponentModel;
using PFT;

namespace PFT.Base
{



    public class TransactionAutoRepeat : INotifyPropertyChanged
    {

        public int Id { get; set; }

        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;
                NotifyPropertyChanged("ItemId");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }

        //private DateTime _date;
        //public DateTime Date
        //{
        //    get { return _date; }
        //    set
        //    {
        //        _date = value;
        //        NotifyPropertyChanged("Date");
        //    }
        //}


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

        private int _supplierId;
        public int SupplierId
        {
            get { return _supplierId; }
            set
            {
                _supplierId = value;
                NotifyPropertyChanged("SupplierId");
            }
        }

        private int _repeatTypeId;
        /// <summary>
        /// Daily, Weekly, Monthly
        /// </summary>
        public int RepeatTypeId
        {
            get { return _repeatTypeId; }
            set { _repeatTypeId = value; }
        }

        private int _repeatDay;
        public int RepeatDay
        {
            get { return _repeatDay; }
            set { _repeatDay = value; }
        }



        private DateTime _lastDateInserted;
        public DateTime LastDateInserted
        {
            get { return _lastDateInserted; }
            set
            {
                _lastDateInserted = value;
                NotifyPropertyChanged("LastDateInserted");
            }
        }
        
        /// <summary>
        /// A for Auto Insert, M for manual, V for verified
        /// </summary>
        private string _insertType = "A";
        public string InsertType
        {
            get { return _insertType; }
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
