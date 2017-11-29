using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
       private char[,] gameBoard = new char[3, 3];
        public char[,] GameBoard
        {
            get { return gameBoard; }
            set { this.gameBoard = value; } 
        }

        public Board()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = ' ';
                }
            }
        }
        public Board(Board board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.gameBoard[i,j] = board.GameBoard[i,j];
                }
            }
        }

        private char x = 'x';
        private char o = 'o';

        private char marksTurn;
        public char MarksTurn { get { return this.marksTurn; } set { this.marksTurn = value; } }

        public void submitMove(int row, int column, char marker)
        {
            this.gameBoard[row, column] = marker;
        }

        int anzahlAufrufeVonSubmitMove = 0;
        public void submitMove(int action, char marker)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (count == action)
                    {
                        submitMove(i, j, marker);
                    }
                    count++;
                }
            }
            ++anzahlAufrufeVonSubmitMove;
        }

        public void changeMarksTurn()
        {
            if (marksTurn == 'x')
            {
                this.marksTurn = 'o';
            }
            else
            {
                this.marksTurn = 'x';
            }
        }

        private  int count = 0;
        public bool isCat()//Tie
        {
            count++;
            if(count > 8)
            {
                return true;
            }
            else
                return false;
        }

        public bool isWin()
        {
            if (checkHorizontal())
            {
               return true;
            }
            else if (checkVertical())
            {
                return true;
            }
            else if (checkDiagonal())
            {
                return true;
            }

            return false;
        }

        private bool checkHorizontal()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == x || gameBoard[i, 0] == o)
                {
                    if (gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 1] == gameBoard[i, 2])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkVertical()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[1, i] == x || gameBoard[1, i] == o)
                {
                    if (gameBoard[0, i] == gameBoard[1, i] && gameBoard[1, i] == gameBoard[2, i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkDiagonal()
        {
            for (int i = 0; i < 2; i++)
            {
                int a = 0;
                int b = 0;

                char[] charDL = new char[3];
                char[] charDR = new char[3];
                for (int j = 0; j < 3; j++)
			    {
                    
                    if (i == 1)
                    {
                        charDL[j] = gameBoard[a++, b++];
                    }
                    else if (i == 0)
                    {
                        if (j == 0)
                        {
                            b = 2;
                        }
                        charDR[j] = gameBoard[a++, b--];
                    }
			    }

                if (charDL[0] == x || charDL[0] == o)
                {
                    if (charDL[0] == charDL[1] && charDL[1] == charDL[2])
                    {
                        return true;
                    }
                }
                if (charDR[0] == x || charDR[0] == o)
                {
                    if (charDR[0] == charDR[1] && charDR[1] == charDR[2])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //_____________________________________________________________________________________
        //KI Funktionen -------v

        private int[,] boardWithInfoOfBestMoves = new int[3,3];
        private int row = 0;
        private int column = 0;

        public void nameToBeChanged()
        {
            char marktAtRowColumn = gameBoard[row, column];
        }
    }
}
