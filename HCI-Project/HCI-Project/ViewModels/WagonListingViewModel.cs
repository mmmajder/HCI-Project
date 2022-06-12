using DragDropDemo.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DragDropDemo.ViewModels
{
    public class WagonListingViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<WagonViewModel> _WagonViewModels;
        public ObservableCollection<WagonViewModel> _WagonPreview;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<WagonViewModel> WagonViewModels => _WagonViewModels;

        private WagonViewModel _incomingWagonViewModel;
        public WagonViewModel IncomingWagonViewModel
        {
            get
            {
                return _incomingWagonViewModel;
            }
            set
            {
                _incomingWagonViewModel = value;
                OnPropertyChanged(nameof(IncomingWagonViewModel));
            }
        }

        private WagonViewModel _removedWagonViewModel;
        public WagonViewModel RemovedWagonViewModel
        {
            get
            {
                return _removedWagonViewModel;
            }
            set
            {
                _removedWagonViewModel = value;
                OnPropertyChanged(nameof(RemovedWagonViewModel));
            }
        }

        private WagonViewModel _insertedWagonViewModel;
        public WagonViewModel InsertedWagonViewModel
        {
            get
            {
                return _insertedWagonViewModel;
            }
            set
            {
                _insertedWagonViewModel = value;
                OnPropertyChanged(nameof(InsertedWagonViewModel));
            }
        }

        private WagonViewModel _targetWagonViewModel;
        public WagonViewModel TargetWagonViewModel
        {
            get
            {
                return _targetWagonViewModel;
            }
            set
            {
                _targetWagonViewModel = value;
                OnPropertyChanged(nameof(TargetWagonViewModel));
            }
        }

        public ICommand WagonReceivedCommand { get; }
        public ICommand WagonRemovedCommand { get; }
        public ICommand WagonInsertedCommand { get; }

        public WagonListingViewModel()
        {
            _WagonViewModels = new ObservableCollection<WagonViewModel>();
            _WagonPreview = new ObservableCollection<WagonViewModel>();

            WagonReceivedCommand = new WagonReceivedCommand(this);
            WagonRemovedCommand = new WagonRemovedCommand(this);
            WagonInsertedCommand = new WagonInsertedCommand(this);
        }

        public void AddWagon(WagonViewModel item)
        {
            if(!_WagonViewModels.Contains(item))
            {
                _WagonViewModels.Add(item);
            }
        }

        public void InsertWagon(WagonViewModel insertedWagon, WagonViewModel targetWagon)
        {
            if(insertedWagon == targetWagon)
            {
                return;
            }

            int oldIndex = _WagonViewModels.IndexOf(insertedWagon);
            int nextIndex = _WagonViewModels.IndexOf(targetWagon);

            if(oldIndex != -1 && nextIndex != -1)
            {
                _WagonViewModels.Move(oldIndex, nextIndex);
            }
        }

        public void RemoveWagon(WagonViewModel item)
        {
            _WagonViewModels.Remove(item);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
