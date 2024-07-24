using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StoriesHackerNews.Controllers;
using StoriesHackerNews.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace StoriesHackerNews.Services.HackerNews
{
    public class StoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HackerNewsController> _logger;
        public StoriesService(HttpClient httpClient, IMemoryCache memoryCache, ILogger<HackerNewsController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int[]> GetBestStoriesIdsAsync()
        {
            _logger.LogInformation("Validate if is in cache");
            if (!_memoryCache.TryGetValue("BestStoriesIds", out int[] storyIds))
            {
                _logger.LogInformation("Getting the BestStories of the HackerNew site");
                var response= await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                if (response!=null)
                {
                    storyIds = JsonSerializer.Deserialize<int[]>(response);

                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(40)
                    };
                    _memoryCache.Set("BestStoriesIds", storyIds, cacheEntryOptions);
                }
              
            }
            return storyIds;
        }
        public async Task<HackerNewsDto> GetStoryByIdAsync(int id)
        {
            _logger.LogInformation($"Validate if is in cache the storie {id}");
            if (!_memoryCache.TryGetValue($"Story_{id}", out HackerNewsDto story))
            {
                _logger.LogInformation($"Getting storie {id}");
                var response = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                if (response != null)
                {
                    var stories= JsonSerializer.Deserialize<StoriesItemHttpResponse>(response);
                    story = new HackerNewsDto
                    {
                        PostedBy= stories.by,
                        CommentCount=stories.kids.Count,
                        Score=stories.score,
                        Time=stories.GetTimeAsDateTime(),
                        Title=stories.title,
                        Uri = stories.url
                    };
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(40) 
                    };
                    _memoryCache.Set($"Story_{id}", story, cacheEntryOptions);
                }


               
            }
            return story;
        }
    }
}
