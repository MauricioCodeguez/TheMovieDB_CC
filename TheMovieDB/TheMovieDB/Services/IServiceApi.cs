using Refit;
using System.Threading.Tasks;
using TheMovieDB.Models;

namespace TheMovieDB.Services
{
    public interface IServiceApi
    {
        [Get("/3/movie/upcoming?api_key={apiKey}&language={language}&page={page}")]
        Task<Upcoming> GetUpcomingMovies(string apiKey, string language, int page);

        [Get("/3/movie/{movieId}?api_key={apiKey}&language={language}")]
        Task<Detail> GetDetail(int movieId, string apiKey, string language);
    }
}
