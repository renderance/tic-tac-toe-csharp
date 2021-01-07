using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class tile
    {
        /// <summary>
        /// Each tile has a row and column identifier and a claim, either undefined, X or O,
        /// representing whether it has been claimed by a player and which player that was.
        /// </summary>
        public int row { get; set; }
        public int col { get; set; }
        public state claim { get; set; } = state.U;

        public tile(int r, int c, state start_val)
        {
            this.row = r;
            this.col = c;
            this.claim = start_val;
        }
    }
}
