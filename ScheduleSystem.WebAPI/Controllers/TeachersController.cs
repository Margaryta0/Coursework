using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.BLL.Exceptions;

namespace ScheduleSystem.WebAPI.Controllers;

[ApiController]
[Route("api/teachers")]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _teacherService.GetAllAsync();
        return Ok(teachers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            return Ok(teacher);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] TeacherDto dto)
    {
        try
        {
            var createdTeacher = await _teacherService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Id }, createdTeacher);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] TeacherDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { Message = "ID у маршруті та в тілі запиту не збігаються." });
        }

        try
        {
            await _teacherService.UpdateAsync(dto);
            return NoContent(); // 204 No Content для успішного Put
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

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _teacherService.DeleteAsync(id);
            return NoContent(); // 204 No Content для успішного Delete
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}
