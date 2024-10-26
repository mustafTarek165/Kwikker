using AutoMapper;
using Entities.DomainModels;
using Entities.Models;
using Shared.DTOs;

namespace Kwikker_Backend
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            //Src=>Dest
            CreateMap<User, UserDTO>();
            

            CreateMap<TweetForCreationDTO, Tweet>();
            CreateMap<Tweet, TweetDTO>();
            CreateMap<TweetForUpdateDTO, Tweet>();  

            CreateMap<Follow, FollowDTO>();
            CreateMap<Bookmark, BookmarkDTO>();

             CreateMap<Trend,TrendDTO>();    

            CreateMap<Notification, NotificationDTO>();

            CreateMap<UserDTO, GeneralUserDTO>();
        }
    }
}
