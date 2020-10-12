
using System.Collections.Generic;
using AutoMapper;
using dotnet_core_mvc_rest_api.Data;
using dotnet_core_mvc_rest_api.Dtos;
using dotnet_core_mvc_rest_api.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id) {
            var commandItem = commanderRepo.GetCommandById(id);
            
            if(commandItem != null)
                // mapping from `Command` to `CommandReadDto`
                return Ok(mapper.Map<CommandReadDto>(commandItem));
            
            return NotFound();
        }

        // route = POST api/commands/
        [HttpPost()]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmdDto) {

            // converting DTO to model
            var cmd = mapper.Map<Command>(cmdDto);

            // creating new entry in-memory
            commanderRepo.CreateCommand(cmd);

            // save changes to database
            commanderRepo.SaveChanges();

            var cmdReadDto = mapper.Map<CommandReadDto>(cmd);

            // this will return 201=Created HTTP response with
            // a HTTP header `Location` containing URL from which the crated record can be retrived
            return CreatedAtRoute(nameof(GetCommandById), new { Id = cmdReadDto.Id }, cmdReadDto);
        }

        // route = PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult PutCommand(int id, CommandUpdateDto cmdDto) {

            var cmdFromRepo = commanderRepo.GetCommandById(id);

            if(cmdFromRepo == null) {
                return NotFound(); // 404 Not Found
            }

            // mapping DTO directly onto `command` fetched from the database
            // > this will update all the properties with values coming from client
            mapper.Map(cmdDto, cmdFromRepo);

            // the following method will not do anything since it's empty - it does nothing
            // -> that is OK since the previous call to (Map) method has already updated
            //    the EF data. The `cmdFromRepo` is just a reference to an object stored by EF,
            //    meaning that changing it's properties results in EF to be also updated
            commanderRepo.UpdateCommand(cmdFromRepo);

            // persist changes to database
            commanderRepo.SaveChanges();

            return NoContent(); // 204 HTTP code
        }

        // route = PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartionalCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc) {

            // reading the Command which needs to be updated from the DB
            var cmdFromRepo = commanderRepo.GetCommandById(id);

            // checking if DB contains the Command we want tu update
            if(cmdFromRepo == null) {
                return NotFound(); // 404 Not Found
            }

            // converting the current DB Command to DTO
            var commandToPatch = mapper.Map<CommandUpdateDto>(cmdFromRepo);

            // patching the current DTO with information from the request
            patchDoc.ApplyTo(commandToPatch, ModelState);

            // checking if the updated command is valid
            if(!TryValidateModel(commandToPatch)) {
                return ValidationProblem(ModelState);
            }

            // copying data from pached DTO Command to DB Command
            mapper.Map(commandToPatch, cmdFromRepo);

            // the following method will not do anything since it's empty - it does nothing
            // -> that is OK since the previous call to (Map) method has already updated
            //    the EF data. The `cmdFromRepo` is just a reference to an object stored by EF,
            //    meaning that changing it's properties results in EF to be also updated
            commanderRepo.UpdateCommand(cmdFromRepo);

            // persist changes to database
            commanderRepo.SaveChanges();

            return NoContent(); // 204 HTTP code
        }
    }
}