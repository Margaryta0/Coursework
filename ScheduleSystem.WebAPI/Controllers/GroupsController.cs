using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.BLL.Exceptions;

namespace ScheduleSystem.WebAPI.Controllers;

[ApiController]
[Route("api/groups")]
[Authorize]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await _groupService.GetAllAsync();
        return Ok(groups);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var group = await _groupService.GetByIdAsync(id);
            return Ok(group);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] GroupDto dto)
    {
        try
        {
            var createdGroup = await _groupService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdGroup.Id }, createdGroup);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] GroupDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { Message = "ID у маршруті та в тілі запиту не збігаються." });
        }

        try
        {
            await _groupService.UpdateAsync(dto);
            return NoContent(); 
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
            await _groupService.DeleteAsync(id);
            return NoContent(); 
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}
