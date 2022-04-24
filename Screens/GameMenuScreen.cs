using ConsoleSudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class GameMenuScreen : ASudokuScreen {

        private List<string> _choices;
        private int selectedIndex;
        public bool Exit { get; set; } = false;

        public GameMenuScreen() {
            selectedIndex = 0;
            _choices = new List<string> { "Get an Additional Hint" };

            if (Game.Undo.Count != 0) _choices.Add("Undo Last Move");
            if (Game.Redo.Count != 0) _choices.Add("Redo Last Move");

            _choices.Add("Go Back to Start Menu");
        }

        protected override void Draw() {
            ShowSmallerTitle("Game Menu");

            for (int i = 0; i < _choices.Count; i++)
                CW(_choices[i], i == selectedIndex ? randomColor : defaultColor);
        }

        protected override void HandleInput() {
            ConsoleKey key;

            key = ReadKey();
            switch (key) {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                        selectedIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < _choices.Count - 1)
                        selectedIndex++;
                    break;
                case ConsoleKey.Enter:
                    switch (_choices[selectedIndex]) {
                        case "Get an Additional Hint": GetHint(); break;
                        case "Undo Last Move": Undo(); break;
                        case "Redo Last Move": Redo(); break;
                        case "Go Back to Start Menu": Exit = true; break;
                    }
                    exit = true;
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
            }
        }

        /// <summary>
        /// Give the player another hint
        /// </summary>
        private void GetHint() {
            Random rnd = new Random();
            int startX = rnd.Next(0, 8);
            int startY = rnd.Next(0, 8);


            // start at a random position in the grid and get first blank cell as tip
            for (int i = startX; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (i == startX && j < startY) continue;
                    if (Game.Solution[i, j] == 0 && Game.Hints[i, j] == 0) {
                        Game.AdditionalHintCount++;
                        Game.Hints[i, j] = Game.Completed[i, j];
                        Game.Redo.Clear(); // clear redo stack after getting a hint
                        return;
                    }
                }
            }

            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (i == startX && j > startY) break;
                    if (Game.Solution[i, j] == 0 && Game.Hints[i, j] == 0) {
                        Game.AdditionalHintCount++;
                        Game.Hints[i, j] = Game.Completed[i, j];
                        Game.Redo.Clear(); // clear redo stack after getting a hint
                        return;
                    }
                }
                if (i == startX) break;
            }
        }

        /// <summary>
        /// Undo the last move the player made
        /// </summary>
        private void Undo() {
            Move move = Game.Undo.Pop();
            Game.Redo.Push(move);
            Game.Solution[move.X, move.Y] = move.OldValue;
        }

        /// <summary>
        /// Redo a move that has previously been undone
        /// </summary>
        private void Redo() {
            Move move = Game.Redo.Pop();
            Game.Undo.Push(move);
            Game.Solution[move.X, move.Y] = move.NewValue;
        }
    }
}
