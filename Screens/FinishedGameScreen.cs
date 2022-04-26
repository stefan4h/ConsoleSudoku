using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class FinishedGameScreen : ASudokuScreen {

        public FinishedGameScreen() : base() {

        }

        protected override void Draw() {
            CW();
            CW();
            CW();
            CW(GetStringAsRepeatedChar(' ', 5) + " _     _  _______  ___      ___        ______   _______  __    _  _______  __ ");
            CW(GetStringAsRepeatedChar(' ', 5) + "| | _ | ||       ||   |    |   |      |      | |       ||  |  | ||       ||  |");
            CW(GetStringAsRepeatedChar(' ', 5) + "| || || ||    ___||   |    |   |      |  _    ||   _   ||   |_| ||    ___||  |");
            CW(GetStringAsRepeatedChar(' ', 5) + "|       ||   |___ |   |    |   |      | | |   ||  | |  ||       ||   |___ |  |");
            CW(GetStringAsRepeatedChar(' ', 5) + "|       ||    ___||   |___ |   |___   | |_|   ||  |_|  ||  _    ||    ___||__|");
            CW(GetStringAsRepeatedChar(' ', 5) + "|   _   ||   |___ |       ||       |  |       ||       || | |   ||   |___  __ ");
            CW(GetStringAsRepeatedChar(' ', 5) + "|__| |__||_______||_______||_______|  |______| |_______||_|  |__||_______||__|");
            CW();
            CW();
            CW();
            if (Game.GetScore() > 0) {
                W($"You finished the Sudoku with a Score of "); W(Game.GetScore().ToString() + "\n", randomColor);
                W("State your Name for the Score Board: ");
                string name = Console.ReadLine();
            } else {
                CW("You solved the Sudoku but it took you too long!");
                CW("Try to finish it faster next time to get a rank on the score board");
            }


            CW();
            CW();
        }

        protected override void HandleInput() {
            if (Game.GetScore() > 0) {
                exit = true;
                return;
            }
            exit = true;
            ReadKeyInfo();
        }
    }
}
