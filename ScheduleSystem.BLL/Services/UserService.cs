using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string _jwtSecret = "SuperSecretKeyForScheduleSystem2026!!!"; // Ключ для підпису JWT

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<string> LoginAsync(string login, string password)
    {
        var users = await _uow.Users.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            throw new NotFoundException($"Користувача з логіном '{login}' не знайдено.");
        }

        bool isPasswordValid = user.PasswordHash == password || BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new ValidationException("Неправильний пароль.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Токен діє тиждень
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException($"Користувача з ID {id} не знайдено.");
        }

        // Мапимо Entity в DTO за допомогою AutoMapper
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserDto dto, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ValidationException("Пароль не може бути порожнім.");
        }

        var users = await _uow.Users.GetAllAsync();
        if (users.Any(u => u.Login.Equals(dto.Login, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ValidationException($"Логін '{dto.Login}' вже зайнятий.");
        }

        var userEntity = _mapper.Map<User>(dto);
        
        userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        await _uow.Users.AddAsync(userEntity);
        await _uow.SaveAsync();

        dto.Id = userEntity.Id;
        return dto;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException($"Користувача з ID {id} не знайдено.");
        }

        _uow.Users.Delete(user);
        await _uow.SaveAsync();
    }
}
