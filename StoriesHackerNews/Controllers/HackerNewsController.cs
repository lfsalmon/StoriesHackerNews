using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoriesHackerNews.Services.HackerNews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesHackerNews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly StoriesService _storiesService;
       

        public HackerNewsController(StoriesService storiesService)
        {
            _storiesService = storiesService ?? throw new ArgumentNullException(nameof(storiesService));
        }

        [HttpGet]
        [Route("GetBestStories/{n}")]
        public async Task<IActionResult> GetBestStories( int n)
        {
            var storyIds = await _storiesService.GetBestStoriesIdsAsync();
            var tasks = storyIds.Take(n).Select(id => _storiesService.GetStoryByIdAsync(id));
            var stories = await Task.WhenAll(tasks);

            return Ok(stories.OrderByDescending(story => story.Score));
        }
    }
}
