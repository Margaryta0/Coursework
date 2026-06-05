using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.WebAPI.Models;

namespace ScheduleSystem.WebAPI.Controllers;

[ApiController]
[Route("api/schedule")]
[Authorize]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterRequest filter)
    {
        // TODO: map FilterRequest → ScheduleFilterDto, call service
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpGet("group/{groupId:int}")]
    public async Task<IActionResult> GetByGroup(int groupId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpGet("teacher/{teacherId:int}")]
    public async Task<IActionResult> GetByTeacher(int teacherId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ScheduleEntryRequest request)
    {
        // TODO: map request → ScheduleEntryDto, call service, return 201
        throw new NotImplementedException();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ScheduleEntryRequest request)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
