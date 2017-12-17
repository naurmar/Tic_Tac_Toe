using System;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represents tic tac toe game human player
    /// </summary>
    public class Human : Player
    {
        #region Constructors
        public Human(Sign sign) : this("", sign)
        {
        }//c-tor
        public Human(string name, Sign sign) : base(name, sign)
        {
        }//c-tor 
        #endregion

        /// <summary>
        /// Make player move
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Move</returns>
        public override Move MakeMove(Object obj)
        {
            base.MakeMove(null);
            Move newMove = null;
            bool isDone = false;
            while (!(isDone))
            {
                try
                {
                    newMove = new Move((new Cell()));
                    isDone = true;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.CursorLeft = 10;
                    Console.Write("Invalid coordinate. ");
                }
                catch (Exception)
                {
                    Console.CursorLeft = 10;
                    Console.Write("Incorrect input. ");
                }
            }
            return newMove;
        }//MakeMove   
    }//Human
}//TicTacToeGame

;