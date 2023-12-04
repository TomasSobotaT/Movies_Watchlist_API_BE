using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Movies_Watchlist_API.Extensions;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Controllers
{
    [ApiController]
    [Route("testApi")]
    [EnableCors("AllowTestHost")]
    public class TestApiController : Controller    {
        private readonly IMovieManager<TestMovie> _testMovieManager;
        private readonly IMovieManager<TestDeletedMovie> _testDeletedMovieManager;
        public TestApiController(IMovieManager<TestMovie> movieManager, IMovieManager<TestDeletedMovie> deletedMovieManager)
        {
            _testMovieManager = movieManager;
            _testDeletedMovieManager = deletedMovieManager;
        }

        [HttpGet("movies")]
        public IActionResult GetAll()
        {
            var movies = _testMovieManager.GetAllMovies().ToList();
            var deletedMovies = _testDeletedMovieManager.GetAllMovies().ToList();

            var result = new List<List<object>> { movies.Cast<object>().ToList(), deletedMovies.Cast<object>().ToList() };


            return Ok(result);
        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {

             var movieToDelete = _testMovieManager.GetById(id);
             if (movieToDelete != null)
            {
                _testDeletedMovieManager.InsertMovie(
                    new TestDeletedMovie {
                        Name = movieToDelete.Name, 
                        CsfdUrl = movieToDelete.CsfdUrl, 
                        PosterUrl = movieToDelete.PosterUrl, 
                        DateAdded = movieToDelete.DateAdded, 
                        DateWatched = DateTime.Now });

                _testMovieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();       
        }

        [HttpPost("movies")]
        public IActionResult Add([FromBody] MovieDto movieDto)
        {

            TestMovie movie = new TestMovie { Name = movieDto.Name.EditMovieName(),PosterUrl=movieDto.posterUrl, CsfdUrl=movieDto.csfdUrl };
            _testMovieManager.InsertMovie(movie);
            return Ok();

        }

        [HttpDelete("deletedMovies/{id}")]
        public IActionResult Delete2(int id)
        {

            var movieToDelete = _testDeletedMovieManager.GetById(id);
            if (movieToDelete != null)
            {
                _testDeletedMovieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();
        }


    }
}
