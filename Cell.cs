using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TicTacToeGame
{
    /// <summary>
    /// Class represent board square
    /// </summary>
    public class Cell
    {
        #region Properties
        private byte _x;
        /// <summary>
        /// The property represents x
        /// </summary>
        public byte X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value < 0 || value >= Board.GetRows())
                {
                    throw new IndexOutOfRangeException();
                }
                _x = value;
            }
        }

        private byte _y;
        /// <summary>
        /// The property represents y
        /// </summary>
        public byte Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0 && value >= Board.GetColumns())
                {
                    throw new IndexOutOfRangeException();
                }
                _y = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constract new cell
        /// </summary>
        /// <param name="point"></param>
        public Cell(Point point)
        {
            X = (byte)point.X;
            Y = (byte)point.Y;
        }//c-tor
        public Cell(byte x, byte y)
        {
            X = x;
            Y = y;
        }//c-tor
        public Cell()
        {
            Point point = GetCoordinates();
            X = (byte)(point.X - 1);
            Y = (byte)(point.Y - 1);
        }//c-tor 
        #endregion

        public Point GetCoordinates()
        {
            Console.Write("Enter move coordinate : ");
            char[] coor = Console.ReadLine().ToUpper().ToCharArray();
            Point newPoint = new Point();
            newPoint.Y = int.Parse((coor[0] - 64).ToString());
            newPoint.X = int.Parse(coor[1].ToString());
            return newPoint;
        }//GetCoordinate

        public override bool Equals(object obj)
        {
            if (obj is Cell)
            {
                Cell newCell = (Cell)obj;
                if (this.X == newCell.X && this.Y == newCell.Y)
                {
                    return true;
                }
            }
            return false;
        }//Equals
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }//GetHashCode
    }// End class Cell
}//TicTacToe
