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


        }
    }
}
