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
    public class DeletedMovieRepository : IMovieRepository<DeletedMovie>
    {
        private WatchlistDbContext _dbContext;
        public DeletedMovieRepository(WatchlistDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(DeletedMovie entity)
        {
            _dbContext.DeletedMovies.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _dbContext.Movies.Any(x => x.Id == id);
        }

        public DeletedMovie Get(int id)
        {
            var movie = _dbContext.DeletedMovies.FirstOrDefault(x => x.Id == id);
            return movie;
        }

        public IEnumerable<DeletedMovie> GetAll()
        {
            var movies = _dbContext.DeletedMovies.ToArray();
            return movies;
        }

        public void Insert(DeletedMovie entity)
        {
            _dbContext.DeletedMovies.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
