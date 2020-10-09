
using System.Collections.Generic;
using AutoMapper;
using dotnet_core_mvc_rest_api.Data;
using dotnet_core_mvc_rest_api.Dtos;
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
        private readonly IMapper mapper;

        public CommandsController(ICommanderRepo commanderRepo, IMapper mapper)
        {
            this.commanderRepo = commanderRepo;
            this.mapper = mapper;
        }

        // route = GET /api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands() {
            var commandItems = commanderRepo.GetAllCommands();

            // `Ok` will return HTTP response 200
            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // route = GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id) {
            var commandItem = commanderRepo.GetCommandById(id);
            
            if(commandItem != null)
                // mapping from `Command` to `CommandReadDto`
                return Ok(mapper.Map<CommandReadDto>(commandItem));
            
            return NotFound();
        }
    }
}