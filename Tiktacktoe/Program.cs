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
            while(Legal.isLegal==true)
            {
                Legal.square = Convert.ToInt32(Console.ReadLine());
                Legal.CheckSquare(board);
                board.array[Legal.square] = 1;
                board.CheckWin(1);
                board.remainingMoves--;
                AI.randomMove(board);
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
                    Console.WriteLine("Winner");
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
        public void randomMove(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random(); 
            bool moveFound = false;


            while (!moveFound)  
            {
                Legal.square = random.Next(0, 9);  //randomly generate move
                Legal.CheckSquare(board);  // Checking if move is legal

                if (Legal.isLegal)  // Move is legal
                {
                    board.array[Legal.square] = 2;
                    board.CheckWin(2);
                    if (board.CheckWin(2)==true)
                    {
                        AISquare = Legal.square;
                        Console.WriteLine(AISquare);
                        Console.WriteLine("Winner:AI");
                        moveFound = true;  // Ends if move is found
                    }
                    else if (tries == board.remainingMoves)//no winning move possible
                    {
                        AISquare = Legal.square;
                        moveFound = true;
                        tries = 0;
                        Console.WriteLine(AISquare);

                    }
                    else { //Not a winning move
                        
                        board.array[Legal.square] = 0;
                        tries = tries + 1;

                    }

                }
            }
            
        }
    }
}
