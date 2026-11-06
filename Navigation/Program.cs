namespace Navigation;

class Program
{
    static void Main()
    {
        Menu currentMenu = new Main_Menu();

        while (currentMenu != null)
        {
            currentMenu.Display();
            currentMenu = currentMenu.Navigation();
        }

        Console.WriteLine("Program exited.");
    }
}