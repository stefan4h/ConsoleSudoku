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
                        DrawBorder(9, j, "╚", "╧", "╩", "═══", "╝");
                    }
            }
        }

        private void DrawBorder(int x, int y, string left, string middle, string normal, string line, string right) {
            if (y == 0)
                W(left,
                    selected.Item2 == 0 && (selected.Item1 == x || selected.Item1 + 1 == x) ? selectColor : defaultColor);
            else if (y % 3 == 0)
                W(middle, (selected.Item2 == y || selected.Item2 + 1 == y) && (selected.Item1 == x || selected.Item1 + 1 == x) ? selectColor : defaultColor);
            else
                W(normal, (selected.Item2 == y || selected.Item2 + 1 == y) && (selected.Item1 == x || selected.Item1 + 1 == x) ? selectColor : defaultColor);

            W(line, selected.Item2 == y && (selected.Item1 == x || selected.Item1 + 1 == x) ? selectColor : defaultColor);

            if (y == 8)
                W(right + "\n", selected.Item2 == 8 && (selected.Item1 == x || selected.Item1 + 1 == x) ? selectColor : defaultColor);
        }

        private void DrawSudokuLine(int line) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 9; j++) {
                if (j % 3 == 0)
                    W("║", (selected.Item2 == j || selected.Item2 + 1 == j) && selected.Item1 == line ? selectColor : defaultColor);
                else
                    W("|", (selected.Item2 == j || selected.Item2 + 1 == j) && selected.Item1 == line ? selectColor : defaultColor);

                if (sudoku[line, j] != 0)
                    W($" {sudoku[line, j]} ");
                else
                    W("   ");
            }
            W("║", selected.Item2 == 8 && selected.Item1 == line ? selectColor : defaultColor);
            CW(sb);
        }

        protected override void HandleInput() {
            ConsoleKey key;

            key = ReadKey();

            if (KeyIsNumeric(key)) {

                return;
            }

            switch (key) {
                case ConsoleKey.UpArrow:
                    if (selected.Item1 > 0)
                        selected.Item1--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selected.Item1 < 8)
                        selected.Item1++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selected.Item2 > 0)
                        selected.Item2--;
                    break;
                case ConsoleKey.RightArrow:
                    if (selected.Item2 < 8)
                        selected.Item2++;
                    break;
                case ConsoleKey.Enter:
                    exit = true;
                    break;
            }
        }

        private bool KeyIsNumeric(ConsoleKey key) {
            return (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
                || (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9);
        }
    }
}
