using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DragDropDemo.ViewModels
{
    public class TodoViewModel : INotifyPropertyChanged
    {
        public TodoItemListingViewModel InProgressTodoItemListingViewModel { get; }
        public TodoItemListingViewModel CompletedTodoItemListingViewModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public TodoViewModel(TodoItemListingViewModel inProgressTodoItemListingViewModel, TodoItemListingViewModel completedTodoItemListingViewModel)
        {
            InProgressTodoItemListingViewModel = inProgressTodoItemListingViewModel;
            CompletedTodoItemListingViewModel = completedTodoItemListingViewModel;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
