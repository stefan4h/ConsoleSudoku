using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    public abstract class ASudokuScreen {

        protected const int LINE_LENGTH = 90;
        protected const ConsoleColor defaultColor = ConsoleColor.White;
        protected const ConsoleColor selectColor = ConsoleColor.Blue;
        protected bool exit = false;

        private void ShowTitle() {
            CW("   _____                           _          _____             _         _              |");
            CW("  / ____|                         | |        / ____|           | |       | |             |");
            CW(" | |      ___   _ __   ___   ___  | |  ___  | (___   _   _   __| |  ___  | | __ _   _    |");
            CW(" | |     / _ \\ | '_ \\ / __| / _ \\ | | / _ \\  \\___ \\ | | | | / _` | / _ \\ | |/ /| | | |   |");
            CW(" | |____| (_) || | | |\\__ \\| (_) || ||  __/  ____) || |_| || (_| || (_) ||   < | |_| |   |");
            CW("  \\_____|\\___/ |_| |_||___/ \\___/ |_| \\___| |_____/  \\__,_| \\__,_| \\___/ |_|\\_\\ \\__,_|   |");
            CW(GetStringAsRepeatedChar('_', LINE_LENGTH - 1) + "|");
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
