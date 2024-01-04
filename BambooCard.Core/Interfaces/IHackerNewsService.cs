using BambooCard.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooCard.Core.Interfaces
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<HackerNewsStory>> GetTopStoriesAsync(int numberOfStories);
    }
}
