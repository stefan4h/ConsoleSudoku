using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Actions {
    public class VerifySudokuSolvedAction : ISudokuAction {

        private bool solved;
        private int[,] sudoku;
        private int[,] solution;
        public VerifySudokuSolvedAction(int[,] sudoku, int[,] solution) {
            this.solved = false;
            this.solution = sudoku;
            this.solution = solution;
        }

        public bool GetSolved() => solved;

        public void Execute() {
            // inital check if sudoku is complete to avoid unnecessary checks
            if (!NoEmptyField())
                return;

        }

        private bool NoEmptyField() {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++) {
                    if (sudoku[i, j] == 0 || solution[i, j] == 0)
                        return false;
                }

            return true;
        }
    }
}
