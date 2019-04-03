using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheMovieDB.Models
{
    public class Detail
    {
        public string Title { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string PosterPathUrl { get => "https://image.tmdb.org/t/p/w500" + PosterPath; }
        public List<Genre> Genres { get; set; }
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
