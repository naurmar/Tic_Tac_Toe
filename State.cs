using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public enum State : sbyte
    {
        FIRST_MOVE=1,
        CONTINUE=2,
        DRAW=5,
        WIN=10,
        LOSE=-10
    }//State
}
