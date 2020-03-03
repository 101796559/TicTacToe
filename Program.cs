using System;

namespace TicTacToe
{
    class Program
    {
        static string[] choices = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static int player = 1;
        static string mark;
        static int choice = 0;
        static bool validSelection = false;
        static Random random = new Random();

        static void Main(string[] args)
        {
            bool over = new Program().CheckWinCondition();

            while (!over) {
                validSelection = false;

                Console.Clear();
                over = new Program().CheckWinCondition();

                if (player == 1 || player > 2) {
                    player = 1;
                    mark = "X";
                }
                else if (player == 2) {
                    mark = "O"; 
                }

                System.Console.WriteLine("Player {0}\n", mark);

                new Program().ShowBoard();
                if (player == 1) {   
                    new Program().PlayerSelection();
                }
                else {
                    new Program().AiSelection();
                }
            }
            if (player == 3) player = 1;
            System.Console.WriteLine("\nPlayer {0} wins!", player);
        }

        void ShowBoard()
        {
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[1], choices[2], choices[3]);
            System.Console.WriteLine("---|---|---");
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[4], choices[5], choices[6]);
            System.Console.WriteLine("---|---|---");
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[7], choices[8], choices[9]);
        }
        void PlayerSelection()
        {
            while (!validSelection) {
                System.Console.Write("\nChoose a position: ");
                choice = Convert.ToInt32(Console.ReadLine());
                if (choices[choice] != "X" && choices[choice] != "O") {
                    validSelection = true;
                } else {
                    System.Console.WriteLine("{0} already has that spot!", choices[choice]);
                }
            }
            choices[choice] = mark;
            player++;
        }
        void AiSelection() {
            while (!validSelection) {
                choice = random.Next(1,9);
                if (choices[choice] != "X" && choices[choice] != "O") {
                    validSelection = true;
                } else {
                    System.Console.WriteLine("{0} already has that spot!", choices[choice]);
                }
            }
            choices[choice] = mark;
            player++;
        }
        bool CheckWinCondition()
        {
            // Horizontal
            if (choices[1] == choices[2] && choices[2] == choices[3]) {
                return true;
            }
            else if (choices[4] == choices[5] && choices[5] == choices[6]) {
                return true;
            }
            else if (choices[7] == choices[8] && choices[8] == choices[9]) {
                return true;
            }

            // Vertical
            if (choices[1] == choices[4] && choices[4] == choices[7]) {
                return true;
            }
            else if (choices[2] == choices[5] && choices[5] == choices[8]) {
                return true;
            }
            else if (choices[3] == choices[6] && choices[6] == choices[9]) {
                return true;
            }

            // Diagonal
            if (choices[1] == choices[5] && choices[5] == choices[9]) {
                return true;
            }
            else if (choices[3] == choices[5] && choices[5] == choices[7]) {
                return true;
            }

            // All spaces filled
            else if (choices[1] != "1" && choices[2] != "2" && choices[3] != "3" && choices[4] != "4" && choices[5] != "5" && choices[6] != "6" && choices[7] != "7" && choices[8] != "8" && choices[9] != "9") {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
