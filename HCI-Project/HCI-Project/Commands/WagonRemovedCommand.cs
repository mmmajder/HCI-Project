using DragDropDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DragDropDemo.Commands
{
    public class WagonRemovedCommand : ICommand
    {
        private readonly WagonListingViewModel _WagonListingViewModel;
        public event EventHandler CanExecuteChanged;

        public WagonRemovedCommand(WagonListingViewModel WagonListingViewModel)
        {
            _WagonListingViewModel = WagonListingViewModel;
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
            _WagonListingViewModel.RemoveWagon(_WagonListingViewModel.RemovedWagonViewModel);
        }
    }
}
