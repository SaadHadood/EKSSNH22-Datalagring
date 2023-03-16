using _01_EFC.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
    await menu.MainMenu();
}