using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Interfaces
{
    public interface IMovieManager<T,U> where T : BaseEntity where U : BaseMovieDto
    {
        void DeleteMovie(int id);
        IEnumerable<U> GetAllMovies();
        U GetById(int id);
        void InsertMovie(U movie);
    }
}
