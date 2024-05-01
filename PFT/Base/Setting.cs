using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PFT.Base
{
    public class Setting
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

        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value;
            NotifyPropertyChanged("Key");
            }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value;
            NotifyPropertyChanged("Value");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
