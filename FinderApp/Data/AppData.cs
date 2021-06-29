using System;
using System.Collections.Generic;
using System.Text;
using FinderApp.Class;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace FinderApp.Data
{
    class AppData
    {
        //hahahah get it??? its called app data because its the data for the app hahahahah

        private static Map _xamarinMap = new Map();
        private static List<User> _nearbyUsers = new List<User>();
        private static List<Filter> _filters = new List<Filter>()
        {
            new Filter("Mentally ill"), new Filter("Dumbass"), new Filter("Monke"), new Filter("Donkey"),
            new Filter("Pyromaniac"), new Filter("Kiddie Fiddler"), new Filter("Gamer"), new Filter("Lukas")
        };
        private static User _user;

        public static void SetUser( double latitude, double longitude)
        {
            int id = ApiCommunicator.InsertUser(latitude, longitude);
            _user = new User(ConsoleColor.Yellow, latitude, longitude, id);
        }

        public static int GetUser()
        {
            return _user.GetId();
        }

        public static void RemoveUser(int id)
        {
            ApiCommunicator.DeleteUser(id);
            _user = null;
        }

        public static Map GetMap()
        {
            return _xamarinMap;
        }

        public static void SetMap(Map mapValue)
        {
            _xamarinMap = mapValue;
        }

        public static List<string> GetFilters()
        {
            List<string> returnList = new List<string>();
            foreach (Filter filter in _filters)
            {
                returnList.Add(filter.GetName());
            }
            return returnList;
        }

        // check her for ingen pins på map
        public static List<Pin> GetNearbyUsers()
        {
            ApiCommunicator.GenerateTestUsers();
            _nearbyUsers.Clear();
            _nearbyUsers = ApiCommunicator.GetNearbyUsers(new Location(_user.GetPosition()[0], _user.GetPosition()[1]));
            _nearbyUsers.Add(_user);

            List<Pin> returnList = new List<Pin>();

            foreach (User nearbyUser in _nearbyUsers)
            {
                if (nearbyUser.GetId() == _user.GetId())
                {
                    returnList.Add(new Pin
                    {
                        Label = "You",
                        Position = new Position(nearbyUser.GetPosition()[0], nearbyUser.GetPosition()[1]),
                        MarkerId = nearbyUser.GetId()
                    });
                }
                else
                {
                    returnList.Add(new Pin
                    {
                        Label = "User",
                        Position = new Position(nearbyUser.GetPosition()[0], nearbyUser.GetPosition()[1]),
                        MarkerId = nearbyUser.GetId()
                    });
                }
            }

            return returnList;
        }

        public static double GetDistance(int clickedUserId)
        {
            double distanceValue = 0;

            foreach (User nearbyUser in _nearbyUsers)
            {
                if (nearbyUser.GetId() == clickedUserId)
                {
                    distanceValue = Location.CalculateDistance(_user.GetPosition()[0], _user.GetPosition()[1],
                        nearbyUser.GetPosition()[0], nearbyUser.GetPosition()[1], DistanceUnits.Kilometers);
                }
            }

            return distanceValue;
        }
    }
}