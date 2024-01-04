using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooCard.Core.Config
{
    public class HackerNewsSettings
    {
        public string BaseUrl { get; set; }
        public string CacheKey { get; set; }
        public string LastRequestedCountCacheKey { get; set; }
        public int CacheDurationInMinutes { get; set; }
    }
}
