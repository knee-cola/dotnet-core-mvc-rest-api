using System.Collections.Generic;
using System.Linq;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext context;

        public SqlCommanderRepo(CommanderContext context)
        {
            this.context = context;
        }
 
        public IEnumerable<Command> GetAllCommands()
        {
            return context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(p => p.Id == id);
        }
    }

}