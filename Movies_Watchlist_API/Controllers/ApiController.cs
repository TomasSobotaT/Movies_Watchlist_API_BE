using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies_Watchlist_API.Extensions;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Controllers
{
    [EnableCors("MyCorsPolicy")] 
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public class ApiController : Controller
    {
        private readonly IMovieManager<Movie,BaseMovieDto> _movieManager;
        private readonly IMovieManager<DeletedMovie, BaseMovieDto> _deletedMovieManager;



        public ApiController(
            IMovieManager<Movie, BaseMovieDto> movieManager,
            IMovieManager<DeletedMovie, BaseMovieDto> deletedMovieManager)
        {
            
            _movieManager = movieManager;
            _deletedMovieManager = deletedMovieManager;
        }

        [HttpGet("movies")]
        public IActionResult GetAll()
        {
            try
            {
                var moviesToWatch = _movieManager.GetAllMovies().ToList();
                var moviesWatched = _deletedMovieManager.GetAllMovies().ToList();

                var result = new List<List<object>> { moviesToWatch.Cast<object>().ToList(), moviesWatched.Cast<object>().ToList() };


                return Ok(result);
            }
            catch(Exception e)
            { 
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {

             var movieToDelete = _movieManager.GetById(id);
             if (movieToDelete != null)
            {
                _deletedMovieManager.InsertMovie(movieToDelete);
                _movieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();       
        }

        [HttpPost("movies")]
        public IActionResult Add([FromBody] BaseMovieDto movieDto)
        {
            _movieManager.InsertMovie(movieDto);
            return Ok();

        }

        [HttpDelete("deletedMovies/{id}")]
        public IActionResult Delete2(int id)
        {

            var movieToDelete = _deletedMovieManager.GetById(id);
            if (movieToDelete != null)
            {
                _deletedMovieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();
        }


    }
}
