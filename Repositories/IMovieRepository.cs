using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMagic.Models;

namespace MovieMagic.Repositories
{
    public interface IMovieRepository 
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(string movieId);
        Task<Movie> GetMovieByExternalId(string externalId);
        Task<Movie> CreateMovie(Movie newMovie);
        Task<Movie> UpdateMovie(Movie newMovie);
        Task DeleteMovie(string movieId);
    }
}