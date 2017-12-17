using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TicTacToeGame
{
    /// <summary>
    /// This static class represent user interface 
    /// </summary>
    static class UIManager
    {
        public static event SelectOptionHendler OnContinueBattle;
        public static event SelectOptionHendler OnNewBattle;

        public static int ConsoleCursorLeft()
        {
            return (Console.WindowWidth / 2) - (Board.GetColumns() * 6) - 5;
        }//ConsoleCursorLef

        #region Print Menu        
        public static void PrintHeader()
        {
            Console.Clear();
            Console.CursorTop = 1;
            PrintLine('*', ConsoleColor.DarkBlue);
            string header = "tic tac toe game";
            Console.CursorLeft = Console.WindowWidth / 2 - header.Length / 2;
            Console.WriteLine(header.ToUpper());
            PrintLine('*', ConsoleColor.DarkBlue);
            Console.ResetColor();
        }//PrintHeader
        private static void PrintMainMenu()
        {
            PrintHeader();
            PrintMenu(new string[] { "One pleyer ", "Two pleyers", "Exit       " }, ConsoleColor.Gray);
        }//PrintMainMenu
        public static void PrintMenu(string[] menu, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.CursorTop = 8;
            for (int i = 0; i < menu.Length; i++)
            {
                Console.CursorLeft = 10;
                Console.WriteLine("{0} ({1})", menu[i].ToUpper(), i + 1);
            }
            Console.ResetColor();
            Console.WriteLine();
        }//PrintMenu 
        public static void PrintLine(char sign, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(sign);
            }
        }//PrintLine 
        #endregion

        #region Print Board
        /// <summary>
        /// Print empty board
        /// </summary>
        public static void PrintEmptyBoard(byte[,] board)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            PrintAllGorizontLines('_', new Point(Console.BufferWidth / 2 + 5, 6));
            PrintAllVerticalLines('|', new Point(Console.BufferWidth / 2 + 5, 7));
            PrintTemplate(new Point(Console.BufferWidth / 2 + 8, 8), board);
            Console.ResetColor();
        }//PrintEmptyBoard

        /// <summary>
        /// Print the board configuration
        /// </summary>
        public static void PrintBoard(byte[,] board)
        {
            PrintAllGorizontLines('_', new Point(ConsoleCursorLeft(), 6));
            PrintAllVerticalLines('|', new Point(ConsoleCursorLeft(), 7));
            PrintValue(new Point(ConsoleCursorLeft() + 3, 8), null, board);
        }//PrintBoard

        /// <summary>
        /// Print the board configuration with winning line
        /// </summary>
        public static void PrintBoard(Board board)
        {
            int x = Console.BufferWidth / 2 + 10;
            PrintAllGorizontLines('_', new Point(x, 7));
            PrintAllVerticalLines('|', new Point(x, 8));
            PrintValue(new Point(x + 3, 9), board.WinLine, board.GameBoard);
        }//PrintBoard
        private static void PrintTemplate(Point point, byte[,] board)
        {
            for (int i = 0, l = point.Y; i < board.GetLength(0); i++, l += 3)
            {
                for (int j = 65, m = 0, k = point.X; j < board.GetLength(1) + 65; j++, k += 6, m++)
                {
                    Console.CursorLeft = k;
                    Console.CursorTop = l;
                    if (board[i, m] == (byte)Sign.EMPTY)
                    {
                        Console.Write(((char)j) + (i + 1).ToString());
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }//PrintTemplate
        private static void PrintValue(Point point, LinkedList<Cell> winLine, byte[,] board)
        {
            for (int i = 0, l = point.Y; i < board.GetLength(0); i++, l += 3)
            {
                for (int j = 0, k = point.X; j < board.GetLength(1); j++, k += 6)
                {
                    Console.CursorLeft = k;
                    Console.CursorTop = l;
                    if ((Sign)board[i, j] == Sign.EMPTY)
                        Console.WriteLine(" ");
                    else if (winLine != null)
                    {
                        if (winLine.Contains(new Cell((byte)i, (byte)j)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write((Sign)board[i, j]);
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write((Sign)board[i, j]);
                        }

                    }
                    else
                        Console.Write((Sign)board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }//PrintValue
        private static void PrintGorizontLine(char sign, Point point)
        {
            int columns = Board.GetColumns();
            Console.CursorLeft = point.X + 1;
            Console.CursorTop = point.Y;
            for (int i = 0; i < columns * 6 - 1; i++)
            {

                Console.Write(sign);
            }
        }//PrintGorizontLine
        private static void PrintVerticalLine(char sign, Point point)
        {
            int rows = Board.GetRows();
            Console.CursorTop = point.Y;
            for (int i = 0; i < rows * 3; i++)
            {
                Console.CursorLeft = point.X;
                Console.WriteLine(sign);
            }
            Console.WriteLine();
        }//PrintVerticalLine
        private static void PrintAllVerticalLines(char sign, Point point)
        {
            int columns = Board.GetColumns();
            int j = point.X;
            for (int i = 0; i <= columns; i++, j += 6)
            {
                point.X = j;
                PrintVerticalLine(sign, point);
            }
        }//PrintAllVerticalLines
        private static void PrintAllGorizontLines(char sign, Point point)
        {
            int rows = Board.GetRows();
            int j = point.Y;
            for (int i = 0; i <= rows; i++, j += 3)
            {
                point.Y = j;
                PrintGorizontLine(sign, point);
            }
        }//PrintAllGorizontLines
        #endregion

        #region Select Options
        /// <summary>
        /// Select with whom to play in a new battle.
        /// </summary>
        /// <returns>BattleType</returns>
        public static BattleType SelectBattleType()
        {
            PrintHeader();
            PrintMenu(new string[] { "One pleyer ", "Two pleyers", "Exit       " }, ConsoleColor.Gray);
            byte selection = 0;
            do
            {
                Console.WriteLine();
                Console.CursorLeft = 10;
                Console.Write("Please select the option: ");
                try
                {
                    selection = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.CursorLeft = 10;
                    Console.WriteLine("Incorrect input. ");
                    continue;
                }
                Console.CursorLeft = 10;
                switch (selection)
                {
                    case 1:
                        if (Board.GetColumns()>3&&Board.GetRows()>3)
                        {
                            Console.WriteLine("The board is too big to play. Change the settings of the board. ");
                            break;
                        }
                        else
                        return BattleType.ONEPLAYER;
                      //  Console.WriteLine("Not implemented yet. ");
                    // Environment.Exit(0);
                    //  break;
                    case 2:
                        return BattleType.TWOPLAYERS;
                    case 3:
                        Console.WriteLine("May be next time... Bay bay! ;-)");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. ");
                        break;
                }
            } while (selection != 3);
            return BattleType.ONEPLAYER;
        }//SelectBattleType    
        public static bool IsSelectedNewBattle()
        {
            PrintMenu(new string[] { "Continiue battle", "new battle", "exit" }, ConsoleColor.Gray);
            byte selection = 0;
            do
            {
                Console.CursorLeft = 10;
                Console.Write("Please select the options: ");
                try
                {
                    selection = byte.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.CursorLeft = 10;
                    Console.WriteLine("Incorrect input. ");
                    continue;
                }
                Console.CursorLeft = 10;
                switch (selection)
                {
                    case 1:
                        OnContinueBattle?.Invoke();
                        return false;
                    case 2:
                        OnNewBattle?.Invoke();
                        return true;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. ");
                        break;
                }
            } while (selection != 3);
            return false;
        }//HasSelectedNewBattle   
        #endregion

        /// <summary>
        /// Print winner
        /// </summary>
        public static void PrintWinner(string str)
        {
            string winner;
            if (str == State.DRAW.ToString())
                winner = State.DRAW.ToString();
            else
                winner = string.Format("{0} win", str);
            Console.CursorLeft = Console.BufferWidth / 2 + 10;
            Console.CursorTop = 6;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(winner.ToUpper());
            Console.ResetColor();
        }//PrintWinner  

        /// <summary>
        /// Print players score 
        /// </summary>
        public static void PrintScore(Hashtable scores)
        {
            Console.CursorTop = 6;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorLeft = 10;
            Console.Write("SCORE:");
            foreach (DictionaryEntry de in scores)
            {
                Console.Write(" {0} {1}\t", de.Key.ToString().ToUpper(), de.Value);
            }
            Console.WriteLine();
        }//PrintScore
    }//UIManager
}//TicTacToeGame
