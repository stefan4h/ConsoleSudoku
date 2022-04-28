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
            } catch { }

            _choices.Add("Start New Game");

            // load the score board from memory
            try {
                using (Stream fileStream = File.OpenRead("scoreboard.bin")) {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    Game.ScoreBoard = (List<FinishedGame>)deserializer.Deserialize(fileStream);

                    _choices.Add("Scoreboard");
                }
            } catch { }

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
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < _choices.Count - 1)
                        selectedIndex++;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    switch (_choices[selectedIndex]) {
                        case "Resume Game": ResumeGame(); break;
                        case "Start New Game": StartNewGame(); break;
                        case "Scoreboard": ShowScoreBoard(); break;
                        case "Exit": Environment.Exit(0); break;
                    }
                    exit = true;
                    break;
                default: skipRedraw = true; break;
            }
        }

        /// <summary>
        /// Resume the game that is currently played
        /// </summary>
        private void ResumeGame() {
            GameBoardScreen board = new GameBoardScreen();
            board.Show();

            if (board.Solved) BoardSolved();
        }

        /// <summary>
        /// Discard any savings of the old game and generate a new one
        /// </summary>
        private void StartNewGame() {
            Game.Reset();

            // choose a difficulty as the first step
            DifficultyMenuScreen difficultyMenu = new DifficultyMenuScreen();
            difficultyMenu.Show();
            Game.Difficulty = difficultyMenu.GetDifficulty();

            // Now generate a new sudoku puzzle for the given difficulty
            var generateCompleteSudokuAction = new GenerateCompleteSudokuAction();
            generateCompleteSudokuAction.Execute();

            // And show the board with the sudoku
            GameBoardScreen board = new GameBoardScreen();
            board.Show();

            if (board.Solved) BoardSolved();
        }

        /// <summary>
        /// Shows the Score Board Screen
        /// </summary>
        private void ShowScoreBoard() {
            ScoreBoardScreen scoreBoardScreen = new ScoreBoardScreen();
            scoreBoardScreen.Show();
        }

        /// <summary>
        /// Things to do when the user solves a sudoku completely
        /// </summary>
        private void BoardSolved() {
            FinishedGameScreen finishedGameScreen = new FinishedGameScreen();
            finishedGameScreen.Show();
            Game.Reset();
            _choices.Remove("Resume Game");
            UpdateShow();
        }
    }
}