using Microsoft.AspNetCore.Components;

namespace MrBlue.Components.Pages.Prototype;

public partial class Bingo : ComponentBase
{

}

public class BingoPlayer
{
    public string Name { get; set; }
    public List<int> CardNumbers { get; set; } = new();
    public decimal Bet { get; set; }
    public bool IsWinner { get; set; } = false;
}

public class BingoGame
{
    public BingoPlayer Player { get; set; } = new();
    public BingoPlayer Bot { get; set; } = new();
    public HashSet<int> DrawnNumbers { get; set; } = new();
    public decimal PrizePool => Player.Bet + Bot.Bet;

    public void DrawNumber()
    {
        var rnd = new Random();
        int num;
        do
        {
            num = rnd.Next(1, 76); // 1-75
        } while (DrawnNumbers.Contains(num));
        DrawnNumbers.Add(num);
    }

    public bool CheckForWinner(BingoPlayer player)
    {
        return player.CardNumbers.All(n => DrawnNumbers.Contains(n));
    }
}
