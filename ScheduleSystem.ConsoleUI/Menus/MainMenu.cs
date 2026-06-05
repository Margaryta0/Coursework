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
        // TODO: read login/password, call POST /api/auth/login
        // save token via _client.SetToken(token)
        // redirect to role-specific menu
        throw new NotImplementedException();
    }
}
