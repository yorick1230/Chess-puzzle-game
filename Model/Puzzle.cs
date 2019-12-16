using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    public class Puzzle
    {
        public string BlunderMove { get; set; }
        public string Elo { get; set; }
        public string FenBefore { get; set; }
        public List<string> ForcedLine { get; set; }
        public string Game_id { get; set; }
        public string Id { get; set; }
        public int Move_index { get; set; }
    }
}
