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

    public sealed class Game {
        private ESudokuDifficulty difficulty;

        private int[,] exampleSudoku = new int[9, 9];

        private void generateExample() {
            exampleSudoku[0, 1] = 4;
            exampleSudoku[0, 5] = 7;
            exampleSudoku[1, 2] = 1;
            exampleSudoku[1, 3] = 8;
            exampleSudoku[1, 4] = 9;
            exampleSudoku[1, 5] = 4;
            exampleSudoku[2, 0] = 8;
            exampleSudoku[2, 1] = 2;
            exampleSudoku[2, 2] = 7;
            exampleSudoku[2, 4] = 6;
            exampleSudoku[2, 5] = 3;
            exampleSudoku[2, 7] = 9;
            exampleSudoku[3, 3] = 7;
            exampleSudoku[3, 5] = 8;
            exampleSudoku[3, 8] = 4;
            exampleSudoku[4, 1] = 8;
            exampleSudoku[4, 2] = 5;
            exampleSudoku[4, 4] = 4;
            exampleSudoku[4, 5] = 9;
            exampleSudoku[4, 6] = 6;
            exampleSudoku[4, 8] = 7;
            exampleSudoku[5, 1] = 7;
            exampleSudoku[5, 3] = 6;
            exampleSudoku[5, 4] = 5;
            exampleSudoku[5, 5] = 1;
            exampleSudoku[5, 6] = 2;
            exampleSudoku[5, 7] = 8;
            exampleSudoku[6, 1] = 9;
            exampleSudoku[6, 2] = 6;
            exampleSudoku[6, 3] = 3;
            exampleSudoku[6, 4] = 8;
            exampleSudoku[6, 5] = 2;
            exampleSudoku[6, 6] = 5;
            exampleSudoku[7, 0] = 7;
            exampleSudoku[7, 3] = 9;
            exampleSudoku[7, 4] = 1;
            exampleSudoku[8, 5] = 5;
            exampleSudoku[8, 8] = 3;
        }

        public void Play() {
            DifficultyMenuScreen difficultyMenu = new DifficultyMenuScreen();
            difficultyMenu.Show();
            difficulty = difficultyMenu.GetDifficulty();

            generateExample();
            GameBoardScreen board = new GameBoardScreen(exampleSudoku);
            board.Show();
        }
    }
}
