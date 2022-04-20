using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Actions {
    public class SolveSudokuAction : ISudokuAction {

        private int[,] _sudoku = new int[9, 9];

        public bool Solved { get; private set; }

        public SolveSudokuAction(int[,] sudoku) {
            _sudoku = sudoku;
        }

        public void Execute() {
            Solved = Solve();
        }

        /// <summary>
        /// Solves the soduku grid with the random generated numbers using backtracking
        /// </summary>
        /// <returns>True if the sudoku could be solved and false if it cannot be solved</returns>
        private bool Solve() {

            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (_sudoku[i, j] == 0) {
                        for (int d = 1; d <= 9; d++) {

                            if (IsValid(d, i, j)) {
                                _sudoku[i, j] = d;
                                if (Solve())
                                    return SudokuIsValid();
                                else
                                    _sudoku[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }

            return SudokuIsValid();
        }

        private bool SudokuIsValid() {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (!IsValid(_sudoku[i, j], i, j))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the given value is would be valid at a position in the grid
        /// </summary>
        /// <param name="value">Value to be inserted in the grid</param>
        /// <param name="x">x coordinate of the grid</param>
        /// <param name="y">y coordinate of the grid</param>
        /// <returns>True if the value would be valid or False if the value would not be valid</returns>
        private bool IsValid(int value, int x, int y) {
            // check if column is valid
            for (int i = 0; i < 9; i++) {
                if (i == x) continue;
                if (_sudoku[i, y] == value)
                    return false;
            }

            // check if row is valid
            for (int i = 0; i < 9; i++) {
                if (i == y) continue;
                if (_sudoku[x, i] == value)
                    return false;
            }

            for (int i = 3 * (x / 3); i < (3 * (x / 3)) + 3; i++) {
                for (int j = 3 * (y / 3); j < (3 * (y / 3)) + 3; j++) {
                    if (i == x && j == y) continue;
                    if (_sudoku[i, j] == value)
                        return false;
                }
            }

            return true;
        }
    }
}
