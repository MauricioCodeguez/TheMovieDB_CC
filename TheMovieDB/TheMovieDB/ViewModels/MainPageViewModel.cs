using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMovieDB.Models;
using TheMovieDB.Services;

namespace TheMovieDB.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        IPageDialogService _dialogService;

        private List<UpcomingInfo> _upcomingInfoList;
        public List<UpcomingInfo> UpcomingInfoList
        {
            get { return _upcomingInfoList; }
            set { SetProperty(ref _upcomingInfoList, value); }
        }

        private List<UpcomingInfo> _upcomingInfoListFiltered;
        public List<UpcomingInfo> UpcomingInfoListFiltered
        {
            get { return _upcomingInfoListFiltered; }
            set { SetProperty(ref _upcomingInfoListFiltered, value); }
        }

        private UpcomingInfo _selectedMovie;
        public UpcomingInfo SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                if (SetProperty(ref _selectedMovie, value))
                {
                    if (_selectedMovie == null)
                        return;

                    NavigationService.NavigateAsync($"DetailsPage?id={_selectedMovie.Id}");
                    _selectedMovie = null;
                }
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private bool _isLoadingList;
        public bool IsLoadingList
        {
            get { return _isLoadingList; }
            set { SetProperty(ref _isLoadingList, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _dialogService = dialogService;

            Title = "Upcoming Movies";
            _isLoading = false;
            _upcomingInfoList = new List<UpcomingInfo>();
            _selectedMovie = new UpcomingInfo();
            _isLoadingList = false;

            LoadUpcomingMovies(1);
        }

        public async void LoadUpcomingMovies(int page)
        {
            if (IsConnected)
            {
                if (page == 1)
                {
                    IsLoading = true;

                    var retorno = await Get(1);

                    if (retorno != null)
                        UpcomingInfoList = retorno.Results;

                    Filter();
                    IsLoading = false;
                }
                else
                {
                    IsLoadingList = true;

                    var retorno = await Get(page);

                    if (retorno != null)
                    {
                        var listaAux = new List<UpcomingInfo>();

                        foreach (var item in UpcomingInfoList)
                            listaAux.Add(item);

                        foreach (var item in retorno.Results)
                            listaAux.Add(item);

                        UpcomingInfoList.Clear();
                        UpcomingInfoList = listaAux;
                    }

                    Filter();
                    IsLoadingList = false;
                }
            }
        }

        private async Task<Upcoming> Get(int page)
        {
            var api = RestService.For<IServiceApi>(EndPoints.BaseUrl);
            var retorno = await api.GetUpcomingMovies("1f54bd990f1cdfb230adb312546d765d", "en-US", page);

            return retorno;
        }

        public void Filter(string filter = "")
        {
            UpcomingInfoListFiltered = UpcomingInfoList;

            if (!string.IsNullOrEmpty(filter))
                UpcomingInfoListFiltered = UpcomingInfoList.Where(a => a.Title.ToLower().Contains(filter.ToLower())).ToList();
        }

    }
}
