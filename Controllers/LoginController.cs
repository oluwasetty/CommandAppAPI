using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApprovalApp.Models;
using ApprovalApp.Interfaces;
using AutoMapper;
using ApprovalApp.Dtos.UserDtos;

namespace ApprovalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public LoginController(IConfiguration config, IUserRepository repository, IMapper mapper)
        {
            _config = config;
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLoginDto login)
        {
            ActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                var userReadDto = _mapper.Map<UserReadDto>(user);
                response = Ok(new { token = tokenString, user = userReadDto });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(UserLoginDto login)
        {
            User user = _repository.AuthenticateUser(login.EmailAddress, login.Password);

            return user;
        }
    }
}