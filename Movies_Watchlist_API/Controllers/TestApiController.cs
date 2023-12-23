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
    [Route("testApi")]
    public class TestApiController : Controller   
    {
        private readonly IMovieManager<TestMovie, BaseMovieDto> _testMovieManager;
        private readonly IMovieManager<TestDeletedMovie,BaseMovieDto> _testDeletedMovieManager;
        private readonly UserManager<MovieUser> _userManager;


        public TestApiController(
            IMovieManager<TestMovie, BaseMovieDto> movieManager,
            IMovieManager<TestDeletedMovie, BaseMovieDto> deletedMovieManager,
            UserManager<MovieUser> userManager)
        {
            _userManager = userManager;
            _testMovieManager = movieManager;
            _testDeletedMovieManager = deletedMovieManager;
        }

        [Authorize]
        [HttpGet("movies")]
        public async Task<IActionResult> GetAll()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (currentUser is null)
                return NotFound("nepřihášen");


            var movies = _testMovieManager.GetAllMovies(currentUser.Id).ToList();
            var deletedMovies = _testDeletedMovieManager.GetAllMovies(currentUser.Id).ToList();

            var result = new List<List<object>> { movies.Cast<object>().ToList(), deletedMovies.Cast<object>().ToList() };


            return Ok(result);
        }

        [Authorize]
        [HttpDelete("movies/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);


            var movieToDelete = _testMovieManager.GetById(id);
             if (movieToDelete != null)
            {
                _testDeletedMovieManager.InsertMovie(movieToDelete,currentUser);
                _testMovieManager.DeleteMovie(id);
                return Ok();
            }

            return NotFound();       
        }
        [Authorize]

        [HttpPost("movies")]
        public async Task<IActionResult> Add([FromBody] BaseMovieDto movieDto)
        {
            var currentUser = await _userManager.GetUserAsync(User);     

            _testMovieManager.InsertMovie(movieDto,currentUser);
            return Ok();

        }

        [Authorize]
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
