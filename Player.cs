using System;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represents abstruct player
    /// </summary>
    public abstract class Player
    {
        #region Properties
        public Sign PlayerSign { get; set; }//PlayerSign

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }//Name 
        #endregion

        #region Constructors
        public Player(string name, Sign sing)
        {
            _name = name;
            PlayerSign = sing;
        }//c-tor
        public Player() : this("Player", Sign.X)
        {
        }//c-tor 
        #endregion

        public virtual Move MakeMove(Object obj)
        {
            Console.WriteLine();
            Console.CursorLeft = 10;
            Console.Write("{0} ({1}), your turn. ",this.Name.ToUpper(),PlayerSign);
            return null;
        }//MakeMove

        public static string GetNewPlayerName(string player)
        {
            Console.WriteLine();
            Console.CursorLeft = 10;
            Console.Write("{0}, enter your name: ", player.ToUpper());
            return Console.ReadLine();
        }//GetPlayerName
    }//Player  
}//TicTacToeGame
