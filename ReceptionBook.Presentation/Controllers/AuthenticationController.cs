using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionBook.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service; 
        
        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration) 
        { 
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration); 

            if (!result.Succeeded) 
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            } 
            return StatusCode(201); 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user) 
        { 
            if (!await _service.AuthenticationService.ValidateUser(user)) 
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" }); 

            var token = await _service.AuthenticationService.CreateToken();

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token }); 
        }
    }
}
