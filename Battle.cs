using System;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToeGame
{
    public delegate void SelectOptionHendler();

    /// <summary>
    /// This class represents a Tic Tac Toe Game battle.
    /// </summary>
    class Battle
    {
        #region Properties
        /// <summary>
        /// Property Storage represents games stack and 
        ///intended for statistics .
        /// </summary>
        private Stack<Game> Storage { get; set; }//Storage

        /// <summary>
        /// Property Score intended to store players score in multiple games battle. 
        /// </summary>
        public Hashtable Score
        {
            get; set;
        }//Score

        /// <summary>
        /// Property represents current game 
        /// </summary>
        private Game _currentGame;
        public Game CurrentGame
        {
            get
            {
                return _currentGame;
            }
            set
            {
                _currentGame = value;
            }
        }//CurrentGame

        /// <summary>
        /// Property Player1 represents battle's first player.
        /// </summary>
        private Player _player1;
        public Player Player1
        {
            get
            {
                return _player1;
            }

            set
            {
                while (value.Name == string.Empty)
                {
                    Console.CursorLeft = 10;
                    Console.WriteLine("Name can'not be empty. ");
                    value.Name = Player.GetNewPlayerName("player 1");
                }
                _player1 = value;
            }
        }//Player1

        /// <summary>
        /// Property Player2 represents battle's second player.
        /// </summary>
        private Player _player2;
        public Player Player2
        {
            get
            {
                return _player2;
            }
            set
            {
                while (_player1.Name == value.Name || value.Name == string.Empty)
                {
                    Console.CursorLeft = 10;
                    if (value.Name == string.Empty)
                    {
                        Console.WriteLine("Name can'not be empty. ");
                    }
                    else
                        Console.WriteLine("This name already exist. ");
                    value.Name = Player.GetNewPlayerName("player 2");
                }
                _player2 = value;
            }
        }//Player2 
        #endregion

        #region Constructors
        /// <summary>
        /// Construct new battle
        /// </summary>
        public Battle(BattleType type)
        {
            _player1 = new Human(Player.GetNewPlayerName("player1"), Sign.X);
            if (type == BattleType.ONEPLAYER)
                _player2 = new Computer(Sign.O);
            else
                Player2 = new Human(Player.GetNewPlayerName("player2"), Sign.O);
            InitScore();
            Storage = new Stack<Game>();
            _currentGame = new Game(_player1);
            _currentGame.GameContinueHalder += OnGameContinue;
            _currentGame.GameOverHandler += OnGameOver;
            UIManager.OnContinueBattle += UIManager_OnBattleContinue;
        }//c-tor
        public Battle() : this(BattleType.ONEPLAYER) { }//c-tor 
        #endregion

        private void UIManager_OnBattleContinue()
        {
            UIManager.PrintHeader();
            SwitchPlayers();
            _currentGame = new Game(Player1);
            _currentGame.GameContinueHalder += OnGameContinue;
            _currentGame.GameOverHandler += OnGameOver;
        }//UIManager_OnBattleContinue

        private void OnGameOver(object sender, EventArgs e)
        {
            SetScore();
            UIManager.PrintHeader();
            UIManager.PrintBoard(_currentGame.Board);
            UIManager.PrintWinner((string)sender);           
            UIManager.PrintScore(Score);
        }//OnGameOver

        private void OnGameContinue(object sender, EventArgs e)
        {
            UIManager.PrintHeader();
            ChangeCurrentPlayer();
            UIManager.PrintEmptyBoard((byte[,])sender);
            UIManager.PrintBoard((byte[,])sender);
        }//OnGameContinue

        /// <summary>
        /// The function switch players.
        /// One who played for the toe, will be playing for crosses and makes move first,
        /// and vice versa
        /// </summary>
        private void SwitchPlayers()
        {
            if (Player1.PlayerSign == Sign.X)
            {
                Player1.PlayerSign = Sign.O;
                Player2.PlayerSign = Sign.X;
            }
            else
            {
                Player1.PlayerSign = Sign.X;
                Player2.PlayerSign = Sign.O;
            }
            Player tempPlayer = Player1;
            Player1 = Player2;
            Player2 = tempPlayer;
        }//SwitchPlayers

        /// <summary>
        /// Set new score to hashtable
        /// </summary>
        private void SetScore()
        {
            if (CurrentGame.State == State.DRAW)
                return;

            IDictionaryEnumerator denum = Score.GetEnumerator();
            DictionaryEntry dentry;
            while (denum.MoveNext())
            {
                dentry = (DictionaryEntry)denum.Current;
                if (dentry.Key.ToString() == CurrentGame.CurrentPlayer.Name)
                {
                    int score = (int)dentry.Value;
                    score++;
                    dentry.Value = score;
                    Score.Remove(CurrentGame.CurrentPlayer.Name);
                    Score.Add(dentry.Key, dentry.Value);
                    break;
                }
            }
        }//SetScore

        /// <summary>
        /// Create new storage for players score
        /// </summary>
        private void InitScore()
        {
            Score = new Hashtable(2);
            Score.Add(_player1.Name, 0);
            Score.Add(_player2.Name, 0);
        }//InitScore

        /// <summary>
        /// Changes current player to make next move
        /// </summary>
        private void ChangeCurrentPlayer()
        {
            if (_currentGame.CurrentPlayer == Player1)
            {
                _currentGame.CurrentPlayer = Player2;
            }
            else
            {
                _currentGame.CurrentPlayer = Player1;
            }
        }//ChangeCurrentPlayer        
    }//TicTacToeGame
}//TicTacToe
