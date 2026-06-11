using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models; 
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.WebAPI.Models;

namespace ScheduleSystem.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {

            var token = await _userService.LoginAsync(request.Login, request.Password);
            

            return Ok(new { Token = token });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {

            var dto = new UserDto
            {
                Login = request.Login,
                Role = request.Role,
                GroupId = request.GroupId,
                TeacherId = request.TeacherId
            };

            var createdUser = await _userService.CreateAsync(dto, request.Password);
            
            return Ok(createdUser);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
