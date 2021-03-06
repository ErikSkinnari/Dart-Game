﻿using System;

namespace Dartspelet
{
    public class Turn
    {
        public int ThrowOne { get; set; } 
        public int ThrowTwo { get; set; }
        public int ThrowThree { get; set; }
        public int Skill { get; set; }

        public Turn() {} // Default constructor

        public Turn(int one, int two, int three, int skill) // Other constructor
        {
            this.ThrowOne = one;
            this.ThrowTwo = two;
            this.ThrowThree = three;
            this.Skill = skill;
        }

        /// <summary>
        /// Called to get the total score of this turns 3 throws.
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return this.ThrowOne + this.ThrowTwo + this.ThrowThree;
        }

        /// <summary>
        /// Prints a summary of this turn in text. 
        /// </summary>
        /// <returns></returns>
        public void PrintTurn()
        {
            Console.Write($" Throw one: {ThrowOne}");
            Console.SetCursorPosition(14, Console.CursorTop);
            Console.Write($" | Throw two: {ThrowTwo}");
            Console.SetCursorPosition(31, Console.CursorTop);
            Console.Write($" | Throw three: {ThrowThree}");
            Console.SetCursorPosition(49, Console.CursorTop);
            Console.Write($" | Total {this.GetScore()} points.");
            Console.SetCursorPosition(68, Console.CursorTop);
            Console.WriteLine($" | Skill level at this round: {this.Skill}");
        }
    }
}
