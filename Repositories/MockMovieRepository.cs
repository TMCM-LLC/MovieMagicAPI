using System.Collections.Generic;
using System.Linq;
using MovieMagic.Models;

namespace MovieMagic.Repositories
{
    public class MockMovieRepository : IMovieRepository
    {
        private List<Movie> mockMovies;

        public MockMovieRepository() {
            mockMovies = new List<Movie>
            {
                new Movie { Id = "1", Name = "Up", Rating = 4 },
                new Movie { Id = "2", Name = "Jurassic Park", Rating = 5 },
                new Movie { Id = "3", Name = "Back to the Future", Rating = 5 }
            };
        }     

        public Movie CreateMovie(Movie newMovie)
        {
            var maxId = mockMovies.Select(m => m.Id).DefaultIfEmpty().DefaultIfEmpty().Max();
            newMovie.Id = maxId + 1;
            mockMovies.Add(newMovie);
            return newMovie;
        }

        public void DeleteMovie(string movieId)
        {
            var movie = mockMovies.FirstOrDefault(m => m.Id == movieId);
            if (movie != null) {
                mockMovies.Remove(movie);
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return mockMovies;
        }

        public Movie GetMovieById(string movieId)
        {
            return mockMovies.FirstOrDefault(m => m.Id == movieId);
        }

        public Movie UpdateMovie(Movie newMovie)
        {
            var movie = mockMovies.FirstOrDefault(m => m.Id == newMovie.Id);
            if (movie != null) {
                movie.Name = newMovie.Name;
                movie.Rating = newMovie.Rating;
            }
            return movie;
        }
    }
}