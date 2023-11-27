using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Movies_Watchlist_API.Interfaces;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Interfaces;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors("AllowHost")]
    public class ApiController : Controller
    {
        private readonly IMovieManager<Movie> _movieManager;
        private readonly IMovieManager<DeletedMovie> _deletedMovieManager;
        public ApiController(IMovieManager<Movie> movieManager, IMovieManager<DeletedMovie> deletedMovieManager)
        {
            _movieManager = movieManager;
            _deletedMovieManager = deletedMovieManager;
        }

        [HttpGet("movies")]
        public IActionResult GetAll()
        {
            var result = _movieManager.GetAllMovies().ToList();
            var result2 = _deletedMovieManager.GetAllMovies().ToList();

            

            var result3 = new List<List<object>> { result.Cast<object>().ToList(), result2.Cast<object>().ToList() };


            return Ok(result3);
        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {

             var movieToDelete = _movieManager.GetById(id);
             if (movieToDelete != null)
            {
                _deletedMovieManager.InsertMovie(new DeletedMovie { Name = movieToDelete.Name, CsfdUrl = movieToDelete.CsfdUrl, PosterUrl = movieToDelete.PosterUrl });
                _movieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();       
        }

        [HttpPost("movies")]
        public IActionResult Add([FromBody] MovieDto movieDto)
        {

            Movie movie = new Movie { Name = movieDto.Name,PosterUrl=movieDto.posterUrl, CsfdUrl=movieDto.csfdUrl };
            _movieManager.InsertMovie(movie);
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
