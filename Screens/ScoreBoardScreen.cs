using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class ScoreBoardScreen : ASudokuScreen {

        private int selectedIndex = 0;

        protected override void Draw() {
            ShowSmallerTitle("Scoreboard");

            for (int i = 0; i < Game.ScoreBoard.Count; i++) {
                int spaces = Game.ScoreBoard[i].Score >= 100 ? 0 : 1;

                CW($"{i + 1}.   {GetStringAsRepeatedChar(' ', spaces)}{Game.ScoreBoard[i].Score} - {Game.ScoreBoard[i].Name}", i == selectedIndex ? selectColor : defaultColor);
            }

            CW();
            CW();
            W("Press "); W("Enter ", color: selectColor); W("to watch a Replay of the Selected Game\n");
            W("Press "); W("ESC ", color: selectColor); W("to go Back to the Start Menu");
        }

        protected override void HandleInput() {
            ConsoleKey key;

            key = ReadKey();
            switch (key) {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                        selectedIndex--;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < Game.ScoreBoard.Count - 1)
                        selectedIndex++;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    FinishedGameReplayScreen finishedGameReplayScreen = new FinishedGameReplayScreen(Game.ScoreBoard[selectedIndex]);
                    finishedGameReplayScreen.Show();
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                default: skipRedraw = true; break;
            }
        }
    }
}
