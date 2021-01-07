using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    public class board
    {
        /// <summary>
        /// The 'board' class provides the framework for each individual game.
        /// It has rows and columns, referring to how many tiles are in play, 
        /// and tracks what turn it is, which player's turn it is. Each instance
        /// of a board starts at turn 1, with player 1 allowed to make a move.
        /// </summary>

        private tile[,] tileboard;
        private int turn { get; set; } = 1;
        public int player { get; set; } = 1;
        public int rows { get; set; } = 3;
        public int cols { get; set; } = 3;

        public board(int rows, int cols)
        {
            /// <summary>
            /// Upon initializing a board, the size of the board needs to be provided.
            /// Each row and column combination will refer to one tile, which is set
            /// to the 'undecided' state [state.U].
            /// </summary>
            tileboard = new tile[rows, cols];
            for (int ii = 0; ii < rows; ii++)
            {
                for (int jj = 0; jj < cols; jj++)
                {
                    tileboard[ii, jj] = new tile(ii, jj, state.U);
                }
            }
            this.rows = rows;
            this.cols = cols;
        }

        public void print_board()
        {
            /// <summary>
            /// This parameterless and outputless method simply prints strings that represent
            /// the current board's state.
            /// </summary>
            for (int ii = 0; ii < this.rows; ii++)
            {
                string separator = "";
                string str_to_print = "";
                for (int jj = 0; jj < this.cols; jj++)
                {
                    string tile_symbol = (this.tileboard[ii, jj].claim == state.U) ? " " :
                        (this.tileboard[ii, jj].claim == state.O) ? "O" : "X";
                    // For every column in this row, except the last, add the state and a separator to a string
                    // and add a horizontal separator sequence with a cross-node to the separator.
                    if (jj < this.cols - 1)
                    {
                        str_to_print += $" {tile_symbol} |";
                        separator += "---+";
                    }
                    // For the last column, only add the state to a string
                    // and only add the horizontal separator sequence to the separator.
                    else
                    {
                        str_to_print += $" {tile_symbol} ";
                        separator += "---";
                    }
                }
                // For every row, display the string.
                Console.WriteLine(str_to_print);
                // For every row but the last, display the separator.
                if (ii < (this.rows - 1))
                {
                    Console.WriteLine(separator);
                }
            }
        }

        public bool claim_tile(int row, int col) 
        {
            /// <summary>
            /// The claim_tile method takes a row value and a column value and updates the state
            /// of the tile that it corresponds to, to match the player, if it is not already claimed.
            /// </summary>
            if (this.tileboard[row, col].claim == state.U)
            {
                if (this.player == 1)
                {
                    this.tileboard[row, col].claim = state.X;
                    this.player += 1;
                }
                else
                {
                    this.tileboard[row, col].claim = state.O;
                    this.player -= 1;
                }
                this.turn += 1;
                return true;
            }
            else
            {
                Console.WriteLine("This tile is already claimed. Claim another, please.");
                return false;
            }
        }

        public state check_victory()
        {
            /// <summary>
            /// The check_victory method returns a state based on whether or not a row of three of a player symbol occurs.
            /// </summary>
            state condition_met = state.U;
            // For each tile (row-column value combination)...
            for (int ii = 0; ii < this.rows; ii++)
            {
                for (int jj = 0; jj < this.cols; jj++)
                {
                    
                    // ...do four different checks.
                    for(int zz = -1; zz < 3; zz++)
                    {
                        int Arow=-10;
                        int Acol=-10;
                        int Brow=-10;
                        int Bcol=-10;

                        // First, check the two horizontal neighbours.
                        if (zz == -1)
                        {
                            Arow = ii;
                            Acol = jj + 1;
                            Brow = ii;
                            Bcol = jj - 1;
                        }
                        // Second, check the two downward diagonal neighbours.
                        else if (zz == 0)
                        {
                            Arow = ii + 1;
                            Acol = jj + 1;
                            Brow = ii - 1;
                            Bcol = jj - 1;
                        }
                        // Third, check the two vertical neighbours.
                        else if (zz==1)
                        {
                            Arow = ii + 1;
                            Acol = jj;
                            Brow = ii - 1;
                            Bcol = jj;
                        }
                        // Fourth, check the two upward diagonal neighbours.
                        else if (zz==2)
                        {
                            Arow = ii + 1;
                            Acol = jj - 1;
                            Brow = ii - 1;
                            Bcol = jj + 1;
                        }
                        // If all neighbour indices have valid values on the board...
                        if (Arow>-1 && Arow<this.rows &&
                            Acol>-1 && Acol<this.cols &&
                            Brow>-1 && Brow<this.rows &&
                            Bcol>-1 && Bcol<this.cols &&
                            // ...check if the current tile has a player symbol...
                            this.tileboard[ii, jj].claim!=state.U &&
                            // ...and check if both neighbours match said player symbol.
                            this.tileboard[ii,jj].claim==this.tileboard[Arow,Acol].claim && 
                            this.tileboard[ii, jj].claim == this.tileboard[Brow, Bcol].claim)
                        {
                            // If so, three tiles in a row have been claimed by one player.
                            condition_met = this.tileboard[ii,jj].claim;
                        }
                    }
                }
            }
            return condition_met;
        }

        public bool check_draw()
        {
            /// <summary>
            /// The check_draw method tests if a draw has occurred by seeing whether the 
            /// maximum number of turns have been reached, given the current board.
            /// </summary>
            if (this.turn == (this.rows * this.cols + 1)) // +1 because it checks AFTER last move.
                return true;
            else
                return false;
        }
    }
}

