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
            while (true)
            {
                Console.WriteLine(board.array[0] + " " + board.array[1] + " " + board.array[2]);
                Console.WriteLine(board.array[3] + " " + board.array[4] + " " + board.array[5]);
                Console.WriteLine(board.array[6] + " " + board.array[7] + " " + board.array[8]);
                playmov(board);
                AI.AIOutput(board);
                board.array[AI.AISquare] = 2;
                board.CheckWin(2);
                board.remainingMoves--;
            }

        }
        static void playmov(Board board)
        {

            legalCheck Legal = new legalCheck();
            while (true)
            {
                Legal.square = Convert.ToInt32(Console.ReadLine());
                Legal.CheckSquare(board);
                board.CheckWin(1);
                Console.WriteLine(Legal.isLegal);
                Console.WriteLine(board.win);
                if (Legal.isLegal == true)
                {
                    Console.WriteLine();
                    board.array[Legal.square] = 1;
                    board.remainingMoves--;
                    break;
                }
                else
                {
                    Console.WriteLine("Zug nicht legal");
                }
        }
        }

    }
    class Board
    {
        public bool is0active = false;
        public int remainingMoves = 9;
        public int win = 0;
        public int[] array = new int[9];
        public bool GameEnd=false;
        public void BoardSetUp()
        {
            for (int i = 0; i < 9; i++)
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
                if (board.array[square] != 0)
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
        public int a = 0;
        public int a2 = 0;
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


            for (int i = 0; i <9; i++)
            {
                    Legal.square = i;
                    Legal.CheckSquare(board);// Checking if move is legal

                    if (Legal.isLegal)  // Move is legal
                    {

                        board.array[Legal.square] = 2;
                        if (board.CheckWin(2) == true)
                        {
                            board.array[Legal.square] = 0;
                            tries = 0;
                            AIWinsquare = Legal.square;
                            aiwin = true;
                        break;
                    }
                        else if (tries == 8)//no winning move possible
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
        public void AIWin(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random();


            for (int i = 0; i < 9; i++)
            {
                Legal.square = i;
                Legal.CheckSquare(board);// Checking if move is legal

                if (Legal.isLegal)  // Move is legal
                {

                    board.array[Legal.square] = 2;
                    if (board.CheckWin(2) == true)
                    {
                        a++;
                       board.array[Legal.square] = 0;
                        tries = 0;
                        AIWinsquare = Legal.square;
                        aiwin = true;
                    }
                    else if (tries == 9)//no winning move possible
                    {
                        aiwin = false;
                        tries = 0;
                        board.array[Legal.square] = 0;
                        break;
                    }
                    else
                    { //Not a winning move
                        board.array[Legal.square] = 0;
                        tries++;
                    }
                    board.array[Legal.square] = 0;
                }

            }

        }
        
        public void PlayerWin(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random();
            for (int i=0; i<9;i++)
            {
 

            

                    Legal.square = i;  //generate move
                    Legal.CheckSquare(board);  // Checking if move is legal
                    
                    if (Legal.isLegal)
                    {
                        board.array[Legal.square] = 1;
                        board.CheckWin(1);
                        if (board.CheckWin(1) == true)
                        {
                        a2--;
                        PlayerWinsquare = Legal.square;
                            board.array[Legal.square] = 0;
                            Playerwin = true;
                            tries = 0;
                            board.win = 0;

                    }
                        else if (tries == 8)
                        {

                            tries = 0;
                            board.array[Legal.square] = 0;
                            Playerwin = false;
                           
                        }
                        else
                        {
                        board.array[Legal.square] = 0;
                        tries++;

                        }
                    board.array[Legal.square] = 0;
                    }
            }
        }
        public void AIOutput(Board board)
        {
            Random random = new Random();
            legalCheck Legal = new legalCheck();
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
                
                /* bool moveFound=false;
                 while (!moveFound)
                 {
                     Legal.square = random.Next(0, 9);
                     Legal.CheckSquare(board);
                     if (Legal.isLegal)
                     {
                         AISquare = Legal.square;
                         break;
                     }
                 }     */
                int maxWert = allmoveeval[0];
                board.win = 0;
                evaluation(board, 1);
                for (int i = 1; i < allmoveeval.Length; i++)
                {
                    if (allmoveeval[i] > maxWert)
                    {
                        maxWert = allmoveeval[i];
                        maxIndex = i;
                    }
                }
                AISquare = maxIndex;

                /* Legal.square = maxIndex;
                 Legal.CheckSquare(board);
                 Console.WriteLine(Legal.square);
                 Console.WriteLine(Legal.isLegal);
                 if (Legal.isLegal == true)
                 {

                     Console.WriteLine(Legal.isLegal);
                     AISquare = maxIndex;
                 }
                 else
                 {
                     bool moveFound=false;
                      while (!moveFound)
                      {
                         Legal.square = random.Next(0, 8);
                          Legal.CheckSquare(board);
                          if (Legal.isLegal)
                          {
                              AISquare = Legal.square;
                              moveFound = true;
                              break;
                          }
                      }
                     }*/
            }
                Console.WriteLine("AI:"+AISquare);
        }
            public int[] allmoveeval = new int[9];
            public int[] allmoveeval2 = new int[9];
            public int maxIndex = 0;
            public void evaluation(Board board, int player)
            {
                legalCheck Legal = new legalCheck();
                for (int i = 0; i < 9; i++)
                {
                    Legal.square = i;
                    Legal.CheckSquare(board);
                    if (Legal.isLegal)
                    {
                        board.array[Legal.square] = player;
                        if (player == 2)
                        {
                            PlayerWin(board);
                            evaluation(board, 1);
                            board.array[Legal.square] = 0;
                            a--;
                    }
                        else if (player == 1)
                        {
                            AIWin(board);
                            evaluation(board, 2);
                            board.array[Legal.square] = 0;
                            a++;
                        }

                    /*
                    PlayerWin(board);

                    for (int i2 = 0; i2 < 9; i2++)
                    {
                        Legal.square = allMoves[i2];
                        Legal.CheckSquare(board);
                        if (Legal.isLegal)
                        {
                            board.array[Legal.square] = 1;
                            AIWin(board);
                            allmoveeval[i2] = a + a2;
                            Console.WriteLine("eval:" + allmoveeval[i2]);
                            board.array[Legal.square] = 0;
                            a2= 0;
                        }
                    }*/
                        allmoveeval[i] = a;
                        board.array[Legal.square] = 0;
                        a = 0;
                    }
                

                }
            
         }
    }
}
