using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Actions {
    public class GenerateCompleteSudokuAction : ISudokuAction {

        private int[,] sudoku = new int[9, 9];

        public int[,] GetSudoku() {
            return sudoku;
        }

        public void Execute() {
            Random rnd = new Random();
            List<int> digits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> rows = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> columns = new List<int>(rows);
            for (int i = 0; i < 9; i++) {
                int digitIndex = rnd.Next(0, digits.Count); // choose random digit out of list of possibilities
                int rowIndex = rnd.Next(0, rows.Count);
                int columnIndex = rnd.Next(0, columns.Count);

                sudoku[rows[rowIndex], columns[columnIndex]] = digits[digitIndex]; //insert random digit in random position
                digits.RemoveAt(digitIndex); // remove chosen digit from posibilities
                rows.RemoveAt(rowIndex);
                columns.RemoveAt(columnIndex);
            }
            //Solve();
        }

        private bool Solve() {

            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (sudoku[i, j] == 0) {
                        for (int d = 1; d <= 9; d++) {

                            if (IsValid(d, i, j)) {
                                sudoku[i, j] = d;
                                if (Solve())
                                    return true;
                                else
                                    sudoku[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsValid(int value, int x, int y) {
            // check if column is valid
            for (int i = 0; i < 9; i++) {
                if (i == x) continue;
                if (sudoku[i, y] == value)
                    return false;
            }

            // check if row is valid
            for (int i = 0; i < 9; i++) {
                if (i == y) continue;
                if (sudoku[x, i] == value)
                    return false;
            }

            for (int i = 3 * (x / 3); i < (3 * (x / 3)) + 3; i++) {
                for (int j = 3 * (y / 3); j < (3 * (y / 3)) + 3; j++) {
                    if (i == x && j == y) continue;
                    if (sudoku[i, j] == value)
                        return false;
                }
            }

            return true;
        }
    }
}
