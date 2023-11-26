using Movies_Watchlist_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Watchlist_DB.Interfaces
{
    public interface IMovieRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Delete(T entity);

        bool Exists(int id);

    }
}
