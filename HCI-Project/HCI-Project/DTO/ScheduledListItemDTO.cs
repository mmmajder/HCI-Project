using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.DTO
{
    public class ScheduledListItemDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _Station;
        public string Station
        {
            get { return _Station; }
            set
            {
                if (_Station != value)
                {
                    _Station = value;
                    OnPropertyChanged("Station");
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
    }
}
