using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Threading.Tasks;
using TheMovieDB.Models;
using TheMovieDB.Services;

namespace TheMovieDB.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        IPageDialogService _dialogService;

        private Detail _movie;
        public Detail Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        public DetailsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) 
            : base(navigationService)
        {
            _dialogService = dialogService;

            Title = "Details";
            _movie = new Detail();
            _isLoading = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.Count > 0 && parameters.ContainsKey("id"))
            {
                if (IsConnected)
                {
                    IsLoading = true;

                    int movieId = Convert.ToInt32(parameters["id"]);

                    var api = RestService.For<IServiceApi>(EndPoints.BaseUrl);
                    var retorno = await api.GetDetail(movieId, "1f54bd990f1cdfb230adb312546d765d", "en-US");

                    if (retorno != null)
                        Movie = retorno;

                    IsLoading = false;
                }
            }
        }
    }
}
