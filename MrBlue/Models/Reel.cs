using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrBlue.Models;

public class Reel
{
    private readonly ICollection<Symbol> _symbols;
    public Symbol? CurrentSymbol { get; private set; }

    public Reel(ICollection<Symbol> symbols)
    {
        if (symbols is null || !symbols.Any())
        {
            this._symbols = new HashSet<Symbol>();
            CurrentSymbol = new();
            return;
        }

        this._symbols = symbols;
        this.CurrentSymbol = symbols.OrderByDescending(s => s.PayOut).FirstOrDefault();
    }

    public async Task Spin(Func<Task> updateUi)
    {
        var random = new Random();
        var spins = random.Next(10, 20);

        for (var i = 0; i < spins; i++)
        {
            CurrentSymbol = _symbols.ToList()[random.Next(_symbols.Count)];
            await updateUi();
            await Task.Delay(100 + (i * 10));
        }
    }
}