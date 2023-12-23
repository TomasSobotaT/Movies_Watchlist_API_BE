using AutoMapper;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;


namespace Movies_Watchlist_API_Managers
{
    public class MovieManager<T, U> : IMovieManager<T, U> where T : BaseEntity where U : BaseMovieDto
    {
        private readonly IMovieRepository<T> _repository;
        private IMapper _mapper;

        public MovieManager(IMovieRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<U> GetAllMovies()
        {
            var movies = _repository.GetAll();

            if (movies is null)
                return Enumerable.Empty<U>();

            var moviesDto = _mapper.Map<IEnumerable<U>>(movies);

            return moviesDto;

        }
        public IEnumerable<U> GetAllMovies(string id)
        {
            var movies = _repository.GetAll(id);

            if (movies is null)
                return Enumerable.Empty<U>();

            var moviesDto = _mapper.Map<IEnumerable<U>>(movies);

            return moviesDto;

        }

        public void DeleteMovie(int id)
        {
            var movie = _repository.Get(id);

            if (movie is not null)
                _repository.Delete(movie);
        }


        public void InsertMovie(U movieDto)
        {
            movieDto.Id = default;
            var movie = _mapper.Map<T>(movieDto);

            _repository.Insert(movie);
        }

        public void InsertMovie(U movieDto, MovieUser user)
        {
            movieDto.Id = default;
            var movie = _mapper.Map<T>(movieDto);

            movie.movieUser = user; 
            _repository.Insert(movie);
        }


        public U GetById(int id)
        {
            var movie = _repository.Get(id);
            var movieDto = _mapper.Map<U>(movie);

            return movieDto;
        }

    }
}