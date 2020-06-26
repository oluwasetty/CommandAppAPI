using AutoMapper;
using ApprovalApp.Dtos.UserDtos;
using ApprovalApp.Models;

namespace ApprovalApp.profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // source -> target
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}