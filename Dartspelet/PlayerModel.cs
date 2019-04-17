using System;
using System.Collections.Generic;

namespace Dartspelet
{
    class PlayerModel
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int Score { get; set; }
        public List<Turn> Turns { get; set; }
        public bool Human { get; set; }


        /// <summary>
        /// Creates a player with a name and a skill level.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skill"></param>
        public PlayerModel(string name, int skill, bool human)
        {
            this.Name = name;
            this.SkillLevel = skill;
            this.Turns = new List<Turn>();
            this.Human = human;
        }

        /// <summary>
        /// Add a turn to player.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="three"></param>
        public void AddTurn(int one, int two, int three)
        {
            Turn t = new Turn(one, two, three);
            this.Turns.Add(t);
            Console.WriteLine($"Turn added. Score recorded were {one}, {two} and {three}.");
        }

        // Add all scores together from all throws in all turns. 
        public int CalculateScore()
        {
            int total = 0;
            foreach(var turn in Turns)
            {
                total += turn.GetScore();
            }
            return total;
        }


        // Use ToString to get this Players info.
        public override string ToString()
        {
            return $" Name: {this.Name}, Skill level: {this.SkillLevel}, Human: {this.Human}.";
        }
    }
}
