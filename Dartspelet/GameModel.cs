using System;
using System.Collections.Generic;

namespace Dartspelet
{
    public class GameModel
    {
        public string Name { get; set; } // Name of the game
        public List<PlayerModel> PlayerList { get; set; } // List of the players in the game.
        public PlayerModel CurrentPlayer { get; set; } // Keeps track of the player throwing.

        // For displaying the points of the turned played at the moment. First set to dummy.
        public Turn CurrentTurn { get; set; } = new Turn(); 

        public PlayerModel Winner { get; set; } // Winner of the game.
        public bool GameEnd { get; set; } = false; // Is the game finished?

        // The player with the highest score. Dummy before player takes the spot.
        public PlayerModel HighScore { get; set; } = new PlayerModel("", 0, false); 

        public int WinningScore { get; set; } = 301; // The score player have to reach to win the game.


        // Constructor
        public GameModel(string name, int score)
        {
            this.Name = name;
            this.WinningScore = score;
            this.PlayerList = new List<PlayerModel>();
        }
        // Adding player to game. 
        public void AddPlayer(PlayerModel player)
        {
            PlayerList.Add(player);
            Console.WriteLine($" {player.ToString()} added to the game {this.Name}");
        }
    }
}
