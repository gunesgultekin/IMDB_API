using IMDB_API.Context;
using IMDB_API.Interfaces;
using IMDB_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserServiceController : ControllerBase
    {
        private IUserServiceRepository _userServiceRepository;

        public UserServiceController(IUserServiceRepository userServiceRepository)
        {
            _userServiceRepository = userServiceRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<long>> register([FromBody] IMDB_USERS user)
        {
            try
            {
                await _userServiceRepository.register(user);
                return Ok(user.id);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> login([FromBody] LoginModel user)
        {
            try
            {
                string response = await _userServiceRepository.login(user);
                if (response == "" || response == null)
                {
                    return StatusCode(404,"Incorrect Credentials");

                }

                else
                {
                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                return BadRequest();

            }
        }

        [HttpGet("addToWatchlist")]
        public async Task<ActionResult<int>> addToWatchlist(string username, string title)
        {
            try
            {
                return Ok(await _userServiceRepository.addToWatchlist(username, title));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet("myWatchlist")]
        public async Task<ActionResult<List<IMDB_MOVIES>>> myWatchlist(string username)
        {
            try
            {
                return Ok(await _userServiceRepository.myWatchlist(username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\n\n"+ ex.InnerException);
            }

        }

        [HttpGet("removeFromWatchlist")]
        public async Task<ActionResult<int>> removeFromWatchlist(string username, string title)
        {
            try
            {
                return Ok(await _userServiceRepository.removeFromWatchlist(username,title));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\n\n" + ex.InnerException);
            }

        }
    }
}
