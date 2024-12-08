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
                Console.WriteLine(AI.AISquare);
                board.CheckWin(2);
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
            // Alle Gewinnkombinationen prüfen
            int[][] winCombinations = new int[][]
            {
                new int[] {0, 1, 2},  // horizontale Reihen
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
                new int[] {0, 3, 6},  // vertikale Spalten
                new int[] {1, 4, 7},
                new int[] {2, 5, 8},
                new int[] {0, 4, 8},  // Diagonalen
                new int[] {2, 4, 6}
            };

            foreach (var combination in winCombinations)
            {
                if (array[combination[0]] == player &&
                    array[combination[1]] == player &&
                    array[combination[2]] == player)
                {
                    win = 1;// Spieler hat gewonnen
                    Console.WriteLine("Winner");
                    return true;
                }
            }

            return false;  // Kein Gewinn
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
        public int tries;
        public void randomMove(Board board)
        {
            legalCheck Legal = new legalCheck();
            Random random = new Random();
            Board Win = new Board(); 
            bool moveFound = false;



            while (!moveFound)  // Solange kein gültiger Zug gefunden wurde
            {
                Legal.square = random.Next(0, 9);  // Zufällige Zahl zwischen 0 und 8 generieren
                Legal.CheckSquare(board);  // Überprüfen, ob der Zug gültig ist

                if (Legal.isLegal)  // Wenn der Zug gültig ist, speichere den Wert
                {
                    if (Win.CheckWin(Legal.square))
                    {
                        AISquare = Legal.square;
                        moveFound = true;  // Beende die Schleife, wenn ein gültiger Zug gefunden wurde
                        tries++;
                        if (tries == Win.remainingMoves)
                        {
                          
                        }
                    }
                    tries++;
                    if (tries == Win.remainingMoves)
                    {

                    }
                }
            }
        }
    }
}
