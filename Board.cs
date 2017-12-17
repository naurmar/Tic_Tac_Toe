using System;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToeGame
{
    public delegate void BoardEvaluateHandler(State state);

    /// <summary>
    /// This class represents tic tac toe game board
    /// </summary>
    public class Board : ICloneable
    {
        public event BoardEvaluateHandler OnCheck;

        private static readonly byte _rows =3;
        private static readonly byte _columns = 3;
        private static readonly byte _winLength = 3;

        #region Properties
        /// <summary>
        ///The list intended to save winning line
        /// </summary>
        private LinkedList<Cell> _winLine;
        public LinkedList<Cell> WinLine
        {
            get
            {
                return _winLine;
            }

            set
            {
                if (value.Count == TicTacToeGame.Board.GetWinLength())
                {
                    _winLine = value;
                }
            }
        }//WinLine

        //A two dimention array represents a game board
        private byte[,] _board;
        public byte[,] GameBoard
        {
            get
            {
                return _board;
            }

            set
            {
                _board = value;
            }
        }

        //a array determines whether  positions on a board is open
        private bool[] _isPositionsOpen;
        public bool[] IsPositionsOpen
        {
            get
            {
                return _isPositionsOpen;
            }
            set
            {
                _isPositionsOpen = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new board
        /// </summary>
        public Board() : this(_rows, _columns) { }//c-tor
        public Board(byte rows, byte columns)
        {
            _board = new byte[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _board[i, j] = (byte)Sign.EMPTY;
                }
            }
            IsPositionsOpen = new bool[_columns * _rows];
            for (int i = 0; i < IsPositionsOpen.Length; i++)
            {
                IsPositionsOpen[i] = true;
            }
        }//c-tor 
        #endregion

        /// <summary>
        ///The function returns if a board is empty
        /// </summary>
        /// <returns>bool</returns>
        public bool IsEmpty()
        {
            foreach (bool position in _isPositionsOpen)
            {
                if (!position)
                {
                    return false;
                }
            }
            return true;
        }//IsEmpty    

        /// <summary>
        /// Returns the number of rows in board
        /// </summary>
        /// <returns></returns>
        public static byte GetRows()
        {
            return _rows;
        }//GetRows

        /// <summary>
        /// Returns the number of columns in board
        /// </summary>
        /// <returns></returns>
        public static byte GetColumns()
        {
            return _columns;
        }//GetColumns

        /// <summary>
        /// Returns winning line length
        /// </summary>
        /// <returns></returns>
        public static byte GetWinLength()
        {
            return _winLength;
        }//GetWinLength

        /// <summary>
        /// Returns sign (X or O) at the corresponding cell on the board
        /// </summary>
        /// <param name="cell"> square on the board</param>
        /// <returns></returns>
        internal Sign GetBoardCellSign(Cell cell)
        {
            return (Sign)_board[cell.X, cell.Y];
        }//GetBoardCellSign   

        /// <summary>
        /// Set new move on the board
        /// </summary>
        /// <param name="move">position to set</param>
        /// <param name="sign"> represent X or O</param>
        /// <returns></returns>
        internal bool SetMove(Move move, Sign sign)
        {
            try
            {
                if (_board[move.PositionCell.X, move.PositionCell.Y] != (byte)Sign.EMPTY)
                {
                    Console.CursorLeft = 10;
                    Console.Write("The place already taken. ");
                    return false;
                }
                _board[move.PositionCell.X, move.PositionCell.Y] = (byte)sign;
                IsPositionsOpen[move.Value] = false;
                return true;
            }
            catch (Exception ex)
            {
                Console.CursorLeft = 10;
                Console.Write(ex.Message);
                return false;
            }
        }//SetMove 

        internal void Check(Sign sign)
        {
            OnCheck?.Invoke((State)Evaluate(sign));
        }//Check

        /// <summary>
        /// The function intended to evaluate board state
        /// </summary>
        /// <returns>cost of board state</returns>
        internal sbyte Evaluate(Sign sign)
        {
            if (HasWinner(out _winLine))
            {
                if (GetBoardCellSign(_winLine.First.Value) == sign)
                {
                    return (sbyte)State.WIN;
                }
                else return (sbyte)State.LOSE;
            }
            else if (HasDraw())
            {
                return (sbyte)State.DRAW;
            }
            else return (sbyte)State.CONTINUE;
        }

        public bool IsTerminal()
        {
            LinkedList<Cell> wl = new LinkedList<Cell>();
            return HasWinner(out wl) || HasDraw();
        }

        #region Check For Win

        private bool CheckRowsForWin(out LinkedList<Cell> winLine)
        {
            for (byte i = 0; i < _rows; i++)
            {
                if (CheckRowForWin(i, out winLine))
                {
                    return true;
                }
            }
            winLine = null;
            return false;
        }//CheckRowsForWin
        private bool CheckColumnsForWin(out LinkedList<Cell> winLine)
        {
            for (byte i = 0; i < _columns; i++)
            {
                if (CheckColumnForWin(i, out winLine))
                {
                    return true;
                }
            }
            winLine = null;
            return false;
        }//CheckColumnsForWin
        private bool CheckRowForWin(byte row, out LinkedList<Cell> winLine)
        {
            winLine = new LinkedList<Cell>();
            for (byte j = 0; j < _columns - 1; j++)
            {
                winLine.Clear();
                if (_board[row, j] == (byte)Sign.EMPTY)
                {
                    continue;
                }
                winLine.AddFirst(new Cell(row, j));
                while ((j != _columns - 1) && _board[row, j] == _board[row, j + 1])
                {
                    winLine.AddLast(new Cell(row, (byte)(j + 1)));
                    if (winLine.Count == _winLength)
                        return true;
                    j++;
                }
                winLine.Clear();
            }
            winLine = null;
            return false;
        }//CheckRowForWin
        private bool CheckColumnForWin(byte column, out LinkedList<Cell> winLine)
        {
            winLine = new LinkedList<Cell>();
            for (byte i = 0; i < _rows - 1; i++)
            {
                winLine.Clear();
                if (_board[i, column] == (byte)Sign.EMPTY)
                {
                    continue;
                }
                winLine.AddFirst(new Cell(i, column));
                while (i != _columns - 1 && _board[i, column] == _board[i + 1, column])
                {
                    winLine.AddFirst(new Cell((byte)(i + 1), column));
                    if (winLine.Count == _winLength)
                        return true;
                    i++;
                }
                winLine.Clear();
            }
            winLine = null;
            return false;
        }//CheckColumnForWin        
        private bool CheckRLDiagonalForWin(byte index_i, byte index_j, out LinkedList<Cell> winLine)
        {
            winLine = new LinkedList<Cell>();
            for (byte i = index_i, j = index_j; i <= _rows - _winLength && j < _columns - 1; i++, j++)
            {
                winLine.Clear();
                if (_board[i, j] == (byte)Sign.EMPTY)
                {
                    continue;
                }
                winLine.AddFirst(new Cell(i, j));
                while (_board[i, j] == _board[i + 1, j + 1]) 
                {
                    winLine.AddLast(new Cell((byte)(i + 1), (byte)(j + 1)));
                    if (winLine.Count == _winLength)
                    {
                        return true;
                    }
                    i++;
                    j++;
                    if (i==_rows-1||j==_columns-1)
                    {
                        break;
                    }
                }
            }
            winLine = null;
            return false;
        }//CheckRLDiagonalForWin
        private bool CheckLRDiagonalForWin(byte i, byte j, out LinkedList<Cell> winLine)
        {
            winLine = new LinkedList<Cell>();
            for (; i >1 && j < _columns-1; i--, j++)
            {
                if (_board[i, j] == (byte)Sign.EMPTY)
                {
                    continue;
                }
                winLine.Clear();
                winLine.AddFirst(new Cell((byte)i, (byte)j));
                while (_board[i, j] == _board[i - 1, j + 1])
                {
                    winLine.AddLast(new Cell((byte)(i - 1), (byte)(j + 1)));
                    if (winLine.Count == _winLength)
                        return true;                  
                    i--;
                    j++;
                    if (i==0||j==_columns-1)
                    {
                        break;
                    }
                }
            }
            winLine = null;
            return false;
        }//CheckLRDiagonalForWin
        private bool CheckRLDiagonalsForWin(out LinkedList<Cell> winLine)
        {
            for (byte i = 0, j = 0; i <= _rows - _winLength; i++)
            {
                if (CheckRLDiagonalForWin(i, j, out winLine))
                {
                    return true;
                }
            }
            for (byte i = 0, j = 1; j <= _columns - _winLength; j++)
            {
                if (CheckRLDiagonalForWin(i, j, out winLine))
                {
                    return true;
                }
            }
            winLine = null;
            return false;
        }//CheckRLDiagonalsForWi
        private bool CheckLRDiagonalsForWin(out LinkedList<Cell> winLine)
        {
            for (byte i = (byte)(_winLength - 1), j = 0; i < _rows; i++)
            {
                if (CheckLRDiagonalForWin(i, j, out winLine))
                {
                    return true;
                }
            }
            for (byte i = (byte)(_rows - 1), j = 1; j <= _columns - _winLength; j++)
            {
                if (CheckLRDiagonalForWin(i, j, out winLine))
                {
                    return true;
                }
            }
            winLine = null;
            return false;
        }//CheckLRDiagonalsForWin
        /// <summary>
        /// Checks the board configuration for a winner
        /// </summary>
        /// <param name="winLine">winning line </param>
        /// <returns>true if there is a winner and false otherwise</returns>
        public bool HasWinner(out LinkedList<Cell> winLine)
        {
            if (CheckRowsForWin(out winLine) || CheckColumnsForWin(out winLine) || CheckRLDiagonalsForWin(out winLine) || CheckLRDiagonalsForWin(out winLine))
                return true;
            return false;
        }//HasWinner
        /// <summary>
        /// Checks the board configuration to see if it is currently no open positions.
        /// </summary>
        /// <returns>true if there is a draw and false otherwise</returns>
        public bool HasDraw()
        {
            foreach (bool isOpen in IsPositionsOpen)
            {
                if (isOpen)
                    return false;
            }
            return true;
        }//HasDraw

        #endregion

        #region ICloneable Members
        /// <summary>
        /// Implementation ICloneable interface
        /// To copy object board configuration when searching for a move
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Board clone = new Board();
            clone.GameBoard = (byte[,])GameBoard.Clone();
            clone.IsPositionsOpen = (bool[])IsPositionsOpen.Clone();
            return clone;
        }//Clone  
        #endregion
    }//Board
}//TicTacToe
