using System;

namespace Dartspelet
{
    static class GameMechanics
    {
        static Random chance = new Random(); // Used to calculate the probability that the player hits desired field on board.

        public static int Throw(PlayerModel player, GameModel game)
        {
            Graphics.GameHeader();
            Program.Pause();

            int aim = 0;

            if (player.Human == true)
            {
                Graphics.GameHeader();
                Console.Write($" OK {player.Name}. Time to make a throw! Your score is {player.CalculateScore() + game.CurrentTurn.GetScore()}. " +
                    $"What is your aim? (1-20) [0 to auto aim]: ");
                Console.CursorVisible = true;

                aim = -1;

                while (aim < 0 || aim > 20)
                {
                        while (!int.TryParse(Console.ReadLine(), out aim)) // If parsing fails.
                        {
                            Graphics.GameHeader();
                            Console.WriteLine(" Must be a whole number between 1 and 20. Try again: ");
                        }
                }

                Console.CursorVisible = false;
            }

            // First handle if the user dont aim on a field. "Auto Aim" 
            if (aim < 1)
            {
                int pointDifference = game.WinningScore - (player.CalculateScore() + game.CurrentTurn.GetScore());

                if (pointDifference < 20) // If player is close to the winning score, set aim to the desired point field.
                {
                    aim = pointDifference;
                }
                else aim = 20; // Else set the aim to 20. 
            }

            int aimIndex = Array.IndexOf(Program.dartBoard, aim); // Gets the index of where in dartBoard array the value.

            //Random chance = new Random(); // Used to calculate the probability that the player hits desired field on board.
            if (chance.Next(1, 101) < player.SkillLevel) // Calculates the probability that the player hits the desired point.
            {
                if (player.SkillLevel < 100)
                {
                    if (chance.Next(1,6) == 1) // 1/5 chance of player increasing the Skill Level.
                    {
                        player.SkillLevel++; // Skill increased (max 100)!
                        Console.WriteLine($" {player.Name} plays so good that the skill level " +
                            $"increases one point! Skill level is now {player.SkillLevel}.");
                    }
                    
                }
                Console.WriteLine($" Wow! Right on target! {aim} points added to {player.Name}'s turn! ");
                Program.Pause();
                return aim;
            }
            else // Player misses the desired point. Now calculate if player hits adjacent point field.
            {
                Console.WriteLine($" {player.Name} missed the field aimed for.  ");
                if (chance.Next(1, 101) < player.SkillLevel)
                {
                    //Player hits one of two adjacent fields. Now we calculate if it is clocwise or CCW. 
                    int direction = chance.Next(1, 3);

                    if (direction == 1) // Counter clockwise
                    {
                        if (aimIndex <= 0) // If aim is on the first index of array
                        {
                            aimIndex = Program.dartBoard.Length - 1; // Return last index as points. (One step CCW)
                        }
                        else aimIndex--; // Return the point on index one less than players aim.

                        Console.WriteLine($" {player.Name} hit the adjacent field, worth {Program.dartBoard[aimIndex]} points.  ");
                        Program.Pause();
                    }

                    else if (direction == 2) // Clockwise
                    {
                        if (aimIndex == Program.dartBoard.Length - 1) // If aim is on the last index of array
                        {
                            aimIndex = 0; // Return first index as points. (One step CW)
                        }
                        else aimIndex++; // Return the point on index one less than players aim.

                        Console.WriteLine($" {player.Name} hit the adjacent field, worth {Program.dartBoard[aimIndex]} points.  ");
                        Program.Pause();
                    }
                }
                else // Player misses the aim, and also misses the adjacent point fields. Now we randomize the hit.
                {
                    int randomHit = chance.Next(1, 20);

                    // If random hit has same value as the aimed index or one of the two adjacent, User misses the board completely
                    if (randomHit == aimIndex || randomHit == aimIndex - 1 || randomHit == aimIndex + 1)
                    {
                        player.SkillLevel--; // The player missed the target. Skill decreased!
                        Graphics.GameHeader();
                        Console.WriteLine(" FAIL! ");
                        Console.WriteLine($" {player.Name} missed the board completely. :( ");
                        Graphics.PrintInRed(true, $" {player.Name} is so nervous that the skill level " +
                            $"decreases one point! Skill level is now {player.SkillLevel}.");
                        Program.Pause();
                        return 0; // Miss!
                    }
                    else
                    {
                        aimIndex = randomHit;
                        Console.WriteLine($" {player.Name} hit the field worth {Program.dartBoard[aimIndex]} points.");
                        Program.Pause();
                    }
                }
            }

            return Program.dartBoard[aimIndex]; // Send back the points hit.
        }

        /// <summary>
        /// Main method responsible for the handling of a game. Runs as long as no player hade triggered the GameEnd boolean to true.
        /// </summary>
        /// <param name="game"></param>
        private static void Game(GameModel game)
        {
            while (!game.GameEnd)
            {
                foreach (var player in game.PlayerList)
                {
                    game.CurrentPlayer = player; // Set current player, used for displaying whos turn it is.


                    Graphics.GameHeader(); // Update header so active player is right.

                    Graphics.PrintInRed(true, " Next Player!");
                    Program.Pause();
                    Graphics.GameHeader(); // Update header.

                    Turn t = PlayTurn(player, game);

                    // As long as this turn does not get the player OVER the winning score, add this turn to players total score.
                    if (player.CalculateScore() + t.GetScore() <= game.WinningScore)
                    {
                        player.Turns.Add(t); // Add turn to Turns.
                    }

                    // If this players score is the highest total make this player the HighScore.
                    if (player.CalculateScore() > game.HighScore.CalculateScore())
                    {
                        game.HighScore = player;
                    }

                    // If this player reaches the winning score, set player as winner.
                    if (player.CalculateScore() == game.WinningScore)
                    {
                        game.Winner = player;
                        game.GameEnd = true;
                    }
                }
            }
            GameOver(game); // Finish game.
        }

        /// <summary>
        /// Called when the game is over. Prints out all turns of the winner.
        /// </summary>
        /// <param name="game"></param>
        private static void GameOver(GameModel game)
        {
            Graphics.GameHeader();
            Console.WriteLine();
            Graphics.PrintInGreen(true, " The game is over!");
            Console.WriteLine();
            Graphics.PrintInGreen(true, $" Winner is {game.Winner.Name}.");
            Console.WriteLine($" {game.Winner.Name}'s turns are as follows:");
            Console.WriteLine();
            int i = 1;
            foreach (var turn in game.Winner.Turns)
            {
                Console.WriteLine($" Round {i}");
                turn.PrintTurn();
                i++;
            }
            Console.WriteLine();
            Console.WriteLine($" That adds up to the winning score {game.WinningScore} and this game is over.");
            Console.WriteLine(" Thank you for playing with us!");
            Console.WriteLine();
            Graphics.PrintInGreen(true, " Press Enter to continue to Main Menu.");
            Console.ReadLine();
            MenuHandling.MainMenu();
        }

        /// <summary>
        /// Controlling a new turn and adds it to the player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        private static Turn PlayTurn(PlayerModel player, GameModel game)
        {
            Turn t = new Turn();
            game.CurrentTurn = t;
            t.ThrowOne = GameMechanics.Throw(player, game);
            if (CheckEnd(player, game, t)) return t; // Check if user finished the game. If so, return this turn vith its current value.
            Graphics.GameHeader();
            t.ThrowTwo = GameMechanics.Throw(player, game);
            if (CheckEnd(player, game, t)) return t; // Check if user finished the game. If so, return this turn vith its current value.
            Graphics.GameHeader();
            t.ThrowThree = GameMechanics.Throw(player, game);
            if (CheckEnd(player, game, t)) return t; // Check if user finished the game. If so, return this turn vith its current value.

            Graphics.GameHeader();
            if (player.CalculateScore() + t.GetScore() > game.WinningScore)
            {
                t.ThrowOne = 0;
                t.ThrowTwo = 0;
                t.ThrowThree = 0;

                Console.WriteLine($" Player {player.Name} got to many points. This turn does not count.");
                Program.Pause();
            }
            Graphics.GameHeader();
            //Console.WriteLine(" Turn over. Press Enter to continue:");
            //Console.ReadLine();

            return t;
        }

        /// <summary>
        /// Checking if the current player have reached the Winning score.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool CheckEnd(PlayerModel player, GameModel game, Turn t)
        {
            if (player.CalculateScore() + t.GetScore() == game.WinningScore)
            {
                game.Winner = player;
                game.GameEnd = true;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// The creation of a new game. 
        /// </summary>
        public static void NewGame()
        {
            GameModel g = new GameModel(MenuHandling.GameName(), MenuHandling.GameWinningScore()); // Create new game.
            Program.ActiveGame = g;

            // Time to add players to the game.
            var addPlayer = true;
            do
            {
                PlayerModel p = new PlayerModel(MenuHandling.PlayerName(), MenuHandling.PlayerSkill(), MenuHandling.PlayerHuman());
                g.AddPlayer(p);

                // String to print human or computer depending on bool human.
                string type;
                if (p.Human) type = "HUMAN";
                else type = "COMPUTER";

                Graphics.Header();

                // Confirmation of new player
                Graphics.PrintInBlue(true, $" Added {type} controlled player {p.Name} with a skill level of {p.SkillLevel}.");
                Console.WriteLine();
                Graphics.PrintInGreen(true, " Add another player? y/n"); // Do the user want to add another player?

                // Check if user wants to add another player
                var keyPressed = Console.ReadKey();

                // If user presses Y, loop condition is not changed and loop goes around again.
                if (keyPressed.Key == ConsoleKey.Y)
                {
                    continue;
                }
                else addPlayer = false; // The user do not want another player, break out of loop and continue.

            } while (addPlayer);

            // Print all added players.
            Graphics.Header();
            Graphics.PrintInGreen(true, " Players added: ");
            foreach (var player in g.PlayerList)
            {
                Console.WriteLine(player.ToString());
            }

            Graphics.PrintInGreen(true, " To start the game press enter. ");
            Console.ReadLine();

            // Time to play!!! -------------------------------------------------------------------------
            Game(g);
        }
    }

}
