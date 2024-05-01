﻿using System.ComponentModel;

namespace PFT.Base
{
    public class Tag : INotifyPropertyChanged
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

        private int _parentTagId = -1;
        public int ParentTagId
        {
            get { return _parentTagId; }
            set { _parentTagId = value;
            NotifyPropertyChanged("ParentTagId");
            }
        }

        private bool _isChecked; 
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value;
            NotifyPropertyChanged("IsChecked");
            }
        }

        private double _totalSpent;

        /// <summary>
        /// Total spent for dates given
        /// </summary>
        public double TotalSpent
        {
            get { return _totalSpent; }
            set { _totalSpent = value; }
        }

        /// <summary>
        /// Predicted spend for the year
        /// </summary>
        private double _yearlySpend;

        public double YearlySpend
        {
            get { return _yearlySpend; }
            set { _yearlySpend = value; }
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
