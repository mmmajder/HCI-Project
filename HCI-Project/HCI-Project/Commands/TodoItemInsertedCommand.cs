using DragDropDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropDemo.Commands
{
    public class TodoItemInsertedCommand : ICommand
    {
        private readonly TodoItemListingViewModel _viewModel;
        
        public event EventHandler CanExecuteChanged;

        public TodoItemInsertedCommand(TodoItemListingViewModel viewModel)
        {
            _viewModel = viewModel;
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
            _viewModel.InsertTodoItem(_viewModel.InsertedTodoItemViewModel, _viewModel.TargetTodoItemViewModel);
        }
    }
}
