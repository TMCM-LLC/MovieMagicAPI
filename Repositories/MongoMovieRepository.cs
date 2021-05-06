using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MovieMagic.Models;

namespace MovieMagic.Repositories 
{
    public class MongoMovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movies;

        public MongoMovieRepository(IMovieDatabaseSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _movies = database.GetCollection<Movie>(settings.MovieCollectionName);
        }

        public async Task<Movie> CreateMovie(Movie newMovie)
        {
            await _movies.InsertOneAsync(newMovie);
            return newMovie;
        }

        public async Task DeleteMovie(string movieId)
        {
            await _movies.DeleteOneAsync(movie => movie.Id == movieId);
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = await _movies.FindAsync(movie => true);
            return movies.ToList();
        }

        public async Task<Movie> GetMovieById(string movieId)
        {
            var movies = await _movies.FindAsync<Movie>(m => m.Id == movieId);
            return movies.FirstOrDefault();
        }

        public async Task<Movie> GetMovieByExternalId(string externalId)
        {
            return (await _movies.FindAsync<Movie>(m => m.ExternalId == externalId)).FirstOrDefault();
        }

        public async Task<Movie> UpdateMovie(Movie newMovie)
        {
            await _movies.ReplaceOneAsync(movie => movie.Id == newMovie.Id, newMovie);
            return newMovie;
        }
    }
}