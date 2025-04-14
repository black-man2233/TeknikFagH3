using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using MrBlue.Models;

namespace MrBlue.Components.Pages.Prototype;

public partial class DiamondRizzing : ComponentBase
{
    private ObservableCollection<Symbol> Symbols = new();
    private List<Reel> reels = new();
    private bool IsSpinning;
    private int Credits = 100;
    private string ResultMessage = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();
        InitializeReels();
    }

    private void InitializeReels()
    {
        this.Symbols = new ObservableCollection<Symbol>()
        {
            new Symbol()
            {
                Emoji = "🍒",
                PayOut = 10
            },
            new Symbol()
            {
                Emoji = "🔔",
                PayOut = 20
            },
            new Symbol()
            {
                Emoji = "🍊",
                PayOut = 30
            },
            new Symbol()
            {
                Emoji = "7️⃣",
                PayOut = 40
            },
            new Symbol()
            {
                Emoji = "💎",
                PayOut = 50
            },
            new Symbol()
            {
                Emoji = "💰",
                PayOut = 100
            }
        };
        reels = new List<Reel>
        {
            new Reel(Symbols),
            new Reel(Symbols),
            new Reel(Symbols)
        };
    }

    private async Task Spin()
    {
        if (Credits < 1 || IsSpinning) return;

        Credits--;
        IsSpinning = true;
        ResultMessage = "";

        var tasks = new List<Task>();
        foreach (var reel in reels)
        {
            tasks.Add(reel.Spin(UpdateUI));
        }

        await Task.WhenAll(tasks);

        CheckWin();
        IsSpinning = false;
    }

    private void CheckWin()
    {
        var uniqueSymbols = reels.Select(r => r.CurrentSymbol).Distinct().Count();
        if (uniqueSymbols == 1)
        {
            var payout = reels.FirstOrDefault().CurrentSymbol.PayOut;
            Credits += payout;
            ResultMessage = $"JACKPOT! WON {payout} CREDITS!";
        }
        else
        {
            ResultMessage = "TRY AGAIN!";
        }
    }

    private async Task UpdateUI()
    {
        await InvokeAsync(StateHasChanged);
        await Task.Delay(50);
    }
}