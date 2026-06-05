using ScheduleSystem.ConsoleUI;
using ScheduleSystem.ConsoleUI.Menus;

const string ApiBaseUrl = "https://localhost:5001/";

var client = new ApiClient(ApiBaseUrl);
var menu = new MainMenu(client);

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
await menu.RunAsync();
