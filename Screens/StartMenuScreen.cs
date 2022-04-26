using ConsoleSudoku.Actions;
using ConsoleSudoku.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class StartMenuScreen : ASudokuScreen {

        private List<string> _choices;
        private int selectedIndex;

        public StartMenuScreen() : base() {
            selectedIndex = 0;
            _choices = new List<string> { };

            // if a game exists add it to the choice list
            try {
                using (Stream fileStream = File.OpenRead("current.bin")) {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    var currentGame = (CurrentGame)deserializer.Deserialize(fileStream);

                    Game.Difficulty = currentGame.Difficulty;
                    Game.Completed = currentGame.Completed;
                    Game.Hints = currentGame.Hints;
                    Game.Solution = currentGame.Solution;
                    Game.Progress = currentGame.Progress;
                    Game.Undo = currentGame.Undo;
                    Game.Redo = currentGame.Redo;
                    Game.AdditionalHintCount = currentGame.AdditionalHintCount;

                    _choices.Add("Resume Game");
                }
            } catch {

            }

            _choices.Add("Start New Game");
            _choices.Add("Exit");
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
                        case "Resume Game": ResumeGame(); break;
                        case "Start New Game": StartNewGame(); break;
                        case "Exit": Environment.Exit(0); break;
                    }
                    exit = true;
                    break;
                default: skipRedraw = true; break;
            }
        }

        private void ResumeGame() {
            GameBoardScreen board = new GameBoardScreen();
            board.Show();

            if (board.Solved) {
                FinishedGameScreen finishedGameScreen = new FinishedGameScreen();
                finishedGameScreen.Show();
                Game.Reset();
                _choices.Remove("Resume Game");
                UpdateShow();
            }
        }

        private void StartNewGame() {
            Game.Reset();

            DifficultyMenuScreen difficultyMenu = new DifficultyMenuScreen();
            difficultyMenu.Show();
            Game.Difficulty = difficultyMenu.GetDifficulty();

            var generateCompleteSudokuAction = new GenerateCompleteSudokuAction();
            generateCompleteSudokuAction.Execute();

            GameBoardScreen board = new GameBoardScreen();
            board.Show();

            if (board.Solved) {
                FinishedGameScreen finishedGameScreen = new FinishedGameScreen();
                finishedGameScreen.Show();
                Game.Reset();
                _choices.Remove("Resume Game");
                UpdateShow();
            }
        }
    }
}