//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2023
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>gerhard.ahrens@lifeprojects.de</email>
// <date>30.06.2023</date>
//
// <summary>
// Darstellen eines Weihnachtsbaum 
// </summary>
//-----------------------------------------------------------------------



namespace ChristmasTree
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public static class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private const int SW_MAXIMIZE = 3;

        private static void Main(string[] args)
        {

            int width = default;

            // Get the width of the tree from the first command line argument
            if (args.Length == 0)
            {
                width = 40;
            }
            else
            {
                width = int.Parse(args[0]);
            }

            if (width > 40)
            {
                ConsoleWindowsMaximize();
            }

            ChristmasTree(width);

            ConsoleKey choice;

            do
            {
                choice = Console.ReadKey(true).Key;
                if (choice == ConsoleKey.N)
                {
                    ChristmasTree(width);
                }

            } while (choice != ConsoleKey.X);


        }

        private static void ChristmasTree(int width)
        {
            Console.Clear();

            /* Berechnen Sie die Anzahl der Zeilen, die für die Spitze des Baums benötigt werden */
            int numRows = (width + 1) / 2;

            /* Zufallszahl erstellen */
            var rand = new Random();

            /* Die Spitze des Baumes erstellen */
            for (int i = 0; i < numRows; i++)
            {
                /* Berechnen Sie die Anzahl der Sternchen für diese Zeile */
                int numAsterisks = (i * 2) + 1;

                /* Berechnen Sie die Anzahl der Leerzeichen auf beiden Seiten der Sternchen */
                int numSpaces = (width - numAsterisks) / 2;

                /* Erstellen einer zufällige Anzahl von Ornamenten für diese Zeile */
                int numOrnaments = rand.Next(numAsterisks + 1);

                /* Erstellen Sie eine Liste von Ornamenten für diese Reihe */
                var ornaments = Enumerable.Repeat('o', numOrnaments).Concat(Enumerable.Repeat('*', numAsterisks - numOrnaments)).ToList();

                // Shuffle the ornaments
                ornaments = ornaments.OrderBy(x => rand.Next()).ToList();

                /* Zufällige Anordung von Ornamente erstellen */
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(new string(' ', numSpaces));
                for (int j = 0; j < ornaments.Count; j++)
                {
                    if (ornaments[j] == 'o')
                    {
                        /* Erstelle beliebige Farbe für ein Ornament */
                        ConsoleColor color = (ConsoleColor)rand.Next(1, 16);
                        Console.ForegroundColor = color;
                    }

                    Console.Write(ornaments[j]);
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine(new string(' ', numSpaces));
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;

            if (width < 30)
            {
                Console.WriteLine(new string(' ', width / 2) + "|" + new string(' ', width / 2));
                Console.WriteLine(new string(' ', width / 2) + "|" + new string(' ', width / 2));
            }
            else
            {
                Console.WriteLine(new string(' ', (width / 2) -2 ) + "|||" + new string(' ', width / 2));
                Console.WriteLine(new string(' ', (width / 2) - 2) + "|||" + new string(' ', width / 2));
            }

            Console.ResetColor();
            Console.WriteLine();

            /* der Baum kann jedes Jahr verwendet werden */
            string message = $"Merry Christmas {DateTime.Now.Year}!\n'X' = Exit, 'N' = New";
            Console.ForegroundColor = ConsoleColor.White;
            if (message.Length > width)
            {
                width = message.Length + 1;
            }

            Console.WriteLine(new string(' ', (width - message.Length) / 2) + message + new string(' ', (width - message.Length) / 2));
        }

        private static void ConsoleWindowsMaximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, SW_MAXIMIZE);
        }
    }
}