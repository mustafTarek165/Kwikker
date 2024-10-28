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

            CreateMap<Tweet, TweetDTO>()
    .ForCtorParam("profilePicture", opt => opt.MapFrom(src => src.User.ProfilePicture)) // Maps the User's ProfilePicture to TweetDTO's profilePicture
    .ForCtorParam("userName", opt => opt.MapFrom(src => src.User.Username));             // Maps the User's Username to TweetDTO's userName

            CreateMap<TweetForUpdateDTO, Tweet>();  

            CreateMap<Follow, FollowDTO>();
            CreateMap<Bookmark, BookmarkDTO>();

             CreateMap<Trend,TrendDTO>();    

            CreateMap<Notification, NotificationDTO>();

            CreateMap<UserDTO, GeneralUserDTO>();
        }
    }
}
