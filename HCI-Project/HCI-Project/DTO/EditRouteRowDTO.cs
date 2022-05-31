using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.DTO
{
    class EditRouteRowDTO : INotifyPropertyChanged
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

        private string _ArrivalTime;
        public string ArrivalTime
        {
            get { return _ArrivalTime; }
            set
            {
                if (_ArrivalTime != value)
                {
                    _ArrivalTime = value;
                    // ScheduledRouteWindow.SelectedScheduledStation.TimeRange.Arrival = DateTime.Parse(value); 
                    OnPropertyChanged("ArrivalTime");
                }
            }
        }

        private string _DepatureTime;
        public string DepatureTime
        {
            get { return _DepatureTime; }
            set
            {
                if (_DepatureTime != value)
                {
                    _DepatureTime = value;
                    //ScheduledRouteWindow.SelectedScheduledStation.TimeRange.Depature = DateTime.Parse(value);
                    OnPropertyChanged("DepatureTime");
                }
            }
        }
    }
}
