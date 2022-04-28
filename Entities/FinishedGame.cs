using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku.Entities {
    /// <summary>
    /// Represents already finished games to be saved in memory and later replayed
    /// </summary>
    [Serializable]
    public class FinishedGame {
        public string Name { get; set; }
        public int[,] Hints { get; set; }
        public int[,] Solution { get; set; }
        public int Score { get; set; }
        public List<Move> Moves { get; set; } = new List<Move>();
        public int AdditionalHintCount { get; set; }
        public ESudokuDifficulty Difficulty { get; set; }
    }
}
