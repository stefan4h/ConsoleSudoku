using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Entities {
    [Serializable]
    public class CurrentGame {
        public int[,] Completed { get; set; }
        public int[,] Hints { get; set; }
        public int[,] Solution { get; set; }
        public int Progress { get; set; }
        public Stack<Move> Undo { get; set; }
        public Stack<Move> Redo { get; set; }
        public int AdditionalHintCount { get; set; }
        public ESudokuDifficulty Difficulty { get; set; }
    }
}
