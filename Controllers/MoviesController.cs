
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieMagic.Models;
using MovieMagic.Repositories;

namespace MovieMagic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieRepository _ratingRepository;
        private readonly IMovieSearch _searchService;

        public MoviesController(ILogger<MoviesController> logger, IMovieRepository repo, IMovieSearch searchService)
        {
            _logger = logger;
            _ratingRepository = repo;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _ratingRepository.GetAllMovies();
        }

        [HttpGet, Route("{imdbId}")]
        public async Task<MovieDetails> GetMovieById(string imdbId)
        {
            var movieTask = _ratingRepository.GetMovieByExternalId(imdbId);
            var movieDetailTask = _searchService.GetMovieByImdbId(imdbId);

            await Task.WhenAll(movieTask, movieDetailTask);

            var movie = movieTask.Result;
            var movieDetail = movieDetailTask.Result;

            movieDetail.UserRating = movie?.Rating ?? 0;

            // if (movie == null) {
            //     Response.StatusCode = (int) HttpStatusCode.NotFound;
            //     return null;
            // }

            return movieDetail;
        }

        [HttpPost]
        public async Task<Movie> CreateMovie(Movie movie) 
        {
            if (movie == null || !ModelState.IsValid) {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return null;
            }

            var newMovie = await _ratingRepository.CreateMovie(movie);
            return newMovie;
        }

        [HttpPut, Route("{movieId}")]
        public async Task<Movie> UpdateMovie(Movie movie) 
        {
            if (movie == null || !ModelState.IsValid) {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return null;
            }
            
            var newMovie = await _ratingRepository.UpdateMovie(movie);
            return newMovie;
        }

        [HttpDelete, Route("{movieId}")]
        public async Task DeleteMovie(string movieId) {
            await _ratingRepository.DeleteMovie(movieId);
            Response.StatusCode = (int) HttpStatusCode.NoContent;
        }

        [HttpGet("search/{searchKey}")]
        public async Task<IEnumerable<SearchResult>> SearchMovies(string searchKey) {
            return await _searchService.Search(searchKey);
        }
    
    
        // [HttpGet("imdb/{imdbId}")]
        // public async Task<MovieDetails> GetByImdbId(string imdbId) {
        //     return await _searchService.GetMovieByImdbId(imdbId);
        // }
    }
}
