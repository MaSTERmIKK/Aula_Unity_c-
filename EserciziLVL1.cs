using System;

class Program
{
    static void Main()
    {
        Console.Write("Inserisci un numero intero: ");
        int numero = int.Parse(Console.ReadLine());

        if (numero % 2 == 0)
        {
            Console.WriteLine("Il numero è pari.");
        }
        else
        {
            Console.WriteLine("Il numero è dispari.");
        }
    }
}


---------------------------------------------------------


  using System;

class Program
{
    static void Main()
    {
        Console.Write("Inserisci una frase: ");
        string frase = Console.ReadLine();

        // Suddivisione della frase in parole basandosi sugli spazi
        string[] parole = frase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Conteggio delle parole
        int numeroParole = parole.Length;

        Console.WriteLine("Numero di parole: " + numeroParole);
    }
}
