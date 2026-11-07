namespace Navigation;

enum PlayMenuOptions
{
    SayHello = 1
}

public class PlayMenu : Menu
{
    public PlayMenu(Menu previousMenu):base(previousMenu)
    { }
    public override void Display()
    {
        Console.WriteLine("=== Play Menu ===");
        Console.WriteLine("1 - Say Hello"); 
    }
    protected override Menu SelectOption(string input)
    {
        if (Enum.TryParse(input, out PlayMenuOptions option) &&
            Enum.IsDefined(typeof(PlayMenuOptions), option))
        {
            switch (option)
            {
                case PlayMenuOptions.SayHello:
                    Console.WriteLine("Hello World!");
                    Console.ReadKey();
                    return this;
            }
        }

        Console.WriteLine("Invalid option!");
        return this;
    }
}