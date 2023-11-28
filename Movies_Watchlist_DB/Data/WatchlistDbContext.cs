using Microsoft.EntityFrameworkCore;
using Movies_Watchlist_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Watchlist_DB.Data
{
    public class WatchlistDbContext : DbContext
    {
      
        public DbSet<Movie> Movies { get; set; }
        public DbSet<DeletedMovie> DeletedMovies { get; set; }

        public DbSet<TestMovie> TestMovies { get; set; }
        public DbSet<TestDeletedMovie> TestDeletedMovies { get; set; }
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddTestData(modelBuilder);
        }

        public void AddTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestMovie>().HasData(

                new TestMovie
                {
                    Id = 200,
                    Name= "test",
                    CsfdUrl="test",
                    PosterUrl="test"

                }

                );
        }
    }
}
