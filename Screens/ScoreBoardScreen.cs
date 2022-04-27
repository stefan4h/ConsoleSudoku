using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class ScoreBoardScreen : ASudokuScreen {

        private int selectedIndex = 0;

        protected override void Draw() {
            ShowSmallerTitle("Score Boards");

            for (int i = 0; i < Game.ScoreBoard.Count; i++) {
                int spaces = Game.ScoreBoard[i].Score >= 100 ? 0 : 1;

                CW($"{GetStringAsRepeatedChar(' ', spaces)}{Game.ScoreBoard[i].Score} - {Game.ScoreBoard[i].Name}", i == selectedIndex ? randomColor : defaultColor);
            }
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
                    if (selectedIndex < Game.ScoreBoard.Count - 1)
                        selectedIndex++;
                    break;
                case ConsoleKey.Enter:

                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                default: skipRedraw = true; break;
            }
        }
    }
}
