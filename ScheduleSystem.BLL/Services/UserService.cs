using AutoMapper;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<string> LoginAsync(string login, string password)
    {
        // TODO: find user, verify BCrypt hash, generate JWT token
        throw new NotImplementedException();
    }

    public Task<UserDto> GetByIdAsync(int id)
    {
        // TODO: implement — throw NotFoundException if not found
        throw new NotImplementedException();
    }

    public Task<UserDto> CreateAsync(UserDto dto, string password)
    {
        // TODO: check duplicate login, BCrypt.HashPassword, save
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        // TODO: implement — throw NotFoundException if not found
        throw new NotImplementedException();
    }
}
