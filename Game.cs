using ConsoleSudoku.Actions;
using ConsoleSudoku.Entities;
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
        public static int[,] Completed { get; set; } // completed sudoku to compare in the end
        public static int[,] Hints { get; set; } // to store all the hints of the sudoku
        public static int[,] Solution { get; set; } = new int[9, 9]; // to store the user input for the sudoku solution
        public static int Progress { get; set; } = 0; // to messure the time progress
        public static Stack<Move> Undo { get; set; } = new Stack<Move>(); // undo stack
        public static Stack<Move> Redo { get; set; } = new Stack<Move>(); // redo stack
        public static int AdditionalHintCount { get; set; } = 0;

        public static void Play() {
            DifficultyMenuScreen difficultyMenu = new DifficultyMenuScreen();
            difficultyMenu.Show();
            Difficulty = difficultyMenu.GetDifficulty();

            var generateCompleteSudokuAction = new GenerateCompleteSudokuAction();
            generateCompleteSudokuAction.Execute();

            GameBoardScreen board = new GameBoardScreen();
            board.Show();
        }
    }
}
