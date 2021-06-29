using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FinderApp.Class;
using FinderApp.Data;
using FinderApp.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinderApp.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Command _changeViewCommand;
        public Command ChangeViewCommand
        {
            get { return _changeViewCommand; }
            set { _changeViewCommand = value; }
        }

        public List<string> Filters
        {
            get { return AppData.GetFilters(); }
        }

        private string _selectedFilter;
        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(nameof(SelectedFilter));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public MainPageViewModel()
        {
            ChangeViewCommand = new Command(OnChangeView);
        }

        private async void OnChangeView()
        {
            MapPageView pageView = new MapPageView();
            pageView.BindingContext = new MapPageViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(pageView);
        }
    }
}
