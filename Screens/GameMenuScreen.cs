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
            _choices = new List<string> { "Get a Hint", "Undo" };
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
                    exit = true;
                    break;
                case ConsoleKey.Escape:
                    GameBoardScreen gameBoardScreen = new GameBoardScreen();
                    gameBoardScreen.Show();
                    break;
            }
        }
    }
}
