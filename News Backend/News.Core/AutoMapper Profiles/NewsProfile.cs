using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using News.Core.Models.Dtos.News;

namespace News.Core.AutoMapper_Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<NewsCreateEditDto, Models.Domain.News>();
            CreateMap<Models.Domain.News, NewsReadDto>()
                .ForMember(dest => dest.Category, opt =>
                    opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Author, opt =>
                    opt.MapFrom(src => src.User.UserName));
        }
    }
}
