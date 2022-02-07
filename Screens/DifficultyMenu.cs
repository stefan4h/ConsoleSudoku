using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public class DifficultyMenu : ASudokuScreen {
        private List<ESudokuDifficulty> difficulties;
        private int selectedIndex;

        public DifficultyMenu() {
            selectedIndex = 0;
            difficulties = new List<ESudokuDifficulty> { ESudokuDifficulty.Easy, ESudokuDifficulty.Medium, ESudokuDifficulty.Hard };
        }

        public ESudokuDifficulty GetDifficulty() {
            return difficulties[selectedIndex];
        }

        protected override void Draw() {
            ShowSmallerTitle("Choose your Difficulty");

            for (int i = 0; i < difficulties.Count; i++)
                CW(difficulties[i], i == selectedIndex ? selectColor : defaultColor);
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
                    if (selectedIndex < difficulties.Count - 1)
                        selectedIndex++;
                    break;
                case ConsoleKey.Enter:
                    exit = true;
                    break;
            }
        }
    }
}
