using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FinderApp.Class;
using Xamarin.Essentials;

namespace FinderApp.Data
{
    class ApiCommunicator
    {
        //make api call work
        public static int InsertUser(double latitude, double longitude)
        {
            int returnValue = 0;

            /*
             * InsertUser with values latitude and longitude which gets the id of the new user back
             */

            _testUserDB.Add(new User(ConsoleColor.Yellow, latitude, longitude, _testUserDB[_testUserDB.Count-1].GetId()+1));

            return returnValue;
        }

        public static void DeleteUser(int id)
        {
            /*
             * DeleteUser where id matches
             */

            User user = _testUserDB.Find(x => x.GetId() == id);
            _testUserDB.Remove(user);
        }

        public static List<string[]> GetUsersWithinDistance(int id, int distance)
        {
            List<string[]> returnList = new List<string[]>();

            /*
             * GetWithinDistance with id and where distance is closer than the provided amount
             */



            return returnList;
        }

        public static double GetDistanceToUser(int id, int selectedUserId)
        {
            double returnValue = 0;

            /*
             * GetDistance from the current user to the selected user with the 2 ids
             */

            return returnValue;
        }

        private static List<User> _testUserDB = new List<User>();

        // check her for ingen pins på map
        public static List<User> GetNearbyUsers(Location location)
        {
            List<User> returnList = new List<User>();

            foreach (User user in _testUserDB)
            {
                if (Location.CalculateDistance(location, user.GetPosition()[0], user.GetPosition()[1],
                    DistanceUnits.Kilometers) < 500)
                {
                    string test = Location.CalculateDistance(location, user.GetPosition()[0], user.GetPosition()[1],
                        DistanceUnits.Kilometers).ToString();
                    returnList.Add(user);
                }
            }

            return returnList;
        }

        // check her for ingen pins på map
        public static void GenerateTestUsers()
        {
            _testUserDB.Clear();
            Random rng = new Random();

            for (int i = 0; i < 500; i++)
            {
                _testUserDB.Add(new User(ConsoleColor.Cyan, (rng.Next(-8500000, 8500001)/ 100000.00), (rng.Next(-18000000, 18000001) / 100000.00), i));
            }
        }
    }
}
