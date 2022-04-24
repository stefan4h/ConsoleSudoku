using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Entities {
    /// <summary>
    /// Represents a move the player does during a game of sudoku
    /// </summary>
    [Serializable]
    public class Move {
        public int X { get; set; } // row
        public int Y { get; set; } // col
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }
}