using System.ComponentModel;

namespace PFT.Base
{
    public class Item : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value;
                NotifyPropertyChanged("Id");
                }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
            NotifyPropertyChanged("Name");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value;
            NotifyPropertyChanged("Description");
            }
        }

        private float _budget;
        public float Budget
        {
            get { return _budget; }
            set { _budget = value;
            NotifyPropertyChanged("Budget");
            }
        }
        

        private float _defaultPrice;
        public float DefaultPrice
        {
            get { return _defaultPrice; }
            set { _defaultPrice = value;
            NotifyPropertyChanged("DefaultPrice");
            }
        }

        private double _totalSpend;
        /// <summary>
        /// Total spent for dates given
        /// </summary>
        public double TotalSpend
        {
            get { return _totalSpend; }
            set
            {
                _totalSpend = value;
                NotifyPropertyChanged("TotalSpend");
            }
        }



        private bool _needIt;
        public bool NeedIt
        {
            get { return _needIt; }
            set
            {
                _needIt = value;
                if (value)
                { NotNeeded = false; }
                else
                { NotNeeded = true; }

                NotifyPropertyChanged("NeedIt");
            }
        }


        private bool _notNeeded;
        public bool NotNeeded
        {
            get { return _notNeeded; }
            set
            {
                _notNeeded = value;
                NotifyPropertyChanged("NotNeeded");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
