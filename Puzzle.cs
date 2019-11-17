using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    class Puzzle
    {
        public String fen { get; set; }
        public bool isPlayerWhite { get; set; }
        public String[] correctMoves { get; set; }

        public Puzzle(string fen, bool isPlayerWhite, string[] correctMoves)
        {
            this.fen = fen;
            this.isPlayerWhite = isPlayerWhite;
            this.correctMoves = correctMoves;
        }
    }
}
