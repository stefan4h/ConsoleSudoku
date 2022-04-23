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

        public GameMenuScreen() {
            selectedIndex = 0;
            _choices = new List<string> { "Get a Hint" };

            if (Game.Undo.Count != 0) _choices.Add("Undo Last Move");
            if (Game.Redo.Count != 0) _choices.Add("Redo Last Move");
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
                        case "Get a Hint": GetHint(); break;
                        case "Undo Last Move": Undo(); break;
                        case "Redo Last Move": Redo(); break;
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
