using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Actions {
    public class GenerateCompleteSudokuAction : ISudokuAction {

        public void Execute() {
            Game.Hints = new int[9, 9]; // initialize grid
            Random rnd = new Random();
            List<int> digits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> rows = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> columns = new List<int>(rows);
            for (int i = 0; i < 9; i++) {
                int digitIndex = rnd.Next(0, digits.Count); // choose random digit out of list of possibilities
                int rowIndex = rnd.Next(0, rows.Count);
                int columnIndex = rnd.Next(0, columns.Count);

                Game.Hints[rows[rowIndex], columns[columnIndex]] = digits[digitIndex]; //insert random digit in random position
                digits.RemoveAt(digitIndex); // remove chosen digit from posibilities
                rows.RemoveAt(rowIndex);
                columns.RemoveAt(columnIndex);
            }
            var solveSudokuAction = new SolveSudokuAction(Game.Hints);
            solveSudokuAction.Execute();

            Game.Completed = (int[,])Game.Hints.Clone(); // create copy of completed sudoku

            // now dig holes into the sudoku

            bool[,] digable = new bool[9, 9]; // array to track digable fields

            // initialize digable array with true
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    digable[i, j] = true;

            // variables to store the minimum amount of hints needed
            int maxHints = 0;
            int colRowMinHints = 0;

            // initialize variables with the values for the specific difficulties
            switch (Game.Difficulty) {
                case ESudokuDifficulty.Easy:
                    maxHints = 47;
                    colRowMinHints = 5;
                    break;
                case ESudokuDifficulty.Medium:
                    maxHints = 35;
                    colRowMinHints = 4;
                    break;
                case ESudokuDifficulty.Hard:
                    maxHints = 31;
                    colRowMinHints = 3;
                    break;
            }

            // variables to store the amount of hints
            int hintsCount = 81;
            int[] rowHintCount = new int[9];
            int[] columnHintCount = new int[9];

            // initialize arrays with all 9 because the initial grid has a hint in every column
            Array.Fill(rowHintCount, 9);
            Array.Fill(columnHintCount, 9);

            (int, int) position = (0, 0); // current position looked at to dig a hole

            while (true) {

                // check if digging the cell does not violate any restrictions (min amount of hints in each column and row)
                if (rowHintCount[position.Item1] > colRowMinHints && columnHintCount[position.Item2] > colRowMinHints) {
                    // check wether the sudoku would still yield a unique solution when the position is dug
                    bool unique = true;
                    for (int i = 1; i <= 9; i++) {
                        if (i == Game.Hints[position.Item1, position.Item2]) continue;

                        int[,] sudokuCopy = (int[,])Game.Hints.Clone();
                        sudokuCopy[position.Item1, position.Item2] = i;
                        solveSudokuAction = new SolveSudokuAction(sudokuCopy);
                        solveSudokuAction.Execute();

                        if (solveSudokuAction.Solved) {
                            unique = false;
                            break;
                        }
                    }

                    if (unique) {
                        Game.Hints[position.Item1, position.Item2] = 0;
                        rowHintCount[position.Item1]--;
                        columnHintCount[position.Item2]--;
                    }
                }

                digable[position.Item1, position.Item2] = false;

                // if there are no more digable grids or the minimum amount of hints is reached the puzzle is finished
                if (!DigableAvailable(digable) || hintsCount == maxHints)
                    break;

                position = NextPosition(position, digable); // get next position of sequence
            }
        }

        /// <summary>
        /// Get next position for the sequence of digging holes (determined by difficulty)
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="digable"></param>
        /// <returns>Next position as tuple</returns>
        private (int, int) NextPosition((int, int) currentPosition, bool[,] digable) {
            int x = 0;
            int y = 0;
            Random rnd = new Random();
            while (true) {
                // difficulty: EASY | a random cell can be chosen to be the next selected
                //if (Game.Difficulty == ESudokuDifficulty.Easy) {
                    x = rnd.Next(0, 9);
                    y = rnd.Next(0, 9);
                //}
                // difficulty: MEDIUM/HARD | the sequence type "Jumping one cell" will be used
                //else {
                //    // for even rows
                //    if (currentPosition.Item1 % 2 == 0) {
                //        if (currentPosition.Item2 == 8 && currentPosition.Item1 != 8) {
                //            y = 7;
                //            x = currentPosition.Item1 + 1;
                //        } else if (currentPosition.Item2 == 7 && currentPosition.Item1 != 8) {
                //            y = 8;
                //            x = currentPosition.Item1 + 1;
                //        } else if ((currentPosition.Item2 == 7 || currentPosition.Item2 == 8) && currentPosition.Item1 == 8) {
                //            if (currentPosition.Item2 == 7)
                //                y = 0;
                //            else
                //                y = 1;
                //            x = 0;
                //        } else
                //            y = currentPosition.Item2 + 2;
                //    }
                //    // for odd rows
                //    else {
                //        if (currentPosition.Item2 == 0) {
                //            y = 1;
                //            x = currentPosition.Item1 + 1;
                //        } else if (currentPosition.Item2 == 1) {
                //            y = 0;
                //            x = currentPosition.Item1 + 1;
                //        } else
                //            y = currentPosition.Item2 - 2;
                //    }
                //}

                // check if the next position is digable, if not try the next position
                if (digable[x, y])
                    return (x, y);
                //else
                //    currentPosition = (x, y);
            }
        }

        /// <summary>
        /// Checks if there are still digable cells available
        /// </summary>
        /// <param name="digable"></param>
        /// <returns>True or False</returns>
        private bool DigableAvailable(bool[,] digable) {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (digable[i, j])
                        return true;
            return false;
        }
    }
}
