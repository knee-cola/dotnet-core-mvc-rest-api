using System.Collections.Generic;
using dotnet_core_mvc_rest_api.Models;

namespace dotnet_core_mvc_rest_api.Data
{
    /// <summary>
    /// fake repo with hardkoded values
    /// </summary>
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command> {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle & Pan" },
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a knife", Platform = "Knife & chopping board" },
                new Command { Id = 2, HowTo = "Make a cup of tea", Line = "Place teabag in cup", Platform = "Kettle & cup" }
            };
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil water",
                Platform = "Kettle & Pan"
            };
        }
    }
}