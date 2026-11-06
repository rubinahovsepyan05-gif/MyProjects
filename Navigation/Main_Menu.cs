namespace Navigation;

enum MainMenuOptions
{
    Play = 1,
    Settings,
    About
}

public class Main_Menu : Menu
{
    public Main_Menu():base(null)
    { }
    public override void Display()
    {
        Console.WriteLine("=== Main Menu ===");
        Console.WriteLine("1 - Play\n2 - Settings\n3 - About"); 
    }

    protected override Menu SelectOption(string input)
    {
        if (Enum.TryParse(input, out MainMenuOptions option) &&
            Enum.IsDefined(typeof(MainMenuOptions), option))
        {
            switch (option)
            {
                case MainMenuOptions.Play:
                    return new Play_Menu(this);

                case MainMenuOptions.Settings:
                    return new Settings_Menu(this);

                case MainMenuOptions.About:
                    return new About_Menu(this);
            }
        }

        Console.WriteLine("Invalid option!");
        return this;
    }
}