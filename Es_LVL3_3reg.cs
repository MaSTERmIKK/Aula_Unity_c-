using System;
using System.Collections.Generic;

public abstract class ProdottoSoftware
{
    public string Nome { get; protected set; }
    public decimal PrezzoVendita { get; protected set; }

    public ProdottoSoftware(string nome, decimal prezzoVendita)
    {
        Nome = nome;
        PrezzoVendita = prezzoVendita;
    }

    public abstract decimal CalcolaGuadagno();
}

public class WebApp : ProdottoSoftware
{
    private int utentiMensili;
    private decimal costoServerMensile;

    public WebApp(string nome, decimal prezzoVendita, int utentiMensili, decimal costoServerMensile)
        : base(nome, prezzoVendita)
    {
        this.utentiMensili = utentiMensili;
        this.costoServerMensile = costoServerMensile;
    }

    public override decimal CalcolaGuadagno()
    {
        // Esempio: Guadagno = PrezzoVendita * utentiMensili - costoServerMensile
        return (PrezzoVendita * utentiMensili) - costoServerMensile;
    }
}

public class MobileApp : ProdottoSoftware
{
    private int downloadMensili;
    private decimal commissioneStore; // Commissione pagata allo store

    public MobileApp(string nome, decimal prezzoVendita, int downloadMensili, decimal commissioneStore)
        : base(nome, prezzoVendita)
    {
        this.downloadMensili = downloadMensili;
        this.commissioneStore = commissioneStore;
    }

    public override decimal CalcolaGuadagno()
    {
        // Esempio: Guadagno = (PrezzoVendita * downloadMensili) - commissioneStore
        return (PrezzoVendita * downloadMensili) - commissioneStore;
    }
}

public class ProgramOOP
{
    public static void Main()
    {
        List<ProdottoSoftware> prodotti = new List<ProdottoSoftware>();

        prodotti.Add(new WebApp("SuperSite", 10.00m, 200, 300m));
        prodotti.Add(new MobileApp("AppCool", 2.00m, 1000, 100m));

        decimal guadagnoTotale = 0m;
        foreach (var p in prodotti)
        {
            guadagnoTotale += p.CalcolaGuadagno();
        }

        Console.WriteLine($"Guadagno Totale Software House: {guadagnoTotale:C}");
    }
}
