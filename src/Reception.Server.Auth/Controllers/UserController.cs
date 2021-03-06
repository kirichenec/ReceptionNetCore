﻿using Microsoft.AspNetCore.Mvc;
using Reception.Extension;
using Reception.Model.Network;
using Reception.Server.Auth.Helpers;
using Reception.Server.Auth.Logic;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userService)
        {
            _userLogic = userService;
        }

        // POST User/Authenticate
        [HttpPost("Authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await _userLogic.Authenticate(model);

            if (response.HasNoValue())
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        // GET User/IsAuthValid
        [InternalServerAuthorize]
        [HttpGet("IsAuthValid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult IsAuthValid()
        {
            return Ok();
        }

        // PUT User
        [InternalServerAuthorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticateRequest model)
        {
            var savedUser = await _userLogic.CreateUserAsync(model.Login, model.Password);
            return Ok(savedUser.ToQueryResult());
        }

        // GET User?searchText=5
        [InternalServerAuthorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var searchResult = await _userLogic.SearchAsync(searchText);
            return Ok(searchResult.ToQueryResult());
        }
    }
}