using AutoMapper;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
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

    public Task<IEnumerable<ScheduleEntryDto>> GetByGroupAsync(int groupId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntryDto>> GetByTeacherAsync(int teacherId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntryDto>> GetByClassroomAsync(int classroomId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntryDto>> GetBySubjectAsync(int subjectId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntryDto>> GetFilteredAsync(ScheduleFilterDto filter)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<ScheduleEntryDto> GetByIdAsync(int id)
    {
        // TODO: implement — throw NotFoundException if not found
        throw new NotImplementedException();
    }

    public Task<ScheduleEntryDto> CreateAsync(ScheduleEntryDto dto)
    {
        // TODO: validate, check conflicts (HasConflictAsync), save
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ScheduleEntryDto dto)
    {
        // TODO: implement — throw NotFoundException if not found
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        // TODO: implement — throw NotFoundException if not found
        throw new NotImplementedException();
    }
}
