namespace Navigation;

enum MainMenuOptions
{
    Play = 1,
    Settings,
    About
}

public class MainMenu : Menu
{
    public MainMenu():base(null)
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
                    return new PlayMenu(this);

                case MainMenuOptions.Settings:
                    return new SettingsMenu(this);

                case MainMenuOptions.About:
                    return new AboutMenu(this);
            }
        }

        Console.WriteLine("Invalid option!");
        return this;
    }
}