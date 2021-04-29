using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMagic.Models;

namespace MovieMagic.Repositories
{
    public interface IMovieSearch
    {
        Task<IEnumerable<SearchResult>> Search(string key);
        Task<MovieDetails> GetMovieByImdbId(string imdbId);
    }
}