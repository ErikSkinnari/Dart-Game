//
// A dart game simulator. By Erik Skinnari 2019-04-17
//

using System.Threading;

namespace Dartspelet
{
    class Program
    {
        private static readonly int delayTime = 100; // Setting the delay time. Adjusts the game speed.  
        // If all players is controlled by computer set to low number. 10 for simulation, maybee 1000 if you want to have
        // a chance to see what happens. If player is controlled by human the delay should be at least 2000.

        public static int[] dartBoard = new int[]{20,1,18,4,13,6,10,15,2,17,3,19,7,16,8,11,14,9,12,5}; // The dart board. 
        public static GameModel ActiveGame; // Keeps track of the game.

        static void Main(string[] args)
        {
            MenuHandling.MainMenu(); // Start with main menu.
        }

        /// <summary>
        /// This delay is what decides how fast the game is played. DelayTime set in Program.cs
        /// </summary>
        public static void Pause()
        {
            Thread.Sleep(delayTime); 
        }
    }
}
