using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_DB.Models;
using Movies_Watchlist_DB.Repositories;

namespace Movies_Watchlist_DB.Interfaces
{
    public class MovieManager<T>: IMovieManager<T> where T : BaseEntity
    {
        private readonly IMovieRepository<T> _repository;

        public MovieManager(IMovieRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetAllMovies()
        {
            var movies = _repository.GetAll();

            if (movies is null)
                return Enumerable.Empty<T>();

            return movies;
        
        }


        public void DeleteMovie(int id) {

            var movie = _repository.Get(id);

            if (movie is not null)
                _repository.Delete(movie);

        }


        public void InsertMovie(T movie)
        {
            _repository.Insert(movie);
        }

        public T GetById(int id)
        {
            var movie = _repository.Get(id);

                return movie; ;
        }

        
    }
}
