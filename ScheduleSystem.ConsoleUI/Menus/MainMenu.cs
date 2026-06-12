using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScheduleSystem.ConsoleUI.Menus;

public class MainMenu
{
    private readonly ApiClient _client;

    public MainMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Електронний розклад ===");
            Console.WriteLine("1. Увійти");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await LoginAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    private async Task LoginAsync()
    {
        Console.Clear();
        Console.WriteLine("=== АВТОРИЗАЦІЯ ===");
        Console.Write("Введіть логін: ");
        string? login = Console.ReadLine();
        Console.Write("Введіть пароль: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("\nЛогін або пароль не можуть бути порожніми.");
            Console.ReadKey();
            return;
        }

        try
        {
            var loginBody = new { Login = login, Password = password };
            var response = await _client.PostAsync<TokenResponse>("auth/login", loginBody);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                _client.SetToken(response.Token);

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(response.Token);
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

                Console.WriteLine($"\nВаша роль: {roleClaim}");
                Console.WriteLine("Натисніть будь-яку клавішу для переходу в меню...");
                Console.ReadKey();

                switch (roleClaim)
                {
                    case "Teacher":
                        var teacherMenu = new TeacherMenu(_client);
                        await teacherMenu.RunAsync();
                        break;
                    case "Student":
                        var studentMenu = new StudentMenu(_client);
                        await studentMenu.RunAsync();
                        break;
                    default:
                        Console.WriteLine("\nМеню для вашої ролі недоступне з консолі.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Помилка авторизації]: {ex.Message}");
            Console.ReadKey();
        }
    }
}

public class TokenResponse
{
    public string Token { get; set; } = null!;
}
