using AutoMapper;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.BLL.Services;

public class ScheduleService : IScheduleService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ScheduleService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ScheduleEntryDto> GetByIdAsync(int id)
    {
        var entity = await _uow.ScheduleEntries.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException($"Schedule entry with id {id} not found");
        return _mapper.Map<ScheduleEntryDto>(entity);
    }

    public async Task<IEnumerable<ScheduleEntryDto>> GetByGroupAsync(int groupId)
    {
        var entries = await _uow.ScheduleEntries.GetByGroupAsync(groupId);
        return _mapper.Map<IEnumerable<ScheduleEntryDto>>(entries);
    }

    public async Task<IEnumerable<ScheduleEntryDto>> GetByTeacherAsync(int teacherId)
    {
        var entries = await _uow.ScheduleEntries.GetByTeacherAsync(teacherId);
        return _mapper.Map<IEnumerable<ScheduleEntryDto>>(entries);
    }

    public async Task<IEnumerable<ScheduleEntryDto>> GetByClassroomAsync(int classroomId)
    {
        var entries = await _uow.ScheduleEntries.GetByClassroomAsync(classroomId);
        return _mapper.Map<IEnumerable<ScheduleEntryDto>>(entries);
    }

    public async Task<IEnumerable<ScheduleEntryDto>> GetBySubjectAsync(int subjectId)
    {
        var entries = await _uow.ScheduleEntries.GetBySubjectAsync(subjectId);
        return _mapper.Map<IEnumerable<ScheduleEntryDto>>(entries);
    }

    public async Task<IEnumerable<ScheduleEntryDto>> GetFilteredAsync(ScheduleFilterDto filter)
    {
        var entries = await _uow.ScheduleEntries.GetFilteredAsync(
            filter.GroupId, filter.TeacherId, filter.ClassroomId,
            filter.SubjectId, filter.DayOfWeek, filter.WeekType);
        return _mapper.Map<IEnumerable<ScheduleEntryDto>>(entries);
    }

    public async Task<ScheduleEntryDto> CreateAsync(ScheduleEntryDto dto)
    {
        var subject = await _uow.Subjects.GetByIdAsync(dto.SubjectId);
        if (subject is null)
            throw new NotFoundException($"Subject with id {dto.SubjectId} not found");

        var teacher = await _uow.Teachers.GetByIdAsync(dto.TeacherId);
        if (teacher is null)
            throw new NotFoundException($"Teacher with id {dto.TeacherId} not found");

        var group = await _uow.Groups.GetByIdAsync(dto.GroupId);
        if (group is null)
            throw new NotFoundException($"Group with id {dto.GroupId} not found");

        var classroom = await _uow.Classrooms.GetByIdAsync(dto.ClassroomId);
        if (classroom is null)
            throw new NotFoundException($"Classroom with id {dto.ClassroomId} not found");

        var entity = _mapper.Map<ScheduleEntry>(dto);

        var hasConflict = await _uow.ScheduleEntries.HasConflictAsync(entity);
        if (hasConflict)
            throw new ScheduleConflictException("Schedule conflict detected");

        await _uow.ScheduleEntries.AddAsync(entity);
        await _uow.SaveAsync();

        return _mapper.Map<ScheduleEntryDto>(entity);
    }

    public async Task UpdateAsync(ScheduleEntryDto dto)
    {
        var entity = await _uow.ScheduleEntries.GetByIdAsync(dto.Id);
        if (entity is null)
            throw new NotFoundException($"Schedule entry with id {dto.Id} not found");

        _mapper.Map(dto, entity);

        _uow.ScheduleEntries.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.ScheduleEntries.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException($"Schedule entry with id {id} not found");

        _uow.ScheduleEntries.Delete(entity);
        await _uow.SaveAsync();
    }
}
