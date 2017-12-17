using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /// <summary>
    /// Not finished
    /// </summary>
    public class TicTacToeTree
    {
        private Node _root;
        private byte depth=3;
        private int _count = 0;
        private Sign _maxSign;

        #region Constructors
        public TicTacToeTree(Board board, Sign sign)
        {
            _root = new Node(sign, board);
            _maxSign = sign;
        }//c-tor
        public TicTacToeTree()
        {
            _root = null;
        }//c-tor
        public TicTacToeTree(Node node, Sign maxSign)
        {
            _maxSign = maxSign;
            _root = node;
            Generate(depth, node);
            // _maxSign = _root.MoveSign;
        }
        #endregion

        public void Generate( byte _depth,Node node)
        {
            _count++;
            node.GenerateChildren(GetOppositSign(node.MoveSign));

            foreach (Node child in node.Children)
            {                
                if (child.Board.IsTerminal()||_depth==0)
                {
                    child.SetValue(_maxSign);
                    child.EvaluateParent(_maxSign);
                    continue;
                }

                Generate((byte)(--_depth), child);
            }
            node.EvaluateParent(_maxSign); 
        }

        private Sign GetOppositSign(Sign sign)
        {
            if (sign == Sign.X)
            {
                return Sign.O;
            }
            if (sign == Sign.O)
            {
                return Sign.X;
            }
            return Sign.EMPTY;
        }

        public Move FindBestMove()
        {
            if (_root == null)
            {
                return null;
            }
          //  Evaluate(_root);
            _root.Children.Sort();

            return new Move(_root.Children.First().Position);
        }

        public void SetValues(Node node)
        {
            if (node.Children == null||node.Children.Count==0)
            {
                node.SetValue(_root.MoveSign);
                return;
            }
            foreach (var child in node.Children)
            {
                SetValues(child);
            }
        }

        public void Evaluate(Node node)
        {

            if (node.Value==0)
            {
                foreach (var child in node.Children)
                {
                    Evaluate(child);
                    child.evala(_maxSign);
                }
            }
                     
        }
    }//TicTacToeTree
}//TicTacToeGame
