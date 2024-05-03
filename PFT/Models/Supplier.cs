using System.ComponentModel;

namespace PFT.Base
{
    public class Supplier : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { 
                _id = value;
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

        private double _totalSpend;
        public double TotalSpend
        {
            get { return _totalSpend; }
            set { _totalSpend = value;
            NotifyPropertyChanged("TotalSpend");
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
