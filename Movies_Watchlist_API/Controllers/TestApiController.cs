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
        private readonly IMovieManager<TestMovie, BaseMovieDto> _testMovieManager;
        private readonly IMovieManager<TestDeletedMovie,BaseMovieDto> _testDeletedMovieManager;
        public TestApiController(IMovieManager<TestMovie, BaseMovieDto> movieManager, IMovieManager<TestDeletedMovie, BaseMovieDto> deletedMovieManager)
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
                _testDeletedMovieManager.InsertMovie(movieToDelete);
                _testMovieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();       
        }

        [HttpPost("movies")]
        public IActionResult Add([FromBody] BaseMovieDto movieDto)
        {

            _testMovieManager.InsertMovie(movieDto);
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
