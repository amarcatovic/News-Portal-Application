using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using News.Core.Models.Domain;
using News.Core.Models.Dtos.User;

namespace News.Core.AutoMapper_Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
