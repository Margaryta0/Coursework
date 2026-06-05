using AutoMapper;
using Moq;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Mapping;
using ScheduleSystem.BLL.Services;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.Tests;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<IRepository<User>> _mockUserRepo;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        _mockUserRepo = new Mock<IRepository<User>>();
        _mockUow.Setup(u => u.Users).Returns(_mockUserRepo.Object);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();

        _userService = new UserService(_mockUow.Object, _mapper);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsJwtToken()
    {
        // Arrange
        // TODO: setup mock to return user with valid BCrypt hash

        // Act
        // TODO: var token = await _userService.LoginAsync(login, password)

        // Assert
        // TODO: Assert.NotEmpty(token)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task LoginAsync_WrongPassword_ThrowsValidationException()
    {
        // Arrange
        // TODO: setup mock to return user with mismatching hash

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<ValidationException>(...)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task LoginAsync_NonExistentUser_ThrowsNotFoundException()
    {
        // Arrange
        // TODO: setup mock to return null for user lookup

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<NotFoundException>(...)
        throw new NotImplementedException();
    }

    [Fact]
    public async Task CreateAsync_DuplicateLogin_ThrowsValidationException()
    {
        // Arrange
        // TODO: setup mock to return existing user with same login

        // Act & Assert
        // TODO: await Assert.ThrowsAsync<ValidationException>(...)
        throw new NotImplementedException();
    }
}
