namespace Navigation;

public class About_Menu : Menu
{
    public About_Menu(Menu previousMenu):base(previousMenu)
    { }
    public override void Display()
    {
        Console.WriteLine("=== About Menu ===");
        Console.WriteLine("About ME"); 
    }
    protected override Menu SelectOption(string input)
    {
        Console.WriteLine("Invalid option!");
        return this;
    }
}