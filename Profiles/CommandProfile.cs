using dotnet_core_mvc_rest_api.Dtos;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Profiles
{
    public class CommandProfile : AutoMapper.Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
        }
    }    
}