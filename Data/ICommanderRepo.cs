using System.Collections.Generic;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int Id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
    }
    
}