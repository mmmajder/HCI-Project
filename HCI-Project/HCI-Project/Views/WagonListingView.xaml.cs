using DragDropDemo.ViewModels;
using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.Views
{
    /// <summary>
    /// Interaction logic for WagonListingView.xaml
    /// </summary>
    public partial class WagonListingView : UserControl
    {
        public static readonly DependencyProperty IncomingWagonProperty =
            DependencyProperty.Register("IncomingWagon", typeof(WagonViewModel), typeof(WagonListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public WagonViewModel IncomingWagon
        {
            get { return (WagonViewModel)GetValue(IncomingWagonProperty); }
            set { SetValue(IncomingWagonProperty, value); }
        }

        public static readonly DependencyProperty RemovedWagonProperty =
            DependencyProperty.Register("RemovedWagon", typeof(WagonViewModel), typeof(WagonListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public WagonViewModel RemovedWagon
        {
            get { return (WagonViewModel)GetValue(RemovedWagonProperty); }
            set { SetValue(RemovedWagonProperty, value); }
        }

        public static readonly DependencyProperty WagonDropCommandProperty =
            DependencyProperty.Register("WagonDropCommand", typeof(ICommand), typeof(WagonListingView),
                new PropertyMetadata(null));

        public ICommand WagonDropCommand
        {
            get { return (ICommand)GetValue(WagonDropCommandProperty); }
            set { SetValue(WagonDropCommandProperty, value); }
        }

        public static readonly DependencyProperty WagonRemovedCommandProperty =
            DependencyProperty.Register("WagonRemovedCommand", typeof(ICommand), typeof(WagonListingView),
                new PropertyMetadata(null));

        public ICommand WagonRemovedCommand
        {
            get { return (ICommand)GetValue(WagonRemovedCommandProperty); }
            set { SetValue(WagonRemovedCommandProperty, value); }
        }

        public static readonly DependencyProperty WagonInsertedCommandProperty =
            DependencyProperty.Register("WagonInsertedCommand", typeof(ICommand), typeof(WagonListingView),
                new PropertyMetadata(null));

        public ICommand WagonInsertedCommand
        {
            get { return (ICommand)GetValue(WagonInsertedCommandProperty); }
            set { SetValue(WagonInsertedCommandProperty, value); }
        }

        public static readonly DependencyProperty InsertedWagonProperty =
            DependencyProperty.Register("InsertedWagon", typeof(WagonViewModel), typeof(WagonListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public WagonViewModel InsertedWagon
        {
            get { return (WagonViewModel)GetValue(InsertedWagonProperty); }
            set { SetValue(InsertedWagonProperty, value); }
        }

        public static readonly DependencyProperty TargetWagonProperty =
            DependencyProperty.Register("TargetWagon", typeof(WagonViewModel), typeof(WagonListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public WagonViewModel TargetWagon
        {
            get { return (WagonViewModel)GetValue(TargetWagonProperty); }
            set { SetValue(TargetWagonProperty, value); }
        }

        public WagonViewModel movingWagon;

        public WagonListingView()
        {
            InitializeComponent();
        }

        private void Wagon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                WagonViewModel Wagon = (WagonViewModel)frameworkElement.DataContext;
                movingWagon = Wagon;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement,
                    new DataObject(DataFormats.Serializable, Wagon),
                    DragDropEffects.Move);

                if (dragDropResult == DragDropEffects.None)
                {
                    
                    AddWagon(Wagon, false);
                }
            }
        }

        private void Wagon_DragOver(object sender, DragEventArgs e)
        {
            if (WagonInsertedCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement element)
                {
                    TargetWagon = (WagonViewModel)element.DataContext;
                    InsertedWagon = (WagonViewModel)e.Data.GetData(DataFormats.Serializable);

                    WagonInsertedCommand?.Execute(null);
                }
            }
        }

        private void WagonList_DragOver(object sender, DragEventArgs e)
        {
            WagonViewModel Wagon = (WagonViewModel)e.Data.GetData(DataFormats.Serializable);
            AddWagon(Wagon, false);
        }

        private void Wagon_Drop(object sender, MouseEventArgs e)
        {
            // AddWagon(movingWagon, true);
        }

        private void AddWagon(WagonViewModel Wagon, bool isDrop)
        {
            if (WagonDropCommand?.CanExecute(null) ?? false)
            {
                // if(IncomingWagon != Wagon)
                // {
                //    Wagon.wagonCount += 1;
                    IncomingWagon = Wagon;
                    WagonDropCommand?.Execute(isDrop);
                // }
               
            }
        }

        private void WagonList_DragLeave(object sender, DragEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest(lvItems, e.GetPosition(lvItems));

            if (result == null)
            {
                if (WagonRemovedCommand?.CanExecute(null) ?? false)
                {
                    RemovedWagon = (WagonViewModel)e.Data.GetData(DataFormats.Serializable);
                    WagonRemovedCommand?.Execute(null);
                }
            }
        }
    }
}
