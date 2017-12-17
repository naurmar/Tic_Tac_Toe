using System;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represent computer player
    /// </summary>
    public class Computer : Player
    {
        public Computer(Sign sign) : base("Computer", sign)
        {
        }//c-tor

        /// <summary>
        /// Computer move
        /// </summary>
        /// <param name="board">current board positions</param>
        /// <returns>Returns new move made by computer</returns>
        public override Move MakeMove(Object board)
        {
            base.MakeMove(null);
            Node newNode = new Node(PlayerSign, board);
            TicTacToeTree tree = new TicTacToeTree(newNode, PlayerSign);
            return tree.FindBestMove();
        }//MakeMove    
    }//Computer
}//TicTacToeGame
