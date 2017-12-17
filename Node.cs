using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /// <summary>
    /// This class represent tic tac toe tree node. 
    /// Not finished
    /// </summary>
    public class Node : IEnumerable<Node>, IComparable<Node>
    {
        #region Properties
        private Node _parent;
        public Node Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

        private sbyte _value;
        public sbyte Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        private Sign _moveSign;
        public Sign MoveSign
        {
            get
            {
                return _moveSign;
            }

            set
            {
                _moveSign = value;
            }
        }

        private Board _board;
        public Board Board
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

        private List<Node> _children;
        public List<Node> Children
        {
            get
            {
                return _children;
            }

            set
            {
                _children = value;
            }
        }

        public byte Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }
        private byte _position;
        #endregion

        #region Constructor
        public Node(Sign sign, Object obj)
        {
            if (obj is Board)
            {
                Board newBoard = (Board)obj;
                Board = (Board)newBoard.Clone();
            }
            _moveSign = sign;
            _parent = null;
            _value = 0;
            _children = null;
        }//c-tor
        #endregion

        /// <summary>
        /// Generate children
        /// </summary>
        /// <param name="sign of computer player"></param>
        public void GenerateChildren(Sign sign)
        {
            _children = new List<Node>();
            for (byte i = 0; i < _board.IsPositionsOpen.Length; i++)
            {
                if (_board.IsPositionsOpen[i] == true)
                {
                    Move newMove = new Move(i);
                    Board clone = (Board)_board.Clone();
                    clone.SetMove(newMove, sign);
                    Node newNode = new Node(sign, clone);
                    newNode.Position = i;
                    newNode.Parent = this;
                    
                    _children.Add(newNode);
                }
            }
        }

        public List<byte> GetOpenPositions()
        {
            bool[] tempArray = _board.IsPositionsOpen;
            List<byte> tempList = new List<byte>();
            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i] == true)
                {
                    tempList.Add((byte)i);
                }
            }
            return tempList;
        }

        public void SetValue(Sign sign)
        {
            Value = Board.Evaluate(sign);
        }

        public void EvaluateParent(Sign maxSign)
        {
            if (Parent==null)
            {
                return;
            }

            if (MoveSign==maxSign)
            {
                if (Parent.Value<Value)
                {
                    Parent.Value = Value;
                }
            }
            else
            {
                if (Parent.Value>Value)
                {
                    Parent.Value = Value;
                }
            }
        }

        internal void evala(Sign maxSign)
        {
            if (Children==null)
            {
                return;
            }
           Children.Sort();
            if (MoveSign==maxSign)
            {
                _value = Children.Last().Value;
            }
             

            else
              _value=  Children.First().Value;
        }

        #region IEnumerable Members
        public IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)_children).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Node>)_children).GetEnumerator();
        }
        #endregion

        #region ICompareable Members
        public int CompareTo(Node node)
        {
            if (this.Value>node.Value)
            {
                return 1;
            }
            else if (this.Value<node.Value)
            {
                return -1;
            }

            else return 0;
        }
        #endregion
    }//Node
}//TicTacToeGame
