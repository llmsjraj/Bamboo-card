using AutoMapper;
using BambooCard.Application.DTOs;
using BambooCard.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooCard.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HackerNewsStory, HackerNewsStoryDto>();
            // Add other mappings here
        }
    }
}
