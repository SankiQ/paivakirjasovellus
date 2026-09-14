using paivakirjasovellus;
using System;
using System.Collections.Generic;

public class DiaryManager
{
    private List<DiaryEntry> entries;
    private readonly FileHandler fileHandler;

    public DiaryManager()
    {
        fileHandler = new FileHandler();
        entries = fileHandler.LataaMerkinnat(); // UUSI: ladataan vanhat merkinnät käynnistyksessä
    }

    public void AddEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Uusi merkintä ");
        Console.ResetColor();

        Console.Write("\n  Otsikko: ");
        string title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Virhe: otsikko ei voi olla tyhjä!");
            Console.ResetColor();
            return;
        }

        Console.Write("  Sisältö: ");
        string content = Console.ReadLine();

        int newId = entries.Count + 1;

        DiaryEntry entry = new DiaryEntry(newId, title, content);
        entries.Add(entry);

        fileHandler.TallennaMerkinnat(entries); // UUSI

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n   Merkintä lisätty! ({entry.Date:dd.MM.yyyy})");
        Console.ResetColor();
    }

    public void ShowAllEntries()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Kaikki merkinnät ");
        Console.ResetColor();

        if (entries.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Ei merkintöjä vielä.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine();
        foreach (DiaryEntry entry in entries)
        {
            Console.WriteLine($"  {entry}");
        }
    }

    public void EditEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Muokkaa merkintää ");
        Console.ResetColor();

        ShowAllEntries();

        if (entries.Count == 0) return;

        Console.Write("\n  Anna muokattavan merkinnän numero: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= entries.Count)
        {
            DiaryEntry entry = entries[index - 1];

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Nykyinen otsikko: {entry.Title}");
            Console.ResetColor();
            Console.Write("  Uusi otsikko (Enter säilyttää vanhan): ");
            string newTitle = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newTitle))
                entry.Title = newTitle;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Nykyinen sisältö: {entry.Content}");
            Console.ResetColor();
            Console.Write("  Uusi sisältö (Enter säilyttää vanhan): ");
            string newContent = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newContent))
                entry.Content = newContent;

            fileHandler.TallennaMerkinnat(entries); // UUSI

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n   Merkintä päivitetty!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  Virhe: virheellinen numero!");
            Console.ResetColor();
        }
    }

    public void DeleteEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Poista merkintä ");
        Console.ResetColor();

        ShowAllEntries();

        if (entries.Count == 0) return;

        Console.Write("\n  Anna poistettavan merkinnän numero: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= entries.Count)
        {
            string title = entries[index - 1].Title;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n  Poistetaanko \"{title}\"? (k/e): ");
            Console.ResetColor();
            string confirm = Console.ReadLine();

            if (confirm?.ToLower() == "k")
            {
                entries.RemoveAt(index - 1);
                UpdateIds();
                fileHandler.TallennaMerkinnat(entries); // UUSI

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n   Merkintä poistettu!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n  Peruutettu.");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  Virhe: virheellinen numero!");
            Console.ResetColor();
        }
    }

    private void UpdateIds()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].Id = i + 1;
        }
    }

    public int GetCount()
    {
        return entries.Count;
    }
}