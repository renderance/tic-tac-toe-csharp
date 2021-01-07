using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    public enum state { U, X, O };

    class Program
    {
        static void Main(string[] args)
        {
            // Welcome message:
            Console.WriteLine("Hello, and welcome to TicTacToe v0.1a!");
            Console.WriteLine("Would you like to start a new game? y/n");
            string yesno = Console.ReadLine();

            // If user wants to play a game:
            while (yesno == "y")
            {
                // Initialize gameboard and win-condition flags.
                board gameboard = new board(3, 3);
                state winner = gameboard.check_victory();
                bool draw = false;

                // So long as no win condition is met:
                while (winner == state.U && draw == false)
                {
                    // Display current state of game:
                    Console.Clear();
                    Console.WriteLine("\n");
                    gameboard.print_board();

                    // Request move from player of current turn:
                    Console.WriteLine("\n");
                    Console.WriteLine($"Player {gameboard.player}, " +
                        $"please enter a valid tile.\n" +
                        $"Type a number for the row, press [enter], \n" +
                        $"type a number for the column, and press [enter] again.");

                    // Check validity of player input:
                    bool claim = false;
                    while (claim == false)
                    {
                        int rownum = -1;
                        int colnum = -1;
                        try
                        {
                            // ...expect new input and try to cast it to an integer...
                            Console.Write("Row:\t");
                            rownum = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Column:\t");
                            colnum = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            // ...and complain if you cannot.
                            Console.WriteLine("That was not a valid number.");
                        }
                        
                        // Check if the tile coordinates refer to a valid tile. Complain if not...
                        /*      Note: Because tile coordinates were set to [-1,-1] at initialization,
                         *      this complaint will also fire if a non-integer value was provided by the user.
                         */
                        if (rownum < 1 || colnum < 1 || rownum > gameboard.rows || colnum > gameboard.cols)
                        {
                            Console.WriteLine("That was not a valid tile.");
                        }
                        // ...and claim the tile for the current player if it is.
                        /*      Note: gameboard.claim_tile() will return 'false' if the tile was already claimed
                         *      and informs the user the tile is already claimed.
                         */
                        else
                        {
                            claim = gameboard.claim_tile(rownum - 1, colnum - 1);
                        }
                    }
                    // Once a valid move has been made, check if a tile-state reached victory condition, 
                    // or if a draw occurs.
                    /*      Note: gameboard.check_victory() returns a state (U,X or O). U for undecided,
                     *      O for player 2 wins or X for player 1 wins.
                     */
                    winner = gameboard.check_victory();
                    draw = gameboard.check_draw();
                }

                // As either a win condition or draw condition occurs, display the final board...
                Console.Clear();
                Console.WriteLine("\n");
                gameboard.print_board();
                Console.WriteLine("\n");
                
                // ...call out the winner...
                if (winner != state.U)
                {
                    Console.WriteLine($"Winner is: {winner}");
                }
                
                // ...or declare a draw.
                else if (draw == true)
                {
                    Console.WriteLine("Game ends in a draw.");
                }
                
                // Give player a choice to quit or continue.
                Console.WriteLine("Would you like to start a new game? y/n");
                yesno = Console.ReadLine();
            }
            // Closing message:
            Console.WriteLine("Goodbye!");
        }
    }
}