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
    public class SaveCurrentSudokuAction : ISudokuAction {
        public void Execute() {
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


            using (Stream stream = File.Open("current.bin", FileMode.Create)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, game);
            }
        }
    }
}
