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

        public ApiController(IMovieManager<Movie> movieManager)
        {
            _movieManager = movieManager;
        }

        [HttpGet("movies")]
        public IActionResult GetAll()
        {
            var result = _movieManager.GetAllMovies().ToList();
            if (result is null || result.Count < 1)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("movies/{id}")]
        public IActionResult Delete(int id)
        {
            _movieManager.DeleteMovie(id);
            return Ok();
       
        }

        [HttpPost("movies")]
        public IActionResult Add([FromBody] MovieDto movieDto)
        {


            Movie movie = new Movie { Name = movieDto.Name,PosterUrl=movieDto.posterUrl, CsfdUrl=movieDto.csfdUrl };

            _movieManager.InsertMovie(movie);
            return Ok();

        }

    }
}
