using AutoMapper;
using ApprovalApp.Dtos.CommandDtos;
using ApprovalApp.Models;

namespace ApprovalApp.profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // source -> target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
        }
    }
}