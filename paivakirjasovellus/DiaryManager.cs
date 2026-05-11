using System;
using System.Collections.Generic;

public class DiaryManager
{
    // Lista johon kaikki päiväkirjamerkinnät tallennetaan muistiin
    private List<DiaryEntry> entries = new List<DiaryEntry>();

    // Lisää uuden merkinnän listaan
    public void AddEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Uusi merkintä ");
        Console.ResetColor();

        // Pyydetään käyttäjältä otsikko
        Console.Write("\n  Otsikko: ");
        string title = Console.ReadLine();

        // Tarkistetaan että otsikko ei ole tyhjä
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Virhe: otsikko ei voi olla tyhjä!");
            Console.ResetColor();
            return; // Poistutaan metodista jos otsikko on tyhjä
        }

        // Pyydetään käyttäjältä sisältö
        Console.Write("  Sisältö: ");
        string content = Console.ReadLine();

        // Lasketaan uusi ID: listan pituus + 1
        int newId = entries.Count + 1;

        // Luodaan uusi DiaryEntry-olio ja lisätään se listaan
        DiaryEntry entry = new DiaryEntry(newId, title, content);
        entries.Add(entry);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n   Merkintä lisätty! ({entry.Date:dd.MM.yyyy})");
        Console.ResetColor();
    }

    // Näyttää kaikki merkinnät listasta
    public void ShowAllEntries()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Kaikki merkinnät ");
        Console.ResetColor();

        // Jos lista on tyhjä, ilmoitetaan siitä käyttäjälle
        if (entries.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Ei merkintöjä vielä.");
            Console.ResetColor();
            return;
        }

        // foreach käy listan jokaisen merkinnän läpi yksi kerrallaan
        Console.WriteLine();
        foreach (DiaryEntry entry in entries)
        {
            // Kutsuu DiaryEntry-luokan ToString()-metodia automaattisesti
            Console.WriteLine($"  {entry}");
        }
    }

    // Muokkaa olemassa olevaa merkintää
    public void EditEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Muokkaa merkintää ");
        Console.ResetColor();

        // Näytetään ensin kaikki merkinnät jotta käyttäjä tietää mitä voi muokata
        ShowAllEntries();

        // Jos ei ole merkintöjä, ei ole mitään muokattavaa
        if (entries.Count == 0) return;

        Console.Write("\n  Anna muokattavan merkinnän numero: ");

        // int.TryParse muuntaa tekstin numeroksi turvallisesti
        // Jos syöte ei ole numero tai on väärällä välillä, mennään else-haaraan
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= entries.Count)
        {
            // Haetaan muokattava merkintä listasta (index - 1 koska lista alkaa nollasta)
            DiaryEntry entry = entries[index - 1];

            // Muokataan otsikkoa
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Nykyinen otsikko: {entry.Title}");
            Console.ResetColor();
            Console.Write("  Uusi otsikko (Enter säilyttää vanhan): ");
            string newTitle = Console.ReadLine();

            // Päivitetään vain jos käyttäjä kirjoitti jotain
            if (!string.IsNullOrWhiteSpace(newTitle))
                entry.Title = newTitle;

            // Muokataan sisältöä
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Nykyinen sisältö: {entry.Content}");
            Console.ResetColor();
            Console.Write("  Uusi sisältö (Enter säilyttää vanhan): ");
            string newContent = Console.ReadLine();

            // Päivitetään vain jos käyttäjä kirjoitti jotain
            if (!string.IsNullOrWhiteSpace(newContent))
                entry.Content = newContent;

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

    // Poistaa merkinnän listasta
    public void DeleteEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n    Poista merkintä ");
        Console.ResetColor();

        // Näytetään ensin kaikki merkinnät
        ShowAllEntries();

        // Jos ei ole merkintöjä, ei ole mitään poistettavaa
        if (entries.Count == 0) return;

        Console.Write("\n  Anna poistettavan merkinnän numero: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= entries.Count)
        {
            // Tallennetaan otsikko muistiin vahvistusviesttiä varten
            string title = entries[index - 1].Title;

            // Pyydetään vahvistus ennen poistoa
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n  Poistetaanko \"{title}\"? (k/e): ");
            Console.ResetColor();
            string confirm = Console.ReadLine();

            if (confirm?.ToLower() == "k")
            {
                // Poistetaan merkintä listasta
                entries.RemoveAt(index - 1);

                // Päivitetään kaikkien jäljelle jäävien merkintöjen ID:t
                UpdateIds();

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

    // Päivittää kaikkien merkintöjen ID:t vastaamaan niiden paikkaa listassa
    // Esim. jos poistetaan merkintä 1, niin merkintä 2 saa ID:ksi 1 jne.
    private void UpdateIds()
    {
        // for-silmukka käy listan läpi indeksin avulla
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].Id = i + 1; // i alkaa nollasta, joten lisätään 1
        }
    }

    // Palauttaa merkintöjen määrän
    public int GetCount()
    {
        return entries.Count;
    }
}