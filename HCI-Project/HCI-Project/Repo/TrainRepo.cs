using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class TrainRepo
    {
        private static Dictionary<string, List<Wagon>> TrainTypeMap = new Dictionary<string, List<Wagon>>();

        public TrainRepo()
        {
            List<Wagon> allWagons = WagonRepo.GetWagons();

            List<Wagon> type1Wagons = new List<Wagon> { allWagons[0], allWagons[0], allWagons[0], allWagons[0] };
            List<Wagon> type2Wagons = new List<Wagon> { allWagons[0], allWagons[0], allWagons[0], allWagons[0] };
            AddTrainType("Type 1", type1Wagons);
            AddTrainType("Type 2", type2Wagons);
        }

        internal static List<string> GetTrainTypes()
        {
            return TrainTypeMap.Keys.ToList();
        }

        public static void AddTrainType(string TrainTypeName, List<Wagon> wagons)
        {
            TrainTypeMap.Add(TrainTypeName, wagons);
        }


        public static List<string> GetTrainTypeNames()
        {
            return TrainTypeMap.Keys.ToList();
        }
    }
}
