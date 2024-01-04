using BambooCard.Core.Config;
using BambooCard.Core.Interfaces;
using BambooCard.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BambooCard.Infrastructure.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly HackerNewsSettings _settings;

        public HackerNewsService(HttpClient httpClient, IMemoryCache cache, IOptions<HackerNewsSettings> settings)
        {
            _httpClient = httpClient;
            _cache = cache;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<HackerNewsStory>> GetTopStoriesAsync(int numberOfStories)
        {
            var cacheKey = _settings.CacheKey;

            // Check if stories are cached
            if (!_cache.TryGetValue<IEnumerable<HackerNewsStory>>(cacheKey, out var stories) ||
                numberOfStories > _cache.Get<int>(_settings.LastRequestedCountCacheKey))
            {
                // If not cached or the new count is greater than the last count, fetch story IDs and details from Hacker News API
                var storyIds = await GetBestStoryIdsAsync();
                stories = await FetchAndSortStoriesAsync(storyIds, numberOfStories);

                // Cache the stories and update the last requested count
                _cache.Set(cacheKey, stories, TimeSpan.FromMinutes(_settings.CacheDurationInMinutes));
                _cache.Set(_settings.LastRequestedCountCacheKey, numberOfStories);
            }

            return stories.Take(numberOfStories);
        }

        private async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("beststories.json");
                return JsonConvert.DeserializeObject<IEnumerable<int>>(response);
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP request specific exceptions, log it if necessary
                throw new Exception("Error fetching best story IDs from Hacker News API.", httpEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions, log it if necessary
                throw new Exception("An error occurred while processing best story IDs.", ex);
            }
        }

        private async Task<IEnumerable<HackerNewsStory>> FetchAndSortStoriesAsync(IEnumerable<int> storyIds, int numberOfStories)
        {
            var tasks = storyIds.Take(numberOfStories)
                               .Select(id => GetStoryAsync(id));

            try
            {
                var fetchedStories = await Task.WhenAll(tasks);
                return fetchedStories.OrderByDescending(s => s.Score);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while fetching and sorting Hacker News stories.", ex);
            }
        }

        private async Task<HackerNewsStory> GetStoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"item/{id}.json");
                var storyData = JsonConvert.DeserializeObject<dynamic>(response);

                return new HackerNewsStory
                {
                    Title = storyData.title,
                    Uri = storyData.url,
                    PostedBy = storyData.by,
                    Time = DateTimeOffset.FromUnixTimeSeconds((long)storyData.time).DateTime,
                    Score = storyData.score,
                    CommentCount = storyData.descendants
                };
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP request specific exceptions, log it if necessary
                throw new Exception($"Error fetching story with ID {id} from Hacker News API.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON parsing errors
                throw new Exception($"Error parsing the JSON response for story ID {id}.", jsonEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions, log it if necessary
                throw new Exception($"An error occurred while processing the story ID {id}.", ex);
            }
        }
    }
}
