using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class GameBoard : ASudokuScreen {

        private int[,] sudoku;

        public GameBoard(int[,] sudoku) {
            this.sudoku = sudoku;
        }

        protected override void Draw() {
            CW("╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗");
            DrawSudokuLine(0);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(1);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(2);
            CW("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            DrawSudokuLine(3);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(4);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(5);
            CW("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            DrawSudokuLine(6);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(7);
            CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            DrawSudokuLine(8);
            CW("╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝");
        }

        private void DrawSudokuLine(int line) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++) {
                if (i % 3 == 0)
                    sb.Append("║");
                else
                    sb.Append("|");

                if (sudoku[line, i] != 0)
                    sb.Append($" {sudoku[line, i]} ");
                else
                    sb.Append("   ");
            }
            sb.Append("║");
            CW(sb);
        }

        protected override void HandleInput() {
            ReadKey();
        }
    }
}
