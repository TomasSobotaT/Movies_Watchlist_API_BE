using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_DB.Models;
using Movies_Watchlist_DB.Repositories;

namespace Movies_Watchlist_DB.Interfaces
{
    public class DeletedMovieManager: IMovieManager<DeletedMovie>
    {
        private readonly IMovieRepository<DeletedMovie> _repository;

        public DeletedMovieManager(IMovieRepository<DeletedMovie> repository)
        {
            _repository = repository;
        }

        public IEnumerable<DeletedMovie> GetAllMovies()
        {
            var movies = _repository.GetAll();

            if (movies is null)
                return Enumerable.Empty<DeletedMovie>();

            return movies;
        
        }


        public void DeleteMovie(int id) {

            var movie = _repository.Get(id);

            if (movie is not null)
                _repository.Delete(movie);

        }


        public void InsertMovie(DeletedMovie movie)
        {
            _repository.Insert(movie);
        }

        public DeletedMovie GetById(int id)
        {
            return _repository.Get(id);
        }
    }
}
