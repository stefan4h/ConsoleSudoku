using System;
using System.Text;

namespace ConsoleSudoku.Screens {
    public class GameBoard : ASudokuScreen {

        private int[,] sudoku;
        private (int, int) selected = (-1, -1);

        public GameBoard(int[,] sudoku) {
            this.sudoku = sudoku;
        }

        protected override void Draw() {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    // if no square is selected, select first free square
                    if (selected.Item1 == -1 && selected.Item2 == -1 && sudoku[i, j] == 0)
                        selected = (i, j);

                    if (i == 0)
                        DrawBorder(i, j, "╔", "╦", "╤", "═══", "╗");
                    else if (i % 3 == 0)
                        DrawBorder(i, j, "╠", "╪", "╬", "═══", "╣");
                    else
                        DrawBorder(i, j, "╟", "╫", "┼", "───", "╢");
                }
                DrawSudokuLine(i);
                if (i == 8)
                    for (int j = 0; j < 9; j++) {
                        DrawBorder(i, j, "╚", "╧", "╩", "═══", "╝");
                    }
            }

            //CW("╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗");
            //DrawSudokuLine(0);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(1);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(2);
            //CW("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            //DrawSudokuLine(3);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(4);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(5);
            //CW("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
            //DrawSudokuLine(6);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(7);
            //CW("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
            //DrawSudokuLine(8);
            //CW("╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝");
        }

        private void DrawBorder(int x, int y, string left, string middle, string normal, string line, string right) {
            if (y == 0)
                W(left, selected.Item2 == 0 && (selected.Item1 == x || selected.Item1 + 1 == x));
            else if (y % 3 == 0)
                W(middle, (selected.Item2 == y || selected.Item2 + 1 == y) && (selected.Item1 == x || selected.Item1 + 1 == x));
            else
                W(normal, (selected.Item2 == y || selected.Item2 + 1 == y) && (selected.Item1 == x || selected.Item1 + 1 == x));

            W(line, selected.Item2 == y && (selected.Item1 == x || selected.Item1 + 1 == x));

            if (y == 8)
                W(right + "\n",selected.Item2 == 8 && (selected.Item1 == x || selected.Item1 + 1 == x));
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
