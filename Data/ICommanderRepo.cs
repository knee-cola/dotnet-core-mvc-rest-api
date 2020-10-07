using System.Collections.Generic;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        
    }
    
}