using ConsoleSudoku.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class FinishedGameScreen : ASudokuScreen {

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
                W($"You finished the Sudoku with a Score of "); W(Game.GetScore().ToString() + "\n", selectColor);
                W("State your Name for the Score Board: ");
                string name = Console.ReadLine();

                FinishedGame finishedGame = new FinishedGame {
                    Score = Game.GetScore(),
                    AdditionalHintCount = Game.AdditionalHintCount,
                    Difficulty = Game.Difficulty,
                    Hints = Game.Hints,
                    Solution = Game.Solution,
                    Name = name
                };

                // add all moved to finished game
                while (Game.Undo.Count > 0)
                    finishedGame.Moves.Insert(0, Game.Undo.Pop());

                Game.ScoreBoard.Add(finishedGame);

                List<FinishedGame> scoreBoard = new List<FinishedGame>(Game.ScoreBoard);
                scoreBoard.Add(finishedGame);
                Game.ScoreBoard = scoreBoard;

                // save updated scoreboard
                using (Stream stream = File.Open("scoreboard.bin", FileMode.Create)) {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, scoreBoard);
                }
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
