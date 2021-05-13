using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieMagic.Models;

namespace MovieMagic.Repositories {
    public class OmdbMovieSearch : IMovieSearch
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string baseUrl = "http://www.omdbapi.com/?apikey=";

        public OmdbMovieSearch(IHttpClientFactory httpClient, IConfiguration configuration) {
            _clientFactory = httpClient;
            var apiKey = configuration["OmdbApiKey"];
            baseUrl += apiKey + "&";
        }
        
        public async Task<MovieDetails> GetMovieByImdbId(string imdbId)
        {
            string searchUrl = baseUrl + "i=" + imdbId;

            var client = _clientFactory.CreateClient();

            var resultTask = client.GetStreamAsync(searchUrl);
            var result = await JsonSerializer.DeserializeAsync<OmdbDetail>(await resultTask);

            var movie = new MovieDetails {
                ImdbId = result.ImdbId,
                Title = result.Title,
                Plot = result.Plot,
                Poster = result.Poster,
                Year = result.Year,
                ReleaseDate = result.Released,
                Actors = result.Actors,
                FilmRating = result.Rated,
                Director = result.Director
            };

            foreach (var ratingSource in result.Ratings) {
                switch (ratingSource.Source) {
                    case "Internet Movie Database":
                        movie.ImdbRating = ratingSource.Value;
                        break;
                    case "Rotten Tomatoes":
                        movie.RottenTomatoes = ratingSource.Value;
                        break;
                    case "Metacritic":
                        movie.Metacritic = ratingSource.Value;
                        break;
                }
            }

            return movie;
        }

        public async Task<IEnumerable<SearchResult>> Search(string key)
        {
            string searchUrl = baseUrl + "s=" + key;

            var client = _clientFactory.CreateClient();

            var resultTask = client.GetStreamAsync(searchUrl);
            var result = await JsonSerializer.DeserializeAsync<OmdbSearchResult>(await resultTask);

            return result.Search;
        }
    }
}