using AutoMapper;
using Moq;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Mapping;
using ScheduleSystem.BLL.Services;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;
using Xunit;

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

        var login = "maxim_admin";
        var password = "correct_password123";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password); 

        var userList = new List<User>
        {
            new User { Id = 1, Login = login, PasswordHash = passwordHash, Role = ScheduleSystem.DAL.Enums.UserRole.Admin }
        };


        _mockUserRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(userList);

   
        var token = await _userService.LoginAsync(login, password);


        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public async Task LoginAsync_WrongPassword_ThrowsValidationException()
    {

        var login = "maxim_admin";
        var wrongPassword = "wrong_password";
        var correctPasswordHash = BCrypt.Net.BCrypt.HashPassword("correct_password123");

        var userList = new List<User>
        {
            new User { Id = 1, Login = login, PasswordHash = correctPasswordHash, Role = ScheduleSystem.DAL.Enums.UserRole.Student }
        };

        _mockUserRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(userList);


        await Assert.ThrowsAsync<ValidationException>(() => 
            _userService.LoginAsync(login, wrongPassword));
    }

    [Fact]
    public async Task LoginAsync_NonExistentUser_ThrowsNotFoundException()
    {

        var login = "non_existent_user";

        _mockUserRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());


        await Assert.ThrowsAsync<NotFoundException>(() => 
            _userService.LoginAsync(login, "anyPassword123"));
    }

    [Fact]
    public async Task CreateAsync_DuplicateLogin_ThrowsValidationException()
    {

        var duplicateLogin = "existing_user";
        var existingUser = new User { Id = 5, Login = duplicateLogin };
        
        _mockUserRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User> { existingUser });


        var dto = new ScheduleSystem.BLL.Models.UserDto { Login = duplicateLogin, Role = ScheduleSystem.DAL.Enums.UserRole.Teacher };


        await Assert.ThrowsAsync<ValidationException>(() => 
            _userService.CreateAsync(dto, "password123"));
    }
}
