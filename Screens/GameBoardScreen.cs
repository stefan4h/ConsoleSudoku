using ConsoleSudoku.Actions;
using System;
using System.Text;
using System.Timers;

namespace ConsoleSudoku.Screens {
    public class GameBoardScreen : ASudokuScreen {

        private (int, int) selected = (4, 4);
        private Timer timer = new Timer(30000); // trigger timer event every 30 seconds
        private int progress = 0; // to messure the time progress
        private readonly int maxProgress = 84; // set max time to 7 minutes

        protected override void Draw() {
            // start timer
            if (!timer.Enabled) {
                timer.Elapsed += new ElapsedEventHandler(TimerEvent);
                timer.Enabled = true;
            }

            // draw sudoku
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (i == 0) {
                        DrawBorder(i, j, "╔", "╦", "╤", "═══", "╗");
                    } else if (i % 3 == 0)
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
            
            CW();

            // draw progress bar
            W("   ");
            W(GetStringAsRepeatedChar('█', progress), color: randomColor);
            W(GetStringAsRepeatedChar('░', maxProgress - progress) + "   ");
        }

        private void TimerEvent(object source, ElapsedEventArgs e) {
            if (progress >= maxProgress) return;

            progress += 6;
            UpdateShow();
        }

        private void DrawBorder(int x, int y, string left, string middle, string normal, string line, string right) {
            if (y == 0) {
                W(GetStringAsRepeatedChar(' ', 26)); // to center the sudoku
                W(left,
                        selected.Item2 == 0 &&
                        (selected.Item1 == x || selected.Item1 + 1 == x) ?
                        selectColor :
                        defaultColor
                        );
            } else if (y % 3 == 0)
                W(middle,
                    (selected.Item2 == y || selected.Item2 + 1 == y) &&
                    (selected.Item1 == x || selected.Item1 + 1 == x) ?
                    selectColor :
                    defaultColor
                    );
            else
                W(normal,
                    (selected.Item2 == y || selected.Item2 + 1 == y) &&
                    (selected.Item1 == x || selected.Item1 + 1 == x) ?
                    selectColor :
                    defaultColor
                    );

            W(line,
                selected.Item2 == y &&
                (selected.Item1 == x || selected.Item1 + 1 == x) ?
                selectColor :
                defaultColor
                );

            if (y == 8)
                W(right + "\n",
                    selected.Item2 == 8 &&
                    (selected.Item1 == x || selected.Item1 + 1 == x) ?
                    selectColor :
                    defaultColor
                    );
        }

        private void DrawSudokuLine(int line) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 9; j++) {
                if (j % 3 == 0) {
                    if (j == 0) W(GetStringAsRepeatedChar(' ', 26)); // to center the sudoku
                    W("║", (selected.Item2 == j || selected.Item2 + 1 == j) && selected.Item1 == line ? selectColor : defaultColor);
                } else
                    W("|", (selected.Item2 == j || selected.Item2 + 1 == j) && selected.Item1 == line ? selectColor : defaultColor);

                if (Game.Hints[line, j] != 0)
                    W($" {Game.Hints[line, j]} ");
                else if (Game.Solution[line, j] != 0)
                    W($" {Game.Solution[line, j]} ", selectColor);
                else
                    W("   ");
            }
            W("║", selected.Item2 == 8 && selected.Item1 == line ? selectColor : defaultColor);
            CW(sb);
        }

        protected override void HandleInput() {
            ConsoleKeyInfo key;

            key = ReadKeyInfo();

            // add value to solution if selected field is not part of the sudoku
            if (KeyIsNumeric(key.Key) && Game.Hints[selected.Item1, selected.Item2] == 0) {
                Game.Solution[selected.Item1, selected.Item2] = int.Parse(key.KeyChar.ToString());
                return;
            }

            // remove value from solution if delete or backspace is pressed
            if ((key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Delete) && Game.Hints[selected.Item1, selected.Item2] == 0) {
                Game.Solution[selected.Item1, selected.Item2] = 0;
                return;
            }

            // arrow navigation keys
            switch (key.Key) {
                case ConsoleKey.UpArrow:
                    if (selected.Item1 > 0)
                        selected.Item1--;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.DownArrow:
                    if (selected.Item1 < 8)
                        selected.Item1++;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selected.Item2 > 0)
                        selected.Item2--;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.RightArrow:
                    if (selected.Item2 < 8)
                        selected.Item2++;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    exit = true;
                    break;
            }
        }

        protected override void ExecuteActions() {
            base.ExecuteActions();
            VerifySudokuSolvedAction verifySudokuSolvedAction = new VerifySudokuSolvedAction(Game.Hints, Game.Solution);
        }

        private bool KeyIsNumeric(ConsoleKey key) {
            return (key > ConsoleKey.D0 && key <= ConsoleKey.D9)
                || (key > ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9);
        }
    }
}
