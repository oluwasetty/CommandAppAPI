using System.Collections.Generic;
using ApprovalApp.Models;
using ApprovalApp.Dtos.UserDtos;
using ApprovalApp.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApprovalApp.Controllers
{
    // api/users
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var userItems = _repository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(int id)
        {
            var userItem = _repository.GetUserById(id);
            if (userItem != null)
                return Ok(_mapper.Map<UserReadDto>(userItem));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _repository.CreateUser(userModel, userCreateDto.Password);
            _repository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtRoute(nameof(GetUserById), new { Id = userReadDto.Id }, userReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var userItem = _repository.GetUserById(id);
            if (userItem == null)
                return NotFound();
            _mapper.Map(userUpdateDto, userItem);
            _repository.UpdateUser(userItem);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var userItem = _repository.GetUserById(id);
            if (userItem == null)
                return NotFound();
            _repository.DeleteUser(userItem);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}