using System;

class Program
{
    static void Main(string[] args)
    {
        DiaryManager manager = new DiaryManager();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║      ✦ PÄIVÄKIRJA-SOVELLUS v1.0 ✦      ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  ┌─ VALIKKO ────────────────────────────┐");
            Console.ResetColor();
            Console.WriteLine("  │                                      │");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  │  1");
            Console.ResetColor();
            Console.WriteLine("  Lisää uusi merkintä              │");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  │  2");
            Console.ResetColor();
            Console.WriteLine("  Näytä kaikki merkinnät           │");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  │  3");
            Console.ResetColor();
            Console.WriteLine("  Tallenna ja lopeta               │");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  └──────────────────────────────────────┘");
            Console.ResetColor();

            Console.Write("\n  Valintasi → ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.AddEntry();
                    Console.WriteLine("\n  Paina Enter jatkaaksesi...");
                    Console.ReadLine();
                    break;
                case "2":
                    manager.ShowAllEntries();
                    Console.WriteLine("\n  Paina Enter jatkaaksesi...");
                    Console.ReadLine();
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n  Nähdään taas! Ohjelma suljetaan...");
                    Console.ResetColor();
                    running = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Virheellinen valinta, yritä uudelleen!");
                    Console.ResetColor();
                    Console.WriteLine("  Paina Enter jatkaaksesi...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}