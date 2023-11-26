using Movies_Watchlist_DB.Data;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Watchlist_DB.Repositories
{
    public class MovieRepository : IMovieRepository<Movie>
    {
        private WatchlistDbContext _dbContext;
        public MovieRepository(WatchlistDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(Movie entity)
        {
            _dbContext.Movies.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _dbContext.Movies.Any(x => x.Id == id);
        }

        public Movie Get(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);
            return movie;
        }

        public IEnumerable<Movie> GetAll()
        {
            var movies = _dbContext.Movies.ToArray();
            return movies;
        }

        public void Insert(Movie entity)
        {
            _dbContext.Movies.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
