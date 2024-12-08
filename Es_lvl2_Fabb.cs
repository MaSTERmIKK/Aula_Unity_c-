using System;
using System.Collections.Generic;

public class Giocattolo
{
    public string Nome { get; private set; }
    public string Materiale { get; private set; }
    public decimal PrezzoProduzione { get; private set; }
    public decimal PrezzoVendita { get; private set; }

    public Giocattolo(string nome, string materiale, decimal prezzoProduzione, decimal prezzoVendita)
    {
        Nome = nome;
        Materiale = materiale;
        PrezzoProduzione = prezzoProduzione;
        PrezzoVendita = prezzoVendita;
    }

    public decimal Margine()
    {
        return PrezzoVendita - PrezzoProduzione;
    }
}

public class FabbricaGiocattoli
{
    private List<Giocattolo> giocattoli = new List<Giocattolo>();

    public void AggiungiGiocattolo(Giocattolo g)
    {
        giocattoli.Add(g);
    }

    public void StampaElencoGiocattoli()
    {
        foreach (var g in giocattoli)
        {
            Console.WriteLine($"Nome: {g.Nome}, Materiale: {g.Materiale}, Prezzo Vendita: {g.PrezzoVendita:C}");
        }
    }

    public decimal CalcolaGuadagnoTotale()
    {
        decimal guadagno = 0m;
        foreach (var g in giocattoli)
        {
            guadagno += g.Margine();
        }
        return guadagno;
    }
}

public class Program
{
    public static void Main()
    {
        FabbricaGiocattoli fabbrica = new FabbricaGiocattoli();
        fabbrica.AggiungiGiocattolo(new Giocattolo("Macchinina", "Plastica", 2.50m, 5.00m));
        fabbrica.AggiungiGiocattolo(new Giocattolo("Teddy Bear", "Peluche", 3.00m, 7.50m));
        fabbrica.AggiungiGiocattolo(new Giocattolo("Puzzle", "Cartone", 1.50m, 4.00m));

        Console.WriteLine("Elenco Giocattoli:");
        fabbrica.StampaElencoGiocattoli();

        Console.WriteLine();
        Console.WriteLine($"Guadagno Totale: {fabbrica.CalcolaGuadagnoTotale():C}");
    }
}
