﻿using AutoMapper;
using Entities.DomainModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using System.Dynamic;

namespace Kwikker_Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source to Destination mappings
            CreateMap<UserForRegistrationDto, User>();


            

            CreateMap<User, GeneralUserDTO>();

            CreateMap<UserForUpdateDTO, User>()
      .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<TweetForCreationDTO, Tweet>();

            // Mapping from Tweet to TweetDTO
            CreateMap<Tweet, TweetDTO>()
                .ForCtorParam("profilePicture", opt => opt.MapFrom(src => src.User.ProfilePicture )) // Default to empty string if null
                .ForCtorParam("userName", opt => opt.MapFrom(src => src.User.UserName)); // Maps User's Username to TweetDTO's userName

            CreateMap<TweetForUpdateDTO, Tweet>();
            CreateMap<Trend, TrendDTO>();
            CreateMap<Notification, NotificationDTO>();
        
        }
    }
}
