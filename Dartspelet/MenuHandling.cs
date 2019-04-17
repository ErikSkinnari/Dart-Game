using System;
using System.Threading;

namespace Dartspelet
{
    public class MenuHandling
    {
        /// <summary>
        /// Main menu. Menu options is New Game, Settings and Exit.
        /// </summary>
        public static void MainMenu()
        {
            string[] menuOptions = new string[] {" Play Game", " Exit" };
            int menuSelected = 0;

            while (true)
            {
                Graphics.Header();
                Console.CursorVisible = false;

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    // If this is the selected menu option, print in red.
                    if (menuSelected == i) Graphics.PrintInBlue(true, menuOptions[i]);
                    else Console.WriteLine(menuOptions[i]);
                }

                // Read user input key.
                var keyPressed = Console.ReadKey();

                // If user presses DownArrow, scroll down in menu as long as it is more options.
                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelected != menuOptions.Length - 1) menuSelected++;
                // If user presses UpArrow, scroll up as long as the first option is not the selected one.
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelected >= 1) menuSelected--;

                // If enter is pressed, run method 
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    switch (menuSelected)
                    {
                        case 0:
                            GameMechanics.NewGame();
                            break;
                            
                        default: // Go to exit method.
                            ExitApplication();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Ask the user to enter a name for the game.
        /// </summary>
        /// <returns></returns>
        public static string GameName()
        {
            Graphics.Header();
            Graphics.PrintInGreen(true, " -- New Game -- ");
            Console.Write(" Enter name of the game to be created: ");
            Console.CursorVisible = true;
            var gameName = Console.ReadLine(); // Let user give name to game. Temporary stored in gameName.
            Console.CursorVisible = false;
            return gameName;
        }

        /// <summary>
        /// Ask the user to enter a name of the player.
        /// </summary>
        /// <returns></returns>
        public static string PlayerName()
        {
            Graphics.Header();
            Graphics.PrintInGreen(true, " -- Add player to game -- ");
            Console.Write(" Enter name of the player: ");
            Console.CursorVisible = true;
            var playerName = Console.ReadLine(); // Let user give name to player. Temporary stored in playerName.
            Console.CursorVisible = false;
            return playerName;
        }

        /// <summary>
        /// Method to set the skill level of the player.
        /// </summary>
        /// <returns></returns>
        public static int PlayerSkill()
        {
            Graphics.Header();
            Console.WriteLine(" Now it´s time to set the skill level of the player.");
            Graphics.PrintInGreen(true, " You need to press any key when the \"X\" is as near the center of the animation (green) as possible.");
            Console.WriteLine(" Press enter to continue");

            Console.ReadLine();
            Graphics.Header();
            int i = 1;
            var noInput = true;
            bool up = true; // This bool keeps track of the direction of animation.
            Console.CursorVisible = false;

            while (noInput)
            {
                // Check for input
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    noInput = false; // If user presses a key noInput is false
                }

                // Animate X going rigth and left.
                if (i > 100) { up = false; }
                if (i < 1) { up = true; }
                if (up) { i++; }
                else i--;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(i, Console.CursorTop - 1);

                // Coloring the X'es depending on horizontal position. Green in the middle.
                if (i < 25) { Console.ForegroundColor = ConsoleColor.Red; }
                else if (i < 45) { Console.ForegroundColor = ConsoleColor.Yellow; }
                else if (i < 55) { Console.ForegroundColor = ConsoleColor.Green; }
                else if (i < 75) { Console.ForegroundColor = ConsoleColor.Yellow; }
                else { Console.ForegroundColor = ConsoleColor.Red; }

                Console.Write("X");
                Console.ResetColor();

                // Setting the speed of the animation.
                if (i < 25) { Thread.Sleep(4); }
                else if (i < 45) { Thread.Sleep(3); }
                else if (i < 55) { Thread.Sleep(2); }
                else if (i < 75) { Thread.Sleep(3); }
                else { Thread.Sleep(4); }
            }

            // A little formula to set the players skill level. Max value at start is 80.
            int playerSkill = 70 - 2*(Math.Abs(50 - i));

            // And a fix if the skill level gets too low.
            if (playerSkill < 30) playerSkill = 30;

            Graphics.Header();
            Graphics.PrintInGreen(true, $" Players skill level set to {playerSkill}.");
            Console.WriteLine(" Press enter to continue");
            Console.ReadLine();

            return playerSkill;
        }

        /// <summary>
        /// Sets the player to human or computer.
        /// </summary>
        /// <returns></returns>
        public static bool PlayerHuman()
        {
            Graphics.Header();
            Console.WriteLine(" Should player be controlled by human or by computer? h/c");
            while(true)
            {
                // Set player as human or a computer
                var keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.H) return true;
                else if (keyPressed.Key == ConsoleKey.C) return false;
            }
        }

        /// <summary>
        /// Setting the winning score of the game.
        /// </summary>
        /// <returns></returns>
        public static int GameWinningScore()
        {
            Graphics.Header();
            Console.Write(" What should be the winning score of the game? (Leave blank to use default value of 301 points): ");
            int gameWinningScore; // Temporary holds the value of the winning score.
            Console.CursorVisible = true;
            string input = Console.ReadLine();
            Console.CursorVisible = false;
            if (input == "") // No input, go with default value.
            {
                Console.WriteLine(" Winning score set to 301!");
                Program.Pause();
                gameWinningScore = 301; // Default points to reach.
                
            }
            else // If user enters any input, validate input and try to parse to int.
            {
                while (!int.TryParse(input, out gameWinningScore)) // If parsing fails.
                {
                    Graphics.Header();
                    Console.WriteLine(" Must be a whole number. Try again: ");
                    input = Console.ReadLine();
                }
                Console.WriteLine($" Winning score set to {gameWinningScore}");
            }

            return gameWinningScore;
        }

        /// <summary>
        /// Closes the application after user confirmation.
        /// </summary>
        public static void ExitApplication()
        {
            Graphics.Header();
            Graphics.PrintInRed(true, " Do you want to exit the app? y/n");
            var keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Y)
            {
                Environment.Exit(0);
            }
            else MainMenu();
        }
    }
}
