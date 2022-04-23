using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Actions {
    /// <summary>
    /// Checks if the sudoku has been solved
    /// </summary>
    public class VerifySudokuSolvedAction : ISudokuAction {

        public bool Solved { get; private set; } = false;

        public void Execute() {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    // first check if the value is set
                    // if it is set check if the solution matched the completed sudoku
                    if ((Game.Hints[i, j] == 0 && Game.Solution[i, j] == 0) ||
                        (Game.Solution[i, j] != 0 && Game.Completed[i, j] != Game.Solution[i, j])) {
                        Solved = false;
                        return;
                    }
                }
            }
            Solved = true;
        }
    }
}
