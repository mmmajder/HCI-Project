using DragDropDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropDemo.Commands
{
    public class WagonInsertedCommand : ICommand
    {
        private readonly WagonListingViewModel _viewModel;
        
        public event EventHandler CanExecuteChanged;

        public WagonInsertedCommand(WagonListingViewModel viewModel)
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
            _viewModel.InsertWagon(_viewModel.InsertedWagonViewModel, _viewModel.TargetWagonViewModel);
        }
    }
}
