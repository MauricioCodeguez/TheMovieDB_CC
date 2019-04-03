using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Models;
using TheMovieDB.ViewModels;
using Xamarin.Forms;

namespace TheMovieDB.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _vm;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (_vm == null)
                _vm = (MainPageViewModel)this.BindingContext;

            if (_vm != null)
            {
                if (_vm.IsLoading || _vm.UpcomingInfoList.Count == 0 || _vm.IsLoadingList || !string.IsNullOrEmpty(search.Text))
                    return;

                var item = (e.Item as UpcomingInfo);

                if (item == _vm.UpcomingInfoList[_vm.UpcomingInfoList.Count - 1])
                    _vm.LoadUpcomingMovies(((_vm.UpcomingInfoList.Count / 20) + 1));
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_vm == null)
                _vm = (MainPageViewModel)this.BindingContext;

            if (_vm != null)
                _vm.Filter(search.Text);
        }
    }
}