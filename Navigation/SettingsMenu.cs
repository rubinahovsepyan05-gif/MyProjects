namespace Navigation;

enum SettingsMenuOptions
{
    AudioSettings = 1,
    VideoSettings
}

public class SettingsMenu : Menu
{
    public SettingsMenu(Menu previousMenu):base(previousMenu)
    { }
    public override void Display()
    {
        Console.WriteLine("=== Settings Menu ===");
        Console.WriteLine("1 - Audio Settings\n2 - Video Settings"); 
    }
    protected override Menu SelectOption(string input)
    {
        if (Enum.TryParse(input, out SettingsMenuOptions option) &&
            Enum.IsDefined(typeof(SettingsMenuOptions), option))
        {
            switch (option)
            {
                case SettingsMenuOptions.AudioSettings:
                    Console.WriteLine("Audio Settings");
                    Console.ReadKey();
                    return this;

                case SettingsMenuOptions.VideoSettings:
                    Console.WriteLine("Video Settings");
                    Console.ReadKey();
                    return this;
            }
        }

        Console.WriteLine("Invalid option!");
        return this;
    }
}