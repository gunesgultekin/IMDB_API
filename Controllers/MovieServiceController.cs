using IMDB_API.Context;
using IMDB_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieServiceController : ControllerBase
    {
        private IMovieServiceRepository _movieService;
        public MovieServiceController(IMovieServiceRepository movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("topTen")]
        public async Task<ActionResult<List<IMDB_MOVIES>>> topTen()
        {
            try
            {
                return Ok(await _movieService.getTen());

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }         
        }

        [HttpGet("getMovie")]
        public async Task <ActionResult<IMDB_MOVIES>> getMovie(string title)
        {
            try
            {
                return Ok(await _movieService.getMovie(title));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("rate")]
        public async Task<ActionResult<int>> rateMovie([FromBody] RatingModel model)
        {
            try
            {
                return Ok(await _movieService.rateMovie(model));

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
