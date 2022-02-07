using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public abstract class ASudokuScreen {

        protected const int LINE_LENGTH = 90;
        protected ConsoleColor randomColor;
        protected const ConsoleColor defaultColor = ConsoleColor.White;
        protected const ConsoleColor selectColor = ConsoleColor.Blue;
        protected bool exit = false;

        public ASudokuScreen() {
            randomColor = GetRandomConsoleColor();
        }

        private void ShowTitle() {
            W("   _____", randomColor); W("                           _          "); W("_____", randomColor); W("             _         _              "); W("|\n", randomColor);
            W("  / ____|", randomColor); W("                         | |        "); W("/ ____|", randomColor); W("           | |       | |             "); W("|\n", randomColor);
            W(" | |", randomColor); W("      ___   _ __   ___   ___  | |  ___  "); W("| (___", randomColor); W("   _   _   __| |  ___  | | __ _   _    "); W("|\n", randomColor);
            W(" | |", randomColor); W("     / _ \\ | '_ \\ / __| / _ \\ | | / _ \\  "); W("\\___ \\", randomColor); W(" | | | | / _` | / _ \\ | |/ /| | | |   "); W("|\n", randomColor);
            W(" | |____", randomColor); W("| (_) || | | |\\__ \\| (_) || ||  __/  "); W("____) |", randomColor); W("| |_| || (_| || (_) ||   < | |_| |   "); W("|\n", randomColor);
            W("  \\_____|", randomColor); W("\\___/ |_| |_||___/ \\___/ |_| \\___| "); W("|_____/", randomColor); W("  \\__,_| \\__,_| \\___/ |_|\\_\\ \\__,_|   "); W("|\n", randomColor);
            CW(GetStringAsRepeatedChar('_', LINE_LENGTH - 1) + "|", randomColor);
            CW();
            CW();
        }

        protected void CW(string line = "", ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;

            Console.WriteLine(line);

            Console.ResetColor();
        }

        protected void CW(object? value, ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        protected void W(string line = "", ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;
            Console.Write(line);
            Console.ResetColor();
        }

        protected string GetStringAsRepeatedChar(char c, int count) {
            StringBuilder sb = new StringBuilder();
            return sb.Append(c, count).ToString(); ;
        }

        protected ConsoleColor GetRandomConsoleColor() {
            Random rnd = new Random();

            ConsoleColor color = (ConsoleColor)rnd.Next(0, Enum.GetNames(typeof(ConsoleColor)).Length);

            while (color == ConsoleColor.Black)
                color = (ConsoleColor)rnd.Next(0, Enum.GetNames(typeof(ConsoleColor)).Length);

            return color;
        }

        protected void ShowSmallerTitle(string title) {
            CW(GetStringAsRepeatedChar('_', title.Length));
            CW(GetStringAsRepeatedChar(' ', title.Length) + "|");
            CW(title + "|");
            CW(GetStringAsRepeatedChar('_', title.Length) + "|");
            CW();
        }

        protected ConsoleKey ReadKey() {
            return Console.ReadKey(true).Key;
        }

        public void Show() {
            Console.CursorVisible = false;
            do {
                randomColor = GetRandomConsoleColor();
                Console.Clear();
                ShowTitle();
                Draw();
                HandleInput();
            } while (!exit);
        }

        protected abstract void Draw();
        protected abstract void HandleInput();
    }
}
