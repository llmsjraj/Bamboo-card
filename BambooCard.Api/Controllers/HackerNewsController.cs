using BambooCard.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BambooCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsApplicationService _hackerNewsApplicationService;

        public HackerNewsController(HackerNewsApplicationService hackerNewsApplicationService)
        {
            _hackerNewsApplicationService = hackerNewsApplicationService;
        }

        [HttpGet("topstories/{count}")]
        public async Task<IActionResult> GetTopStories(int count)
        {
            try
            {
                var stories = await _hackerNewsApplicationService.GetTopStoriesAsync(count);
                return Ok(stories);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
