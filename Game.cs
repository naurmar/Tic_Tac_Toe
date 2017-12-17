using System;
using System.Collections.Generic;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represents tic tac toe game
    /// </summary>             
    public class Game
    {
        public event EventHandler GameOverHandler;
        public event EventHandler GameContinueHalder;

        #region Properties
        /// <summary>
        /// The property represent current player who make move
        /// </summary>
        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }

            set
            {
                _currentPlayer = value;
            }
        }//CurrentPlayer

        /// <summary>
        /// The stack intended to store game's moves to cancel last move
        /// </summary>
        private Stack<Move> _moves;
        protected Stack<Move> Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                _moves = value;
            }
        }//Moves

        /// <summary>
        /// Represent game board
        /// </summary>
        private Board _board;
        public Board Board
        {
            get { return _board; }
            set { _board = value; }
        }//Board

        /// <summary>
        /// The property represent state of a game
        /// </summary>
        private State _state;
        public State State
        {
            get { return _state; }
            set { _state = value; }
        }//State         
        #endregion

        #region Constructors
        public Game(Player player) : this()
        {
            CurrentPlayer = player;
        }//c-tor
        public Game()
        {
            _board = new Board();
            _board.OnCheck += OnBoardCheck;
            _state = State.FIRST_MOVE;
            _moves = new Stack<Move>();
        }//c-tor 
        #endregion

        /// <summary>
        ///Play a game
        /// </summary>
        public void Play()
        {
            UIManager.PrintHeader();//show header
            Move newMove;
            do
            {
                do
                {
                    UIManager.PrintEmptyBoard(_board.GameBoard);//show help game board
                    newMove = CurrentPlayer.MakeMove(_board);// player make new move
                } while (!_board.SetMove(newMove, CurrentPlayer.PlayerSign));//until move is located
                _moves.Push(newMove);//save move
                _board.Check(CurrentPlayer.PlayerSign);//chack for game is over
            } while (_state == State.CONTINUE);//while game state is continue
        }//Play 

        /// <summary>
        /// Rise event according to a game state
        /// </summary>
        /// <param name="state">Game current state</param>
        private void OnBoardCheck(State state)
        {
            _state = state;
            if (_state == State.CONTINUE)
            {
                GameContinueHalder?.Invoke(_board.GameBoard, EventArgs.Empty);
            }
            if (_state == State.WIN || _state == State.LOSE || _state == State.DRAW)
            {
                string str = (_state == State.DRAW) ? "DRAW" : _currentPlayer.Name;
                GameOverHandler?.Invoke(str, EventArgs.Empty);
            }
        }//OnBoardCheck
    }//Game
}//TicTacToeGame
