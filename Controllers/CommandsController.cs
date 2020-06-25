using System.Collections.Generic;
using ApprovalApp.Models;
using ApprovalApp.Data;
using ApprovalApp.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApprovalApp.Controllers
{
    // api/commands
    [Route("api/commands")]
    [ApiController]
    [Authorize]
    public class CommandsController : ControllerBase
    {
        private readonly IApprovalAppRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(IApprovalAppRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
                return NotFound();
            _mapper.Map(commandUpdateDto, commandItem);
            _repository.UpdateCommand(commandItem);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
                return NotFound();
            _repository.DeleteCommand(commandItem);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}