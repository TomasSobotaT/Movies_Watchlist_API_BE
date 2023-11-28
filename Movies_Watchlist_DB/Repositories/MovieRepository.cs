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
    public class MovieRepository<T> :BaseMovieRepository<T>, IMovieRepository<T> where T : BaseEntity
    {
        public MovieRepository(WatchlistDbContext _dbContext) : base(_dbContext)
        { }

    }
}
