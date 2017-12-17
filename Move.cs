using System.Drawing;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represent  move on board
    /// </summary>
   public class Move
    {
        #region Properties
        private byte _value;
        public byte Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }//Value

        private Cell _positionCell;
        public Cell PositionCell
        {
            get { return _positionCell; }
            set
            {
                    _positionCell = value;
            }
        }//PositionCell 
        #endregion

        #region Constructors
        public Move(byte value) : this()
        {
            Value = value;
            PositionCell = new Cell(new Point(0, 0));
            SetCellFromValue();
        }//c-tor
        public Move(Cell cell) : this()
        {
            PositionCell = cell;
            SetValueFromCell();
        }//c-tor
        public Move()
        {
        }//c-tor 
        #endregion

        public bool IsValid()
        {
            return (PositionCell.X < Board.GetColumns() && PositionCell.Y < Board.GetRows());
        }//IsValid

        public bool IsOpen(Board board)
        {
            return (board.GetBoardCellSign(this.PositionCell) == Sign.EMPTY);
        }//IsOpen

        public void SetValueFromCell()
        {
          int  value = PositionCell.X * (Board.GetColumns()) + PositionCell.Y;        
          Value = (byte)value;
        }//SetValueFromCell

        public void SetCellFromValue()
        {            
            int x = Value / Board.GetColumns();
            PositionCell.X = (byte)x;
            int y = Value % Board.GetRows();
            PositionCell.Y = (byte)y;
        }//SetCellFromValue
    }//Position
}//TicTacToeGame
