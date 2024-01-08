using System;
using System.Threading;


class Program
{
    static void Main()
    {
        int totalTimeInSeconds = 5 * 60; 
        int playerScore = 0;
        bool hasTorch = false;
        bool hasMap = false;
        int resetView = 10;
        DateTime lastView = DateTime.Now;

        Console.WriteLine("Witaj w Egipskiej Piramidzie!");
        DisplayAsciiArt();
        DisplayScoreAndTime(playerScore,totalTimeInSeconds );
        
        while (totalTimeInSeconds > 0)
        {
            if ((DateTime.Now - lastView).TotalSeconds >= resetView)
            {
                Console.Clear();
                DisplayAsciiArt();
                DisplayScoreAndTime(playerScore, totalTimeInSeconds);
                lastView = DateTime.Now;
            }
            // Zmiana koloru dla wejść gracza
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("E. Przeszukaj pokój");
            Console.WriteLine("P. Rozwiąż łamigłówkę");
            Console.WriteLine("I. Sprawdź dostepne przedmioty");
            Console.WriteLine("U. Użyj przedmiotu");
            
            // Nie można wyjść z piramidy bez pewego pułapu punktów
            if (playerScore >= 50)
            {
                Console.WriteLine("X. Wyjdź z piramidy");
            }
            else
            {
                Console.WriteLine($"X. Wyjdź z piramidy (Wymagane minimum 50 punktów, obecny wynik: {playerScore})");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char userInput = char.ToUpper(keyInfo.KeyChar);

            // Przetwarzanie wejścia gracza
            switch (userInput)
            {
                case 'E':
                    ExploreRoom(ref hasTorch, ref hasMap, ref playerScore);
                    //Zmiany dokonane na parametrze wewnątrz metody wpływają na oryginalną zmienną poza metodą
                    break;

                case 'P':
                    Console.WriteLine("\nRozwiązujesz skomplikowaną łamigłówkę!");
                    playerScore += 20;
                    break;

                case 'I':
                    Console.WriteLine("\nPrzedmioty:");
                    Console.WriteLine(hasTorch ? "   Pochodnia" : "   (Brak pochodni)");
                    Console.WriteLine(hasMap ? "   Mapa" : "   (Brak mapy)");
                    break;

                case 'U':
                    UseItem(hasTorch, hasMap, ref playerScore, ref totalTimeInSeconds);
                    break;

                case 'X':
                    if (playerScore >= 50)
                    {
                        Console.WriteLine("\nOpuszczanie piramidy...");
                        Thread.Sleep(2000);
                        Console.WriteLine("Gratulacje! Udało Ci się uciec z piramidy!");
                        Console.WriteLine($"Twój końcowy wynik: {playerScore}");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine($"\nNie możesz jeszcze opuścić piramidy. Potrzebujesz minimum 50 punktów, a twój obecny wynik to: {playerScore}");
                    }
                    break;

                default:
                    Console.WriteLine("\nNieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
            totalTimeInSeconds -= 20; // Odjęcie 20 sekund za każdym razem, żeby gra była atrakcyjna

            // Dodanie opóźnienia
            Thread.Sleep(1000);
        }
        Console.Clear(); 
        Console.WriteLine("Skończył się czas! Nie udało Ci się uciec z piramidy na czas.");
        Console.WriteLine($"Twój końcowy wynik: {playerScore}");
    }

    static void ExploreRoom(ref bool hasTorch, ref bool hasMap, ref int playerScore)
    {
        Console.WriteLine("\nSprawdzasz pomieszczenie...");

        // Losowe znajdowanie przedmiotów akcji
        Random random = new Random();
        int chance = random.Next(1, 101);

        if (chance <= 40) // 40% szansy znalezienia
        {
            Console.WriteLine("Znajdujesz ukrytą skrzynię!");
            
            int itemChance = random.Next(1, 3);

            switch (itemChance)
            {
                case 1:
                    if (!hasTorch)
                    {
                        hasTorch = true;
                        Console.WriteLine("Znajdujesz pochodnię!");
                        playerScore += 5;
                    }
                    else
                    {
                        Console.WriteLine("Znajdujesz coś, ale już masz pochodnię.");
                    }
                    break;

                case 2:
                    if (!hasMap)
                    {
                        hasMap = true;
                        Console.WriteLine("Znajdujesz mapę!");
                        playerScore += 10;
                    }
                    else
                    {
                        Console.WriteLine("Znajdujesz coś, ale już masz mapę.");
                    }
                    break;

                default:
                    Console.WriteLine("Nie znajdujesz żadnych użytecznych przedmiotów.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Nie znajdujesz niczego użytecznego.");
        }
    }

    static void UseItem(bool hasTorch, bool hasMap, ref int playerScore, ref int totalTimeInSeconds)
    {
        Console.WriteLine("\nWybierz przedmiot do użycia:");

        if (hasTorch)
        {
            Console.WriteLine("P. Użyj pochodni");
        }

        if (hasMap)
        {
            Console.WriteLine("M. Użyj mapy");
        }

        ConsoleKeyInfo itemKeyInfo = Console.ReadKey();
        char itemChoice = char.ToUpper(itemKeyInfo.KeyChar);

        switch (itemChoice)
        {
            case 'P':
                if (hasTorch)
                {
                    Console.WriteLine("\nZapalasz pochodnię, oświetlając otoczenie.");
                    playerScore += 5;
                    totalTimeInSeconds += 30; 
                    Console.WriteLine("Masz więcej casu na znalezienie wyjścia, bo nie błądzisz już w cieności.");
                }
                else
                {
                    Console.WriteLine("\nNie masz pochodni.");
                }
                break;

            case 'M':
                if (hasMap)
                {
                    Console.WriteLine("\nZdobywasz wiedzę o układzie piramidy.");
                    playerScore += 10;
                    Console.WriteLine("Łatwiej ci znaleźć wyjście.");
                }
                else
                {
                    Console.WriteLine("\nNie masz mapy.");
                }
                break;

            default:
                Console.WriteLine("\nNieprawidłowy wybór przedmiotu.");
                break;
        }
    }
    
        static void DisplayScoreAndTime(int score, int time)
        {
            Console.WriteLine($"Twój wynik: {score}  |  Pozostały czas: {time / 60:D2}:{time % 60:D2}");
        }

        static void DisplayAsciiArt()
        {
            Console.WriteLine(@"
               .
              /=\\
             /===\ \
            /=====\' \
           /=======\'' \
          /=========\ ' '\
         /===========\''   \
        /=============\ ' '  \
       /===============\   ''  \
      /=================\' ' ' ' \
     /===================\' ' '  ' \
    /=====================\' '   ' ' \
   /=======================\  '   ' /
  /=========================\   ' /
 /===========================\'  /
/=============================\/
                    
");
        }
    
}