using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheMovieDB.Models
{
    public class Upcoming
    {
        public int Page { get; set; }
        public List<UpcomingInfo> Results { get; set; }
        public Date Dates { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }

    public class UpcomingInfo
    {
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string PosterPathUrl { get => "https://image.tmdb.org/t/p/w500" + PosterPath; }
        public bool? Adult { get; set; }
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }
        public int Id { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        public string Title { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        public string BackdropPathUrl { get => "https://image.tmdb.org/t/p/w500" + BackdropPath; }
        public decimal Popularity { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        public bool? Video { get; set; }
        [JsonProperty("vote_average")]
        public decimal VoteAverage { get; set; }
        public decimal VoteAverageConverted { get => VoteAverage / 2; }
    }

    public class Date
    {
        public string Maximum { get; set; }
        public string Minimum { get; set; }
    }
}
