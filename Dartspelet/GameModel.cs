using System;
using System.Collections.Generic;
using System.Text;

namespace Dartspelet
{
    public class GameModel
    {
        public string Name { get; set; } // Name of the game
        public List<PlayerModel> PlayerList { get; set; } // List of the players in the game.
        public PlayerModel CurrentPlayer { get; set; }
        public Turn CurrentTurn { get; set; } = new Turn(); // For displaying the points of the turned played at the moment. First set to dummy.
        public PlayerModel Winner { get; set; }  // Winner of the game.
        public bool GameEnd { get; set; } = false; // Is the game finished?
        public PlayerModel HighScore { get; set; } = new PlayerModel("dummy", 0, false); // The player with the highest score.
        public int WinningScore { get; set; } = 301; // The score player have to reach to win the game.

        // Constructor
        public GameModel(string name, int score)
        {
            this.Name = name;
            this.WinningScore = score; // Target score to win
            this.PlayerList = new List<PlayerModel>();
        }
        // Adding player to game. 
        public void AddPlayer(PlayerModel player)
        {
            PlayerList.Add(player);
            Console.WriteLine($" {player.ToString()} added to the game {this.Name}");

        }

        public void PlayGame()
        {
            throw new NotImplementedException();    //TODO Add structure to method play game.
        }

        

        public override string ToString()
        {
            // Make a string with game name and all player names and scores.
            StringBuilder output = new StringBuilder();
            output.Append($" Game name = {Name}. Players: ");
            if (PlayerList != null)
            {
                foreach (var player in PlayerList)
                {
                    output.Append($"{player.Name}, {player.Score} | "); // Add all player names and scores.
                }
            }
            
            else output.Append("None."); // If no players added, say "None"

            return output.ToString(); // Return string built.
        }
    }
}
