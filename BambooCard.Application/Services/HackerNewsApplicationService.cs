using AutoMapper;
using BambooCard.Application.DTOs;
using BambooCard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooCard.Application.Services
{
    public class HackerNewsApplicationService
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly IMapper _mapper;

        public HackerNewsApplicationService(IHackerNewsService hackerNewsService, IMapper mapper)
        {
            _hackerNewsService = hackerNewsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HackerNewsStoryDto>> GetTopStoriesAsync(int numberOfStories)
        {
            var stories = await _hackerNewsService.GetTopStoriesAsync(numberOfStories);
            return _mapper.Map<IEnumerable<HackerNewsStoryDto>>(stories);
        }
    }
}
