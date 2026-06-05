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

    private Task ShowMyScheduleAsync() => throw new NotImplementedException();
    private Task ShowGroupScheduleAsync() => throw new NotImplementedException();
}
