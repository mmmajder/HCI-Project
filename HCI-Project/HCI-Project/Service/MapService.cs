using HCI_Project.Model;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HCI_Project.Service
{
    public class MapService
    {
        private static readonly Random rnd = new Random();
        private static List<MapPolyline> drawnPolylines = new List<MapPolyline>();

        public static void AddPushPins(List<Station> stations, MouseButtonEventHandler MouseRightButtonDownEvent, Map myMap)
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


        public static void DrawMapPolygon(List<Route> routes, Map myMap)
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

        private static void RemoveDrawnPolylines(Map myMap)
        {
            foreach (MapPolyline polyline in drawnPolylines)
            {
                myMap.Children.Remove(polyline);
            }
        }

        private static Brush PickBrush()
        {
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            Brush result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
    }
}
