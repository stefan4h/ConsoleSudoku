﻿using ConsoleSudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleSudoku.Screens {
    public class FinishedGameReplayScreen : ASudokuScreen {

        private FinishedGame _finishedGame;
        private Timer timer = new Timer(2000); // trigger timer event every 30 seconds
        private int _step = 0; // to save on which step the replay is currently on
        private int[,] _solution = new int[9, 9]; // store current solution for the replay
        private bool paused = false; // to pause the automatic replay

        public FinishedGameReplayScreen(FinishedGame finishedGame) {
            _finishedGame = finishedGame;
            timer.Elapsed += new ElapsedEventHandler(TimerEvent);
            timer.Enabled = true;
        }

        private void TimerEvent(object source, ElapsedEventArgs e) {
            if (_step >= _finishedGame.Moves.Count) {
                timer.Enabled = false;
                UpdateShow();
                return;
            }

            // add the next move to the replay solution and reload the grid
            _solution[_finishedGame.Moves[_step].X, _finishedGame.Moves[_step].Y] = _finishedGame.Moves[_step].NewValue;
            _step++;
            UpdateShow();
        }

        protected override void Draw() {
            ShowSmallerTitle($"Replay of the {_finishedGame.Difficulty} Sudoku solved by {_finishedGame.Name} with {_finishedGame.Score} Points");

            CW();

            W("Press "); W("ESC ", color: selectColor); W("to go Back to the Score Board\n");
            W("Press "); W("Enter ", color: selectColor); W("to Skip to the final Result\n");
            W("Press "); W("Space ", color: selectColor); W(timer.Enabled ? "to Pause Automatic Replay\n" : "to Continue Autmatic Replay\n");
            if (timer.Enabled) {
                CW();
                CW();
            } else {
                W("Press "); W("◄ Left Arrow ", color: selectColor); W(" to go to Previous Step\n");
                W("Press "); W("Right Arrow ► ", color: selectColor); W(" to go to Next Step\n");
            }

            // draw sudoku
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (i == 0) {
                        DrawBorder(i, j, "╔", "╦", "╤", "═══", "╗");
                    } else if (i % 3 == 0)
                        DrawBorder(i, j, "╠", "╪", "╬", "═══", "╣");
                    else
                        DrawBorder(i, j, "╟", "╫", "┼", "───", "╢");
                }
                DrawSudokuLine(i);
                if (i == 8)
                    for (int j = 0; j < 9; j++) {
                        DrawBorder(9, j, "╚", "╧", "╩", "═══", "╝");
                    }
            }
        }

        private void DrawBorder(int x, int y, string left, string middle, string normal, string line, string right) {
            if (y == 0) {
                W(GetStringAsRepeatedChar(' ', 26)); // to center the sudoku
                W(left);
            } else if (y % 3 == 0)
                W(middle);
            else
                W(normal);
            W(line);

            if (y == 8)
                W(right + "\n");
        }

        private void DrawSudokuLine(int line) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 9; j++) {
                if (j % 3 == 0) {
                    if (j == 0) W(GetStringAsRepeatedChar(' ', 26)); // to center the sudoku
                    W("║");
                } else
                    W("|");

                if (_finishedGame.Hints[line, j] != 0)
                    W($" {_finishedGame.Hints[line, j]} ");
                else if (_solution[line, j] != 0)
                    W($" {_solution[line, j]} ", selectColor);
                else
                    W("   ");
            }
            W("║");
            CW(sb);
        }

        protected override void HandleInput() {
            ConsoleKey key;

            key = ReadKey();
            switch (key) {
                case ConsoleKey.Escape:
                    exit = true;
                    timer.Enabled = false;
                    break;
                case ConsoleKey.Enter:
                    timer.Enabled = false;
                    _solution = _finishedGame.Solution;
                    _step = _finishedGame.Moves.Count;
                    break;
                case ConsoleKey.Spacebar:
                    timer.Enabled = !timer.Enabled; // toggle timer
                    break;
                case ConsoleKey.RightArrow:
                    if (_step < _finishedGame.Moves.Count && !timer.Enabled) {
                        // add the next move to the replay solution and reload the grid
                        _solution[_finishedGame.Moves[_step].X, _finishedGame.Moves[_step].Y] = _finishedGame.Moves[_step].NewValue;
                        _step++;
                        skipRedraw = false;
                    } else
                        skipRedraw = true;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_step > 0 && !timer.Enabled) {
                        // change to the prevous value of the grid
                        _step--;
                        _solution[_finishedGame.Moves[_step].X, _finishedGame.Moves[_step].Y] = _finishedGame.Moves[_step].OldValue;
                        skipRedraw = false;
                    } else
                        skipRedraw = true;
                    break;
                default: skipRedraw = true; break;
            }
        }
    }
}
