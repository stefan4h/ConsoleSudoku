using System;
using System.Collections.Generic;

namespace ConsoleSudoku {
    public class Program {

        static void Main(string[] args) {
            Game game = new Game();
            game.Play();
            
            Console.ReadKey();
        }
    }
}
