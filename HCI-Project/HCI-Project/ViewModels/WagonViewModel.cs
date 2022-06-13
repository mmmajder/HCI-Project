using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DragDropDemo.ViewModels
{
    public class WagonViewModel : INotifyPropertyChanged
    {
        private string _description;
        public event PropertyChangedEventHandler PropertyChanged;

        public Wagon wagon { get; set; }
        public int wagonCount { get; set; }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public WagonViewModel(Wagon wagon, int number)
        {
            Description = wagon.Name;
            this.wagon = wagon;
            wagonCount = number;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
