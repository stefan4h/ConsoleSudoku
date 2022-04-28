using System;
using System.Collections.Generic;

namespace ConsoleSudoku.Screens {
    /// <summary>
    /// Screen to choose the difficulty of the sudoku puzzle
    /// </summary>
    public class DifficultyMenuScreen : ASudokuScreen {
        private List<ESudokuDifficulty> difficulties;
        private int selectedIndex;

        public DifficultyMenuScreen() {
            selectedIndex = 0;
            difficulties = new List<ESudokuDifficulty> { ESudokuDifficulty.Easy, ESudokuDifficulty.Medium, ESudokuDifficulty.Hard };
        }

        /// <summary>
        /// Get the selected difficulty
        /// </summary>
        /// <returns>Returns the selected difficulty</returns>
        public ESudokuDifficulty GetDifficulty() {
            return difficulties[selectedIndex];
        }

        protected override void Draw() {
            ShowSmallerTitle("Choose your Difficulty");

            for (int i = 0; i < difficulties.Count; i++)
                CW(difficulties[i], i == selectedIndex ? selectColor : defaultColor);

            CW();
            CW();
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
                    if (selectedIndex < difficulties.Count - 1)
                        selectedIndex++;
                    else
                        skipRedraw = true;
                    break;
                case ConsoleKey.Enter:
                    exit = true;
                    break;
                default: skipRedraw = true; break;
            }
        }
    }
}
