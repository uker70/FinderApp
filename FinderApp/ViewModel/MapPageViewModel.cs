using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using FinderApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace FinderApp.ViewModel
{
    class MapPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Timer _updateTimer;

        private Command _changeViewCommand;
        public Command ChangeViewCommand
        {
            get { return _changeViewCommand; }
            set { _changeViewCommand = value; }
        }

        public Map XamarinMap
        {
            get { return AppData.GetMap(); }
            set
            {
                AppData.SetMap(value);
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(nameof(XamarinMap));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private double _distanceToSelectedUser;
        public double DistanceToSelectedUser
        {
            get { return _distanceToSelectedUser; }
            set
            {
                _distanceToSelectedUser = value;
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(nameof(DistanceToSelectedUser));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public MapPageViewModel()
        {
            ChangeViewCommand = new Command(OnChangeView);
            Location location = Geolocation.GetLastKnownLocationAsync().Result;
            XamarinMap.MoveToRegion(new MapSpan(new Position(location.Latitude, location.Longitude), location.Latitude, location.Longitude));

            ApiCommunicator.GenerateTestUsers();
            AppData.SetUser(location.Latitude, location.Longitude);

            _updateTimer = new Timer();
            _updateTimer.Interval = 5000;
            _updateTimer.Elapsed += UpdateNearbyUsers;
            _updateTimer.Start();
            //XamarinMap.Pins.Clear();
            //List<Pin> pins = AppData.GetNearbyUsers();

            //foreach (Pin pin in pins)
            //{
            //    pin.MarkerClicked += OnPinClicked;
            //    XamarinMap.Pins.Add(pin);
            //}
        }

        private async void OnChangeView()
        {
            _updateTimer.Stop();
            AppData.RemoveUser(AppData.GetUser());

            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        // check her for ingen pins på map
        public void UpdateNearbyUsers(object obj, ElapsedEventArgs args)
        {
            XamarinMap.Pins.Clear();
            List<Pin> pins = AppData.GetNearbyUsers();

            foreach (Pin pin in pins)
            {
                //Debug.WriteLine((int)((Pin)obj).MarkerId);
                pin.MarkerClicked += OnPinClicked;
                XamarinMap.Pins.Add(pin);
            }
        }

        public void OnPinClicked(object obj, PinClickedEventArgs args)
        {
            if (((Pin)obj).Label != "You")
            {
                int id = (int)((Pin)obj).MarkerId;
                DistanceToSelectedUser = AppData.GetDistance(id);
            }
        }
    }
}
