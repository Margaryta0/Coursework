namespace ScheduleSystem.ConsoleUI.Menus;

public class StudentMenu
{
    private readonly ApiClient _client;

    public StudentMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Меню студента ===");
            Console.WriteLine("1. Мій розклад");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await ShowMyScheduleAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    private Task ShowMyScheduleAsync() => throw new NotImplementedException();
}
