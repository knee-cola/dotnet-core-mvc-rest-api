
using dotnet_core_mvc_rest_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_mvc_rest_api.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        // representation of command model in our database -> we use DbSet
        // > we want to represent our Command object to our database as a DbSet and it is going to be called "Commands"
        public DbSet<Command> Commands { get; set; }
    }
}