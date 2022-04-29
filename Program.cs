using System;
using System.Collections.Generic;

namespace ConsoleSudoku {
    public class Program {

        static void Main(string[] args) {
            Console.SetWindowSize(95, 40); // set windows size
            Game.Play(); // start the game
        }
    }
}
