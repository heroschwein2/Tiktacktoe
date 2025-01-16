using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiktacktoe
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.BoardSetUp();
            legalCheck Legal = new legalCheck();
            randomPlay AI = new randomPlay();
            while (Legal.isLegal == true)
            {
                Legal.square = Convert.ToInt32(Console.ReadLine());
                Legal.CheckSquare(board);
                board.array[Legal.square] = 1;
                board.CheckWin(1);
                board.remainingMoves--;
                AI.AIOutput(board);
                board.array[AI.AISquare] = 2;
                board.CheckWin(2);
                board.remainingMoves--;
            }

        }
    }
    class Board
    {
        public int remainingMoves = 9;
        public int win = 0;
        public int[] array = new int[9];
        public void BoardSetUp()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        public bool CheckWin(int player)
        {
            // All win combinations
            int[][] winCombinations = new int[][]
            {
                new int[] {0, 1, 2},  // horizontal
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
                new int[] {0, 3, 6},  // vertical
                new int[] {1, 4, 7},
                new int[] {2, 5, 8},
                new int[] {0, 4, 8},  // Diagonal
                new int[] {2, 4, 6}
            };

            foreach (var combination in winCombinations)
            {
                if (array[combination[0]] == player &&
                    array[combination[1]] == player &&
                    array[combination[2]] == player)
                {
                    win = 1;//Player won
                    
                    return true;
                }
            }

            return false;  // No win
        }
    }
    class legalCheck
    {
        public int square = 0;
        public bool isLegal = true;
        public void CheckSquare(Board board)
        {

            if (board.win == 0)
            {
                if (board.array[square] == 1 || board.array[square] == 2)
                {
                    isLegal = false;
                }
                else
                {
                    isLegal = true;
                }
            }
            else
            {
                isLegal = false;
            }
        }

    }
    class randomPlay
    {
        public int AISquare;
        public int tries = 0;
        public bool aiwin;
        public bool Playerwin;
        public int AIWinsquare;
        public int PlayerWinsquare;
        public void randomMove(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random();
            bool moveFound = false;

            for (int i = 0; i <9; i++)
            {
                    Legal.square = allMoves[i];
                    Legal.CheckSquare(board);// Checking if move is legal
                    moveFound = false;
                    if (Legal.isLegal)  // Move is legal
                    {

                        board.array[Legal.square] = 2;
                        board.CheckWin(2);
                        if (board.CheckWin(2) == true)
                        {

                            board.array[Legal.square] = 0;
                            tries = 0;
                            AIWinsquare = Legal.square;
                            aiwin = true;
                            moveFound = true;
                            break;
                    }
                        else if (tries == board.remainingMoves)//no winning move possible
                        {

                        aiwin = false;
                            
                            tries = 0;
                            board.array[Legal.square] = 0;
                            break;
                            


                        }
                        else
                        { //Not a winning move

                            board.array[Legal.square] = 0;
                            tries = tries + 1;


                        }

                    }
                
            }

        }
        public int[] allMoves = new int[9];
        public void intallmove()
        {
            for (int i = 0; i < allMoves.Length; i++)
            {
                allMoves[i] = i;
            }
        }
        public void PlayerWin(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random();
            bool moveFound = false;
            for (int i=0; i<9;i++)
            {
            intallmove();

            

                    Legal.square = allMoves[i];  //randomly generate move
                    Legal.CheckSquare(board);  // Checking if move is legal
                    
                    if (Legal.isLegal)
                    {
                        board.array[Legal.square] = 1;
                        board.CheckWin(1);
                        if (board.CheckWin(1) == true)
                        {
                            PlayerWinsquare = Legal.square;
                            board.array[Legal.square] = 0;
                            Playerwin = true;
                            moveFound = true;
                            tries = 0;
                        }
                        else if (tries == board.remainingMoves)
                        {
                            tries = 0;
                            board.array[Legal.square] = 0;
                            Playerwin = false;
                            moveFound = true;
                        }
                        else
                        {
                            board.array[Legal.square] = 0;
                            tries = tries + 1;

                        }
                    }
                
            }
        }
        public void AIOutput(Board board)
        {
            Random random = new Random();
            legalCheck Legal = new legalCheck();
            intallmove();
            randomMove(board);
            PlayerWin(board);
            if (Playerwin)
            {
                if (aiwin)
                {
                    AISquare = AIWinsquare;
                }
                else
                {
                    AISquare = PlayerWinsquare;
                    board.win = 0;
                }
            }
            else if (aiwin)
            {
                AISquare = AIWinsquare;
                board.win = 0;
            }
            else
            {
                bool moveFound=false;
                while (!moveFound)
                {
                    Legal.square = random.Next(0, 9);
                    Legal.CheckSquare(board);
                    if (Legal.isLegal)
                    {
                        AISquare = Legal.square;
                        moveFound=true;
                        break;
                    }
                }     
                board.win = 0;
            }
            Console.WriteLine(AISquare);
        }

    }
}
