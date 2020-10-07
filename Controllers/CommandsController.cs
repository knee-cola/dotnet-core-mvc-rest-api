
using System.Collections.Generic;
using dotnet_core_mvc_rest_api.Data;
using dotnet_core_mvc_rest_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_mvc_rest_api.Controllers {

    // Bazna klasa: ovdje NE koristimo `Controller` zato jer ona donosi dodatne
    // mogućnosti koje nam u ovom primjeru ne trebaju - dovoljna nam je `ControllerBase` klasa
    [Route("api/commands")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly ICommanderRepo commanderRepo;

        public CommandsController(ICommanderRepo commanderRepo)
        {
            this.commanderRepo = commanderRepo;
        }

        // route = GET /api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands() {
            var commandItems = commanderRepo.GetAllCommands();

            // `Ok` will return HTTP response 200
            return Ok(commandItems);
        }

        // route = GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id) {
            var commandItem = commanderRepo.GetCommandById(id);

            return Ok(commandItem);
        }

    }
}