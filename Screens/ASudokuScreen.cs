using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Screens {
    /// <summary>
    /// Base class to encapsulate functionality and behaviour of every screen
    /// </summary>
    public abstract class ASudokuScreen {

        protected const int LINE_LENGTH = 90;
        protected ConsoleColor randomColor;
        protected const ConsoleColor defaultColor = ConsoleColor.White;
        // protected const ConsoleColor selectColor = ConsoleColor.Blue;
        protected bool exit = false; // if set to true the screen will exit on next iteration
        protected bool skipRedraw = false; // if set to true the screen will not be redrawn on next iteration

        public ASudokuScreen() {
            randomColor = GetRandomConsoleColor();
        }

        /// <summary>
        /// Shows the title of the game on top of every screen
        /// </summary>
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

        /// <summary>
        /// Prints a line with a given color
        /// </summary>
        /// <param name="line">Line to be printed</param>
        /// <param name="color">Color used to print the text</param>
        protected void CW(string line = "", ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;

            Console.WriteLine(line);

            Console.ResetColor();
        }

        /// <summary>
        /// Prints a line with a given color
        /// </summary>
        /// <param name="line">Line to be printed</param>
        /// <param name="color">Color used to print the text</param>
        protected void CW(object? value, ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints Text with a given color
        /// </summary>
        /// <param name="line">Text to be printed</param>
        /// <param name="color">Color used to print the text</param>
        protected void W(string line = "", ConsoleColor color = defaultColor) {
            Console.ForegroundColor = color;
            Console.Write(line);
            Console.ResetColor();
        }

        /// <summary>
        /// Helper method receive a string of a repated char
        /// </summary>
        /// <param name="c">char that should be repeated</param>
        /// <param name="count">Number of times the char should be repeated</param>
        /// <returns></returns>
        protected string GetStringAsRepeatedChar(char c, int count) {
            StringBuilder sb = new StringBuilder();
            return sb.Append(c, count).ToString(); ;
        }

        /// <summary>
        /// Get a random ConsoleColor
        /// </summary>
        /// <returns>Random ConsoleColor</returns>
        protected ConsoleColor GetRandomConsoleColor() {
            Random rnd = new Random();

            ConsoleColor color = (ConsoleColor)rnd.Next(0, Enum.GetNames(typeof(ConsoleColor)).Length);

            while (color == ConsoleColor.Black || 
                color == ConsoleColor.White ||
                color == ConsoleColor.DarkGray || 
                color == ConsoleColor.Gray)
                color = (ConsoleColor)rnd.Next(0, Enum.GetNames(typeof(ConsoleColor)).Length);

            return color;
        }

        /// <summary>
        /// Prints a smaller title i.e. a menu title
        /// </summary>
        /// <param name="title">Text for the title</param>
        protected void ShowSmallerTitle(string title) {
            CW(GetStringAsRepeatedChar('_', title.Length));
            CW(GetStringAsRepeatedChar(' ', title.Length) + "|");
            CW(title + "|");
            CW(GetStringAsRepeatedChar('_', title.Length) + "|");
            CW();
        }

        /// <summary>
        /// Reads the key the user inputs
        /// </summary>
        /// <returns>The key value</returns>
        protected ConsoleKey ReadKey() {
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Get the ConsoleKeyInfo of the input key
        /// </summary>
        /// <returns>The ConsoleKeyInfo of the input key</returns>
        protected ConsoleKeyInfo ReadKeyInfo() {
            return Console.ReadKey(true);
        }

        /// <summary>
        /// Show Screen
        /// </summary>
        public void Show() {
            Console.CursorVisible = false;
            do {
                UpdateShow();
            } while (!exit);
        }

        /// <summary>
        /// Update the screen
        /// </summary>
        protected void UpdateShow() {
            randomColor = GetRandomConsoleColor();
            if (!skipRedraw) {
                Console.Clear();
                ShowTitle();
                Draw();
            }
            skipRedraw = false;
            HandleInput();
            ExecuteActions();
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        protected abstract void Draw();

        /// <summary>
        /// For handeling the user input
        /// </summary>
        protected abstract void HandleInput();

        /// <summary>
        /// Actions to be executed after each iteration
        /// </summary>
        protected virtual void ExecuteActions() {

        }
    }
}
