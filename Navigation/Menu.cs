namespace Navigation;

public abstract class Menu
{
    protected Menu previousMenu;

    public Menu(Menu previousMenu)
    {
        this.previousMenu = previousMenu;
    }

    public abstract void Display();

    public Menu Navigation()
    {

        Console.WriteLine("--- Navigation ---\n" +
                          "Type 'back' to go back. Type 'exit' to exit.\n" +
                          "Select option :");
        string input = Console.ReadLine();
        if (input == "exit")
        {
            return null;
        }

        if (input == "back")
        {
            return previousMenu;
        }

        return SelectOption(input);
    }
    protected abstract Menu SelectOption(string input);

}