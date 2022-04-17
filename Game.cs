using ConsoleSudoku.Actions;
using ConsoleSudoku.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku {
    public enum ESudokuDifficulty {
        Easy,
        Medium,
        Hard
    }

    public static class Game {
        public static ESudokuDifficulty Difficulty { get; private set; }
        public static int[,] Completed { get; set; }
        public static int[,] Hints { get; set; }
        public static int[,] Solution { get; set; }

        public static void Play() {
            DifficultyMenuScreen difficultyMenu = new DifficultyMenuScreen();
            difficultyMenu.Show();
            Difficulty = difficultyMenu.GetDifficulty();

            var generateCompleteSudokuAction = new GenerateCompleteSudokuAction();
            generateCompleteSudokuAction.Execute();


            Solution = new int[9,9];
            GameBoardScreen board = new GameBoardScreen();
            board.Show();
        }
    }
}
