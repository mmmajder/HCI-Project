using DragDropDemo.ViewModels;
using System;
using System.Windows.Input;

namespace DragDropDemo.Commands
{
    public class WagonReceivedCommand : ICommand
    {
        private readonly WagonListingViewModel _WagonListingViewModel;
        public event EventHandler CanExecuteChanged;

        public WagonReceivedCommand(WagonListingViewModel WagonListingViewModel)
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
            if((bool)parameter == true)
            {
                Console.WriteLine("DROPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP " + _WagonListingViewModel.IncomingWagonViewModel);
            }
            _WagonListingViewModel.AddWagon(_WagonListingViewModel.IncomingWagonViewModel);
        }
    }
}
