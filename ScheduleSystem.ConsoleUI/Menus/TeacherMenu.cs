using System;
using System.Threading.Tasks;

namespace ScheduleSystem.ConsoleUI.Menus;

public class TeacherMenu
{
    private readonly ApiClient _client;

    public TeacherMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Меню викладача ===");
            Console.WriteLine("1. Мій розклад");
            Console.WriteLine("2. Розклад групи");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await ShowMyScheduleAsync(); break;
                case "2": await ShowGroupScheduleAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    private async Task ShowMyScheduleAsync()
    {
        Console.Clear();
        Console.WriteLine("=== МІЙ РОЗКЛАД ===");
        try
        {
            var result = await _client.GetAsync<object>("api/schedule/teacher/1");
            Console.WriteLine("\nВаш розклад занять:");
            Console.WriteLine(result?.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    private async Task ShowGroupScheduleAsync()
    {
        Console.Clear();
        Console.WriteLine("=== РОЗКЛАД ГРУПИ ===");
        Console.Write("Введіть ID групи: ");
        string? groupId = Console.ReadLine();
        try
        {
            var result = await _client.GetAsync<object>($"api/schedule/group/{groupId}");
            Console.WriteLine($"\nРозклад групи ID {groupId}:");
            Console.WriteLine(result?.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}
