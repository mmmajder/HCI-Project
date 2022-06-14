using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Route
    {
        public long Id { get; set; }
        public List<Station> Stations { get; set; }
        public List<ScheduledRoute> ScheduledRoutes { get; set; }
        public Dictionary<string, double> PriceCatalog { get; set; }
        public string TrainType { get; set; }

        public Route(long id, List<Station> stations, List<ScheduledRoute> scheduledRoutes, string trainType)
        {
            Id = id;
            Stations = stations;
            ScheduledRoutes = scheduledRoutes;
            TrainType = trainType;
            PriceCatalog = new Dictionary<string, double>();
            generateDefaultPriceCatalog();
        }

        private void generateDefaultPriceCatalog()
        {
            Random rand = new Random();

            foreach (Station s in Stations)
            {
                int plusOrMinus = rand.Next(0, 2);
                int randValue = rand.Next(0, 70);

                if (plusOrMinus == 0)
                    PriceCatalog[s.Name] = 100 + randValue;
                else
                    PriceCatalog[s.Name] = 100 - randValue;
            }
        }

        public List<StationPrice> getPrices()
        {
            List <StationPrice> prices = new List<StationPrice>();

            foreach (Station s in Stations)
                prices.Add(new StationPrice(s.Name, PriceCatalog[s.Name]));

            return prices;
        }

        public double getPrice(string stationName)
        {
            if (PriceCatalog.ContainsKey(stationName))
                return PriceCatalog[stationName];
            else
                return 100;
        }

        public double getPrice(string fromName, string toName)
        {
            bool hasComeToFrom = false;
            double price = 0;

            foreach (Station s in Stations)
                if (hasComeToFrom)
                {
                    if (s.Name.Equals(toName))
                        return price;
                    price += getPrice(s.Name);
                }
                else
                    if (s.Name.Equals(fromName))
                    {
                        hasComeToFrom = true;
                        price += getPrice(s.Name);
                    }
                        

            return price;
        }

        public override string ToString()
        {
            string str= Id + " ";

            if (Stations.Count > 0)
                str += Stations[0].Name;

            if (Stations.Count > 2)
            {
                int middleInt = Stations.Count / 2;
                str += "-" + Stations[middleInt].Name;
            }

            if (Stations.Count > 1)
                str += "-" + Stations[Stations.Count-1].Name;

            return str;
        }

        public string getRepeatingDays()
        {
            string str = "";
            List<int> dayInts = new List<int>();

            foreach (ScheduledRoute sc in ScheduledRoutes)
                foreach (int dayId in sc.RepeatigDays)
                    if (!dayInts.Contains(dayId))
                        dayInts.Add(dayId);

            dayInts.Sort();

            foreach (int dayInt in dayInts)
                str += getDay(dayInt) + " ";

            return str;
        }

        private string getDay(int day)
        {
            switch (day)
            {
                case 1: return "Mon";
                case 2: return "Tue";
                case 3: return "Wed";
                case 4: return "Thu";
                case 5: return "Fri";
                case 6: return "Sat";
                case 7: return "Sun";
                default: return "";
            }
        }

        public void updatePrice(string stationName, double newPrice)
        {
            PriceCatalog[stationName] = newPrice;
        }

        //public override string ToString()
        //{
        //    string route = "";
        //    int length = Stations.Count;
        //    for (int i = 0; i < length; i++)
        //    {
        //        if (i == length - 1)
        //        {
        //            route += Stations[i].Name;
        //        }
        //        else
        //        {
        //            route += Stations[i].Name + "-";
        //        }
        //    }
        //    return route;
            
        //}
    }
}
