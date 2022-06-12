using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DragDropDemo.ViewModels
{
    public class WagonContext : INotifyPropertyChanged
    {
        public WagonListingViewModel InProgressWagonListingViewModel { get; }
        public WagonListingViewModel CompletedWagonListingViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WagonContext(WagonListingViewModel inProgressTodoItemListingViewModel, WagonListingViewModel completedTodoItemListingViewModel)
        {
            InProgressWagonListingViewModel = inProgressTodoItemListingViewModel;
            CompletedWagonListingViewModel = completedTodoItemListingViewModel;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
