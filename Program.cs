using System;
using System.Collections.Generic;

namespace ConsoleSudoku {
    public class Program {

        static void Main(string[] args) {
            Console.SetWindowSize(90, 40);

            Game.Play();
            
            Console.ReadKey();
        }
    }
}
