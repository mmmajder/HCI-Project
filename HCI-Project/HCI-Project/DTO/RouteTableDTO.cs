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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _fromDate;
        private string _fromTime;
        private string _toDate;
        private string _toTime;
        private string _time;
        public string FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                if (value != _fromDate)
                {
                    _fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        public string FromTime
        {
            get
            {
                return _fromTime;
            }
            set
            {
                if (value != _fromTime)
                {
                    _fromTime = value;
                    OnPropertyChanged("FromTime");
                }
            }
        }
        public string ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                if (value != _toDate)
                {
                    _toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }

        public string ToTime
        {
            get
            {
                return _toTime;
            }
            set
            {
                if (value != _toTime)
                {
                    _toTime = value;
                    OnPropertyChanged("ToTime");
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
                    OnPropertyChanged("Indeks");
                }
            }
        }
    }
}

