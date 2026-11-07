namespace Navigation;

class Program
{
    static void Main()
    {
        Menu currentMenu = new MainMenu();

        while (currentMenu != null)
        {
            currentMenu.Display();
            currentMenu = currentMenu.Navigation();
        }

        Console.WriteLine("Program exited.");
    }
}