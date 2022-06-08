using DragDropDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropDemo.Commands
{
    public class TodoItemRemovedCommand : ICommand
    {
        private readonly TodoItemListingViewModel _todoItemListingViewModel;
        public event EventHandler CanExecuteChanged;

        public TodoItemRemovedCommand(TodoItemListingViewModel todoItemListingViewModel)
        {
            _todoItemListingViewModel = todoItemListingViewModel;
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
        protected void OnCanExecuteChanged()
        {
        }
        public void Execute(object parameter)
        {
            _todoItemListingViewModel.RemoveTodoItem(_todoItemListingViewModel.RemovedTodoItemViewModel);
        }
    }
}
