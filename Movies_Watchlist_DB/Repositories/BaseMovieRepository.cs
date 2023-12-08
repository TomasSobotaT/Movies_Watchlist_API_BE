using Microsoft.EntityFrameworkCore;
using Movies_Watchlist_DB.Data;
using Movies_Watchlist_DB.Models;


namespace Movies_Watchlist_DB.Repositories
{
    public abstract class BaseMovieRepository<T> where T : BaseEntity
    {

        protected WatchlistDbContext _dbContext;
        protected DbSet<T> _dbSet;


        public BaseMovieRepository(WatchlistDbContext dbContext) {

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
     
            _dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public T Get(int id)
        {
            T entity = _dbSet.Find(id);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            var entity = _dbSet.ToArray();
            return entity;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }


    }
}
