using dotnet_core_mvc_rest_api.Dtos;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Profiles
{
    public class CommandProfile : AutoMapper.Profile
    {
        public CommandProfile()
        {
            // mapping for "read" operation
            CreateMap<Command, CommandReadDto>();
            // mapping for "write" operation
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            // mapping for PATCH request
            CreateMap<Command, CommandUpdateDto>();
        }
    }    
}