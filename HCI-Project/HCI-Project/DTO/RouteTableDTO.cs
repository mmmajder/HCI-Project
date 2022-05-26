using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.DTO
{
    public class RouteTableDTO : INotifyPropertyChanged
    {
        private string _From;
        public string From
        {
            get { return _From; }
            set
            {
                if (_From != value)
                {
                    _From = value;
                    OnPropertyChanged("From");
                }
            }
        }

        private string _Depature;
        public string Depature
        {
            get { return _Depature; }
            set
            {
                if (_Depature != value)
                {
                    _Depature = value;
                    OnPropertyChanged("Depature");
                }
            }
        }
        private string _To; 
        public string To
        {
            get { return _To; }
            set
            {
                if (_To != value)
                {
                    _To = value;
                    OnPropertyChanged("To");
                }
            }
        }
        private string _Arrival;
        public string Arrival
        {
            get { return _Arrival; }
            set
            {
                if (_Arrival != value)
                {
                    _Arrival = value;
                    OnPropertyChanged("Arrival");
                }
            }
        }
        private string _Time;
        public string Time
        {
            get { return _Time; }
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
    /*public class RouteTableDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _from;
        private string _depature;
        private string _to;
        private string _arrival;
        private string _time;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                if (value != _from)
                {
                    _from = value;
                    OnPropertyChanged("From");
                }
            }
        }
        public string Depature
        {
            get
            {
                return _depature;
            }
            set
            {
                if (value != _depature)
                {
                    _depature = value;
                    OnPropertyChanged("Depature");
                }
            }
        }
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged("To");
                }
            }
        }

        public string Arrival
        {
            get
            {
                return _arrival;
            }
            set
            {
                if (value != _arrival)
                {
                    _arrival = value;
                    OnPropertyChanged("Arrival");
                }
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged("Time");
                }
            }
        }
    }*/
//}

