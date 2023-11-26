using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_DB.Models;
using Movies_Watchlist_DB.Repositories;

namespace Movies_Watchlist_DB.Interfaces
{
    public class MovieManager: IMovieManager<Movie>
    {
        private readonly IMovieRepository<Movie> _repository;

        public MovieManager(IMovieRepository<Movie> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _repository.GetAll();

            if (movies is null)
                return Enumerable.Empty<Movie>();

            return movies;
        
        }


        public void DeleteMovie(int id) {

            var movie = _repository.Get(id);

            if (movie is not null)
                _repository.Delete(movie);

        }


        public void InsertMovie(Movie movie)
        {
            _repository.Insert(movie);
        }


    }
}
