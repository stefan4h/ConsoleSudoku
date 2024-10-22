﻿using ConsoleSudoku.Actions;
using ConsoleSudoku.Entities;
using ConsoleSudoku.Screens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku {
    /// <summary>
    /// Defines the difficulty of the sudoku
    /// </summary>
    public enum ESudokuDifficulty {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Represents the current game played
    /// </summary>
    public static class Game {
        public static ESudokuDifficulty Difficulty { get; set; }
        public static int[,] Completed { get; set; } // completed sudoku to compare in the end
        public static int[,] Hints { get; set; } // to store all the hints of the sudoku
        public static int[,] Solution { get; set; } = new int[9, 9]; // to store the user input for the sudoku solution
        public static int Progress { get; set; } = 0; // to messure the time progress
        public static Stack<Move> Undo { get; set; } = new Stack<Move>(); // undo stack
        public static Stack<Move> Redo { get; set; } = new Stack<Move>(); // redo stack
        public static int AdditionalHintCount { get; set; } = 0;
        private static List<FinishedGame> _scoreBoard;

        public static List<FinishedGame> ScoreBoard {
            get { return _scoreBoard.OrderByDescending(fg => fg.Score).ToList(); }
            set { _scoreBoard = value; }
        }


        /// <summary>
        /// Starts the Game
        /// </summary>
        public static void Play() {
            while (true) {
                StartMenuScreen startMenuScreen = new StartMenuScreen();
                startMenuScreen.Show();
            }
        }

        /// <summary>
        /// Resets the Game
        /// </summary>
        public static void Reset() {
            Completed = null;
            Hints = null;
            Solution = new int[9, 9];
            Progress = 0;
            Undo = new Stack<Move>();
            Redo = new Stack<Move>();
            AdditionalHintCount = 0;
            File.Delete("current.bin");
        }

        /// <summary>
        /// Returns the number of how many empty cells are in the grid
        /// </summary>
        /// <returns>Number of empty cells (Holes) in the grid</returns>
        public static int HolesToFill() {
            int count = 0;
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (Hints[i, j] == 0 && Solution[i, j] == 0) count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Calculates the score of a game
        /// </summary>
        /// <returns>The score of the sudoku game</returns>
        public static int GetScore() {
            if (Progress >= 84) return 0; // the score is 0 when the progress (the time) progressed too far
            return Math.Max(0, (84 - Progress - (AdditionalHintCount * 5)) * 10);
        }
    }
}
