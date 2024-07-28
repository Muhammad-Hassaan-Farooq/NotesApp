using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.DTO.Auth;
using NotesApp.Interfaces.Auth;
using NotesApp.Mappers.Auth;

namespace NotesApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _authRepository.GetUsers();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (await _authRepository.UserExists(registerDTO.Email))
                {
                    return BadRequest("User already exists");
                }
                var user = await _authRepository.CreateUser(registerDTO);

                return Ok("User created successfully");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _authRepository.GetUserByEmail(loginDTO.Email);
                if (user == null)
                {
                    return BadRequest("Invalid email or password");
                }
                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                {
                    return BadRequest("Invalid email or password");
                }
                var token = _tokenService.GenerateToken(user);
                var userDTO = AuthMappers.MapUserToUserDTO(user, token);
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
    }
}
