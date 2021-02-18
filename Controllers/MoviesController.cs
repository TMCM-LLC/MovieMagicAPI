
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IMovieRepository _movieRepository;

        public MoviesController(ILogger<MoviesController> logger, IMovieRepository repo)
        {
            _logger = logger;
            _movieRepository = repo;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        [HttpGet, Route("{movieId}")]
        public Movie GetMovieById(string movieId)
        {
            var movie = _movieRepository.GetMovieById(movieId);

            if (movie == null) {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            return movie;
        }

        [HttpPost]
        public Movie CreateMovie(Movie movie) 
        {
            if (movie == null || !ModelState.IsValid) {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return null;
            }

            var newMovie = _movieRepository.CreateMovie(movie);
            return newMovie;
        }

        [HttpPut, Route("{movieId}")]
        public Movie UpdateMovie(Movie movie) 
        {
            if (movie == null || !ModelState.IsValid) {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return null;
            }
            
            var newMovie = _movieRepository.UpdateMovie(movie);
            return newMovie;
        }

        [HttpDelete, Route("{movieId}")]
        public void DeleteMovie(string movieId) {
            _movieRepository.DeleteMovie(movieId);
            Response.StatusCode = (int) HttpStatusCode.NoContent;
        }
    }
}
