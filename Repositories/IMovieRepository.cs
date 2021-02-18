using System.Collections.Generic;
using MovieMagic.Models;

namespace MovieMagic.Repositories
{
    public interface IMovieRepository 
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(string movieId);
        Movie CreateMovie(Movie newMovie);
        Movie UpdateMovie(Movie newMovie);
        void DeleteMovie(string movieId);
    }
}