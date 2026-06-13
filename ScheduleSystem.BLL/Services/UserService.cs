using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _configuration;

    public UserService(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
    {
        _uow = uow;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<string> LoginAsync(string login, string password)
    {
        var users = await _uow.Users.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            throw new NotFoundException($"Користувача з логіном '{login}' не знайдено.");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new ValidationException("Неправильний пароль.");
        }

        var jwtKey = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key is not configured.");
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        var expiresInMinutes = int.TryParse(_configuration["Jwt:ExpiresInMinutes"], out var minutes)
            ? minutes
            : 60;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
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
