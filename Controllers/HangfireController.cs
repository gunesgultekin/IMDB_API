using Hangfire;
using IMDB_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {
        private IMovieServiceRepository _movieService;
        public HangfireController(IMovieServiceRepository movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("schedulePopularityHandler")]
        public async Task<ActionResult> schedulePopularityHandler ()
        {
            try
            {
                IRecurringJobManager manager = new RecurringJobManager();
                manager.AddOrUpdate("scheduledPopularityHandler", () => _movieService.handlepopularities(), Cron.Daily);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

    }
}
