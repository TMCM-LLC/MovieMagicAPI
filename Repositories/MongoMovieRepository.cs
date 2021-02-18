using System.Collections.Generic;
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

        public Movie CreateMovie(Movie newMovie)
        {
            _movies.InsertOne(newMovie);
            return newMovie;
        }

        public void DeleteMovie(string movieId)
        {
            _movies.DeleteOne(movie => movie.Id == movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movies.Find(movie => true).ToList();
        }

        public Movie GetMovieById(string movieId)
        {
            return _movies.Find<Movie>(m => m.Id == movieId).FirstOrDefault();
        }

        public Movie UpdateMovie(Movie newMovie)
        {
            _movies.ReplaceOne(movie => movie.Id == newMovie.Id, newMovie);
            return newMovie;
        }
    }
}