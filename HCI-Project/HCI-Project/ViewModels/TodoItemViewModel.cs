﻿using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DragDropDemo.ViewModels
{
    public class TodoItemViewModel : INotifyPropertyChanged
    {
        private string _description;
        public event PropertyChangedEventHandler PropertyChanged;

        public Station Station { get; set; }
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

        public TodoItemViewModel(Station station)
        {
            Description = station.Name;
            Station = station;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}