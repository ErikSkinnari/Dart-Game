using System;

namespace Dartspelet
{
    public static class Graphics
    {
        /// <summary>
        /// Prints out the header on top of the window.
        /// </summary>
        public static void GameHeader()
        {
            Console.Clear();
            Console.WriteLine($"¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine($"¤|                                                                                                |¤");
            Console.WriteLine($"¤|                                        The Dart Game                                           |¤");
            Console.WriteLine($"¤|                                                                                                |¤");
            Console.WriteLine($"¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine();

            // Print out the name of the game
            PrintInBlue(false, " Game name: ");
            Console.Write($"{ Program.ActiveGame.Name} |");

            // Print out the high score and the player holding it.
            PrintInBlue(false, " Current highest score: ");
            Console.Write($"{ Program.ActiveGame.HighScore.CalculateScore()} ");
            PrintInBlue(false, "held by ");
            Console.WriteLine($"{ Program.ActiveGame.HighScore.Name}. ");
            Console.WriteLine();

            // Print out the target score.
            PrintInGreen(true, $" Target score for this game is: {Program.ActiveGame.WinningScore}.");
            Console.WriteLine();

            // List all players in game with Name, Score and Skill Level.
            PrintInBlue(true, " Players:");
            foreach (var player in Program.ActiveGame.PlayerList) 
            {
                Graphics.PrintInGreen(false, $" {player.Name}");
                Console.Write(" | With total score: ");
                Graphics.PrintInBlue(false, $"{ player.CalculateScore()}");
                Console.Write(" | and Skill Level: ");
                Graphics.PrintInBlue(true, $"{ player.SkillLevel}%");
            }
            Console.WriteLine();

            // Print out info about the active player.
            PrintInGreen(false, " Active player: ");
            Graphics.PrintInGreen(false, $"{Program.ActiveGame.CurrentPlayer.Name}");
            Console.Write(" | ");
            PrintInBlue(false, "Skill Level: ");
            Console.WriteLine($"{Program.ActiveGame.CurrentPlayer.SkillLevel}%");

            PrintInBlue(false, $" {Program.ActiveGame.CurrentPlayer.Name}'s total score of previous turns: ");
            Console.WriteLine($"{Program.ActiveGame.CurrentPlayer.CalculateScore()}"); // Print Current players total score.

            // Print out this turn.
            PrintInBlue(false, " Score this turn: ");
            Console.Write($" {Program.ActiveGame.CurrentTurn.ThrowOne} |");
            Console.Write($" {Program.ActiveGame.CurrentTurn.ThrowTwo} |");
            Console.Write($" {Program.ActiveGame.CurrentTurn.ThrowThree} |");
            Console.Write($" Total points this turn: {Program.ActiveGame.CurrentTurn.GetScore()}.");
            Console.WriteLine();
            Console.WriteLine();

        }

        /// <summary>
        /// Header. Used before game starts. Just printing out "The Dart Game" in a frame.
        /// </summary>
        public static void Header()
        {
            Console.Clear();
            Console.WriteLine($"¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine($"¤|                                                                                                |¤");
            Console.WriteLine($"¤|                                        The Dart Game                                           |¤");
            Console.WriteLine($"¤|                                                                                                |¤");
            Console.WriteLine($"¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            Console.WriteLine();
        }

        /// <summary>
        /// Console.Write message in green color. Pass Line false for Console.Write, if true Console.WriteLine is called.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintInGreen(bool line, string message)
        {
            if (line)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Console.Write message in red color. Pass Line false for Console.Write, if true Console.WriteLine is called.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintInRed(bool line, string message)
        {
            if (line)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Console.Write message in blue color. Pass Line false for Console.Write, if true Console.WriteLine is called.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintInBlue(bool line, string message)
        {
            if (line)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(message);
                Console.ResetColor();
            }
        }
    }
}
