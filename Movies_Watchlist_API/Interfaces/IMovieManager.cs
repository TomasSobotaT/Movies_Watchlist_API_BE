using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Interfaces
{
    public interface IMovieManager<T> where T : BaseEntity
    {
        void DeleteMovie(int id);
        IEnumerable<T> GetAllMovies();
        T GetById(int id);
        void InsertMovie(T movie);
    }
}
