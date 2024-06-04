using IMDB_API.Context;
using IMDB_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService;
        public SearchController (ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("genericSearch")]
        public async Task<ActionResult<(List<IMDB_MOVIES> , List<IMDB_DIRECTORS>)>> genericSearch(string keyword)
        {
            try
            {
                return Ok(await _searchService.genericSearch(keyword));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("titleSearch")]
        public async Task<ActionResult<List<IMDB_MOVIES>>> titleSearch(string keyword)
        {
            try
            {
                return Ok(await _searchService.titleSearch(keyword));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("actorSearch")]
        public async Task<ActionResult<List<IMDB_MOVIES>>> actorSearch(string keyword)
        {
            try
            {
                return Ok(await _searchService.actorSearch(keyword));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("directorSearch")]
        public async Task<ActionResult<List<IMDB_MOVIES>>> directorSearch(string keyword)
        {
            try
            {
                return Ok(await _searchService.directorSearch(keyword));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
