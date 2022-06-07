﻿using HCI_Project.Model;
using HCI_Project.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : UserControl
    {
        public MapPage()
        {
            InitializeComponent();
            MapInit();
        }

        private void MapInit()
        {
            myMap.Center = new Location(45.267136, 19.833549); // Novi Sad
            myMap.ZoomLevel = 11;
        }

        private readonly Random rnd = new Random();
        private List<MapPolyline> drawnPolylines = new List<MapPolyline>();

        public void AddPushPins(List<Station> stations, MouseButtonEventHandler MouseRightButtonDownEvent)
        {
            foreach (Station station in stations)
            {
                Pushpin pushpin = new Pushpin
                {
                    Location = station.Position,
                    Content = station.Name
                };
                pushpin.MouseLeftButtonDown += MouseRightButtonDownEvent;
                myMap.Children.Add(pushpin);
            }
        }

        public DraggablePin AddDragablePushPin(string name)
        {
            DraggablePin pin = new DraggablePin(myMap)
            {
                Location = myMap.Center,
                Name = name
            };

            myMap.Children.Add(pin);
            return pin;
        }

        /*public DraggablePin AddDragablePushPinRemoveEverynthingElse()
        {
            myMap.Children.Clear();
            return AddDragablePushPin();
        }*/


        public void DrawMapPolygon(List<Route> routes)
        {
            PickBrush();
            RemoveDrawnPolylines(myMap);
            foreach (Route route in routes)
            {
                MapPolyline polyline = new MapPolyline
                {
                    // Stroke = new SolidColorBrush( Color.FromArgb(255, 255, 139, 0)),
                    Stroke = PickBrush(),
                    Locations = new LocationCollection(),
                    // StrokeEndLineCap = PenLineCap.Triangle
                };
                foreach (Station station in route.Stations)
                {
                    polyline.Locations.Add(station.Position);
                }
                myMap.Children.Add(polyline);
                drawnPolylines.Add(polyline);
            }
        }

        private void RemoveDrawnPolylines(Map myMap)
        {
            foreach (MapPolyline polyline in drawnPolylines)
            {
                myMap.Children.Remove(polyline);
            }
        }

        private Brush PickBrush()
        {
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            Brush result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        
    }
}