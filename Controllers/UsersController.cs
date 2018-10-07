using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MementoScraperApi.Models;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using MementoScraperApi.Exceptions;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MementoScraperApi.Credentials;
using Microsoft.AspNetCore.Cors;

namespace MementoScraperApi.Controllers {
    
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSecret _appSecret;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSecret> appSettings) {
            this._userService = userService;
            this._mapper = mapper;
            this._appSecret = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto) {
            var user = this._userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSecret.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto) {
            // map dto to entity
            var user = this._mapper.Map<User>(userDto);

            try {
                // save 
                this._userService.Create(user, userDto.Password);
                return Ok();
            } catch(AppException ex) {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll() {
            var users =  this._userService.GetAll();
            var userDtos = this._mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var user =  this._userService.GetById(id);
            var userDto = this._mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto) {
            // map dto to entity and set id
            var user = this._mapper.Map<User>(userDto);
            user.Id = id;

            try {
                // save 
                this._userService.Update(user, userDto.Password);
                return Ok();
            } catch(AppException ex) {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            this._userService.Delete(id);
            return Ok();
        }
    }
}