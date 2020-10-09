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

        public Command GetCommandById(int Id)
        {
            return context.Commands.FirstOrDefault(p => p.Id == Id);
        }

        void ICommanderRepo.CreateCommand(Command cmd)
        {
            if(cmd == null) {
                throw new System.ArgumentNullException(nameof(cmd));
            }

            context.Add<Command>(cmd);

        }

        bool ICommanderRepo.SaveChanges()
        {
            // submit all changes made to model to the database
            return(context.SaveChanges() >= 0 );
        }

        public void UpdateCommand(Command cmd)
        {
            // no implementation needed
        }
    }

}