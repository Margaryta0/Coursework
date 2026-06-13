using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
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
        var filterDto = new ScheduleFilterDto
        {
            GroupId = filter.GroupId,
            TeacherId = filter.TeacherId,
            ClassroomId = filter.ClassroomId,
            SubjectId = filter.SubjectId,
            DayOfWeek = filter.DayOfWeek,
            WeekType = filter.WeekType
        };

        var entries = await _scheduleService.GetFilteredAsync(filterDto);
        return Ok(entries.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entry = await _scheduleService.GetByIdAsync(id);
        return Ok(ToResponse(entry));
    }

    [HttpGet("group/{groupId:int}")]
    public async Task<IActionResult> GetByGroup(int groupId)
    {
        var entries = await _scheduleService.GetByGroupAsync(groupId);
        return Ok(entries.Select(ToResponse));
    }

    [HttpGet("teacher/{teacherId:int}")]
    public async Task<IActionResult> GetByTeacher(int teacherId)
    {
        var entries = await _scheduleService.GetByTeacherAsync(teacherId);
        return Ok(entries.Select(ToResponse));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ScheduleEntryRequest request)
    {
        var dto = ToDto(request);
        var created = await _scheduleService.CreateAsync(dto);
        var response = ToResponse(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ScheduleEntryRequest request)
    {
        var dto = ToDto(request);
        dto.Id = id;
        await _scheduleService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _scheduleService.DeleteAsync(id);
        return NoContent();
    }

    private static ScheduleEntryDto ToDto(ScheduleEntryRequest request) => new()
    {
        SubjectId = request.SubjectId,
        TeacherId = request.TeacherId,
        GroupId = request.GroupId,
        ClassroomId = request.ClassroomId,
        DayOfWeek = request.DayOfWeek,
        LessonNumber = request.LessonNumber,
        WeekType = request.WeekType,
        Semester = request.Semester,
        Year = request.Year
    };

    private static ScheduleEntryResponse ToResponse(ScheduleEntryDto dto) => new()
    {
        Id = dto.Id,
        SubjectName = dto.SubjectName,
        TeacherFullName = dto.TeacherFullName,
        GroupName = dto.GroupName,
        ClassroomName = dto.ClassroomName,
        DayOfWeek = dto.DayOfWeek.ToString(),
        LessonNumber = (int)dto.LessonNumber,
        WeekType = dto.WeekType.ToString(),
        Semester = dto.Semester,
        Year = dto.Year
    };
}

