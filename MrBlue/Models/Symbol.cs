namespace MrBlue.Models;

public class Symbol
{
    public string Emoji { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int PayOut { get; set; }

    public Symbol()
    {
        Emoji = "🧑🏿‍🔬";
        ImageUrl = "";
        Description = "";
        PayOut = 0;
    }
}