using System;

class Program
{
    static void Main(string[] args)
    {
        // Richiesta del nome all'utente
        Console.Write("Inserisci il tuo nome: ");
        string nome = Console.ReadLine();

        // Richiesta dell'età all'utente
        Console.Write("Inserisci la tua età: ");
        string inputEta = Console.ReadLine();

        // Conversione dell'età da stringa a intero
        int eta;
        if (int.TryParse(inputEta, out eta))
        {
            Console.WriteLine($"Ciao, {nome}! Hai {eta} anni.");
        }
        else
        {
            Console.WriteLine("L'età inserita non è valida.");
        }
    }
}
