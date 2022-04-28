using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using ConsoleSudoku.Entities;

namespace ConsoleSudoku.Actions {
    /// <summary>
    /// Action to save the current sudoku in memory
    /// </summary>
    public class SaveCurrentSudokuAction : ISudokuAction {
        public void Execute() {
            // construct the object
            var game = new CurrentGame {
                Completed = Game.Completed,
                Hints = Game.Hints,
                Solution = Game.Solution,
                Progress = Game.Progress,
                Undo = Game.Undo,
                Redo = Game.Redo,
                AdditionalHintCount = Game.AdditionalHintCount,
                Difficulty = Game.Difficulty
            };

            // save the object as binary file in memory
            using (Stream stream = File.Open("current.bin", FileMode.Create)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, game);
            }
        }
    }
}
