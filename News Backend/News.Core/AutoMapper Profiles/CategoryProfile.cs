using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using News.Core.Models.Domain;
using News.Core.Models.Dtos.Category;

namespace News.Core.AutoMapper_Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateEditDto, Category>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Category, CategoryReadAllNewsDto>();
        }
    }
}
