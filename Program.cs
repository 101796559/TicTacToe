using System;

namespace TicTacToe
{
    class Program {
        static string[] choices = { "filler", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static int player = 1;
        static string mark;
        static int choice;
        static bool validSelection;
        static int over;

        public static void Main(string[] args) {
            Console.Clear();
            new Program().RunGame();    
        }

        public void RunGame() {
            // Welcome player to game

            // Determine opponent
            bool flag = false;
            int opponent = 2;
            while (!flag) {
                System.Console.WriteLine("Choose your opponent:\n1. Live Multiplayer\n2. AI");
                flag = int.TryParse(Console.ReadLine(), out opponent);
                System.Console.WriteLine("\nEnter either 1 or 2.");
            }

            int playAgain = 1;
            while (playAgain == 1) {
                // Reset Game Board
                for (int x = 0; x <=9; x++) {
                    choices[x] = Convert.ToString(x);
                }

                // Overwrite previous game result
                over = 0;

                // While game not over (1 == win/loss, -1 == draw)
                while (over == 0) {
                    // Overwrite previous selections
                    validSelection = false;

                    // Refresh console and check win condition
                    Console.Clear();
                    over = CheckWinCondition();

                    // Who's turn is it?
                    if (player == 1 || player > 2) {
                        player = 1;
                        mark = "X";
                    }
                    else {
                        mark = "O"; 
                    }

                    System.Console.WriteLine("Player {0}: {1}\n", player, mark);

                    ShowBoard();
                    if (player == 1 && over == 0) {   
                        PlayerSelection();
                        player++;
                    }
                    else if (over == 0) {
                        if (opponent != 1) {
                            AiSelection();
                            player++;
                        }
                        else {
                            PlayerSelection();
                            player++;
                        }
                    }
                }

                // Declare game over
                Console.Clear();
                System.Console.WriteLine("Game over!\n");

                // Remove numbers from winner display
                for (int i = 0; i <= 9; i++){
                    if (choices[i] != "X" && choices[i] != "O") {
                        choices[i] = " ";
                    }
                }

                // Declare winner
                ShowBoard();
                if (over == -1) {
                    System.Console.WriteLine("\nIt's a draw!");
                }
                else {
                    if (player == 3 || player == 2) player -= 1;
                    else player += 1;
                    System.Console.WriteLine("\nPlayer {0} wins!", player);
                }
                System.Console.WriteLine("\nWould you like to play again?\n1. Yes\n2. No");
                int.TryParse(Console.ReadLine(), out playAgain);
            }
        }

        private void ShowBoard() {
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[1], choices[2], choices[3]);
            System.Console.WriteLine("---|---|---");
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[4], choices[5], choices[6]);
            System.Console.WriteLine("---|---|---");
            System.Console.WriteLine(" {0} | {1} | {2} ", choices[7], choices[8], choices[9]);
        }

        private void PlayerSelection() {
            while (!validSelection) {
                System.Console.Write("\nChoose a position: ");
                if (int.TryParse(Console.ReadLine(), out choice)) {
                    validSelection = IsValidSelection(choice);
                    System.Console.WriteLine("{0} already has that spot!", choices[choice]);
                }
                else {
                    System.Console.WriteLine("That's not a number!");
                }
            }
            choices[choice] = mark;
        }

        double minimax(int depth, bool isMax) {
            int winCondition = CheckWinCondition();
            
            if (depth == 0 || winCondition != 0) {
                return winCondition;
            }

            if (isMax) {
                double maxEval = double.NegativeInfinity;
                for (int i = 1; i <=9; i++) {
                    if (IsValidSelection(i)) {
                        string placeholder = choices[i];
                        choices[i] = "O";
                        double eval = minimax(depth -1, false);
                        choices[i] = placeholder;
                        if (eval > maxEval) {
                            maxEval = eval;
                        }
                    }
                }
                return maxEval;
            }
            else {
                double minEval = double.PositiveInfinity;
                for (int i=1; i <= 9; i++) {
                    if (IsValidSelection(i)) {
                        string placeholder = choices[i];
                        choices[i] = "X";
                        double eval = minimax(depth - 1, true);
                        choices[i] = placeholder;

                        if (eval < minEval) {
                            minEval = eval;
                        }
                    }
                }
                return minEval;
            }
        }

        private void AiSelection() {
            double bestChoice = double.NegativeInfinity;
            for (int i = 1; i <= 9; i++) {
                if (IsValidSelection(i)) {
                    string placeholder = choices[i];
                    choices[i] = mark;
                    double score = minimax(5, false);
                    choices[i] = placeholder;
                    System.Console.WriteLine(score);

                    if (score > bestChoice) {
                        bestChoice = score;
                        choice = i;
                    }
                }
            }
            choices[choice] = mark;
        }

        private bool IsValidSelection(int choice) {
            if (choice != 0 && choices[choice] == Convert.ToString(choice)) {
                return true;
            }
            else {
                return false;
            }
        }

        private int CheckWinCondition() {

            // Horizontal
            if (choices[1] == choices[2] && choices[2] == choices[3] && choices[1] == "X") {
                return -2;
            }
            else if (choices[4] == choices[5] && choices[5] == choices[6] && choices[4] == "X") {
                return -2;
            }
            else if (choices[7] == choices[8] && choices[8] == choices[9] && choices[7] == "X") {
                return -2;
            }

            // Vertical
            if (choices[1] == choices[4] && choices[4] == choices[7] && choices[1] == "X") {
                return -2;
            }
            else if (choices[2] == choices[5] && choices[5] == choices[8] && choices[2] == "X") {
                return -2;
            }
            else if (choices[3] == choices[6] && choices[6] == choices[9] && choices[3] == "X") {
                return -2;
            }

            // Diagonal
            if (choices[1] == choices[5] && choices[5] == choices[9] && choices[1] == "X") {
                return -2;
            }
            else if (choices[3] == choices[5] && choices[5] == choices[7] && choices[3] == "X") {
                return -2;
            }

            // Horizontal
            if (choices[1] == choices[2] && choices[2] == choices[3] && choices[1] == "O") {
                return 2;
            }
            else if (choices[4] == choices[5] && choices[5] == choices[6] && choices[4] == "O") {
                return 2;
            }
            else if (choices[7] == choices[8] && choices[8] == choices[9] && choices[7] == "O") {
                return 2;
            }

            // Vertical
            if (choices[1] == choices[4] && choices[4] == choices[7] && choices[1] == "O") {
                return 2;
            }
            else if (choices[2] == choices[5] && choices[5] == choices[8] && choices[2] == "O") {
                return 2;
            }
            else if (choices[3] == choices[6] && choices[6] == choices[9] && choices[3] == "O") {
                return 2;
            }

            // Diagonal
            if (choices[1] == choices[5] && choices[5] == choices[9] && choices[1] == "O") {
                return 2;
            }
            else if (choices[3] == choices[5] && choices[5] == choices[7] && choices[3] == "O") {
                return 2;
            }
            // All spaces filled
            else if (choices[1] != "1" && choices[2] != "2" && choices[3] != "3" && choices[4] != "4" && choices[5] != "5" && choices[6] != "6" && choices[7] != "7" && choices[8] != "8" && choices[9] != "9") {
                return -1;
            }
            else {
                return 0;
            }
            
        }
    }
}







// private void Test() {
//             while (!validSelection) {
//                 // Horizontal
//                 // Row 1 AI
//                 if (choices[1] == mark && choices[1] == choices[2] && choices[3] == "3") {
//                     choice = 3;
//                 }
//                 else if (choices[1] == mark && choices[1] == choices[3] && choices[2] == "2") {
//                     choice = 2;
//                 }
//                 else if (choices[2] == mark && choices[2] == choices[3] && choices[1] == "1") {
//                     choice = 1;
//                 }

//                 // Row 2 AI
//                 else if (choices[4] == mark && choices[4] == choices[5] && choices[6] == "6") {
//                     choice = 6;
//                 }
//                 else if (choices[4] == mark && choices[4] == choices[6] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == mark && choices[5] == choices[6] && choices[4] == "4") {
//                     choice = 4;
//                 }
                
//                 // Row 3 AI
//                 else if (choices[7] == mark && choices[7] == choices[8] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[7] == mark && choices[7] == choices[9] && choices[8] == "8") {
//                     choice = 8;
//                 }
//                 else if (choices[8] == mark && choices[8] == choices[9] && choices[7] == "7") {
//                     choice = 7;
//                 }

//                 // Diagonal AI
//                 else if (choices[1] == mark && choices[1] == choices[5] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[1] == mark && choices[1] == choices[9] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == mark && choices[5] == choices[9] && choices[1] == "1") {
//                     choice = 1;
//                 }
//                 else if (choices[3] == mark && choices[3] == choices[5] && choices[7] == "7") {
//                     choice = 7;
//                 }
//                 else if (choices[3] == mark && choices[3] == choices[7] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == mark && choices[5] == choices[7] && choices[3] == "3") {
//                     choice = 3;
//                 }

//                 // Vertical AI
//                 else if (choices[1] == mark && choices[1] == choices[4] && choices[7] == "7") {
//                     choice = 7;
//                 }
//                 else if (choices[1] == mark && choices[1] == choices[7] && choices[4] == "4") {
//                     choice = 4;
//                 }
//                 else if (choices[4] == mark && choices[4] == choices[7] && choices[1] == "1") {
//                     choice = 1;
//                 }

//                 else if (choices[2] == mark && choices[2] == choices[5] && choices[8] == "8") {
//                     choice = 8;
//                 }
//                 else if (choices[2] == mark && choices[2] == choices[8] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == mark && choices[5] == choices[8] && choices[2] == "2") {
//                     choice = 2;
//                 }

//                 else if (choices[3] == mark && choices[3] == choices[6] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[3] == mark && choices[3] == choices[9] && choices[6] == "6") {
//                     choice = 6;
//                 }
//                 else if (choices[6] == mark && choices[6] == choices[9] && choices[3] == "3") {
//                     choice = 3;
//                 }

//                 // Horizontal
//                 // Row 1
//                 else if (choices[1] == choices[2] && choices[3] == "3") {
//                     choice = 3;
//                 }
//                 else if (choices[1] == choices[3] && choices[2] == "2") {
//                     choice = 2;
//                 }
//                 else if (choices[2] == choices[3] && choices[1] == "1") {
//                     choice = 1;
//                 }

//                 // Row 2
//                 else if (choices[4] == choices[5] && choices[6] == "6") {
//                     choice = 6;
//                 }
//                 else if (choices[4] == choices[6] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == choices[6] && choices[4] == "4") {
//                     choice = 4;
//                 }
                
//                 // Row 3
//                 else if (choices[7] == choices[8] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[7] == choices[9] && choices[8] == "8") {
//                     choice = 8;
//                 }
//                 else if (choices[8] == choices[9] && choices[7] == "7") {
//                     choice = 7;
//                 }

//                 // Diagonal 
//                 else if (choices[1] == choices[5] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[1] == choices[9] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == choices[9] && choices[1] == "1") {
//                     choice = 1;
//                 }
//                 else if (choices[3] == choices[5] && choices[7] == "7") {
//                     choice = 7;
//                 }
//                 else if (choices[3] == choices[7] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == choices[7] && choices[3] == "3") {
//                     choice = 3;
//                 }

//                 // Vertical
//                 else if (choices[1] == choices[4] && choices[7] == "7") {
//                     choice = 7;
//                 }
//                 else if (choices[1] == choices[7] && choices[4] == "4") {
//                     choice = 4;
//                 }
//                 else if (choices[4] == choices[7] && choices[1] == "1") {
//                     choice = 1;
//                 }

//                 else if (choices[2] == choices[5] && choices[8] == "8") {
//                     choice = 8;
//                 }
//                 else if (choices[2] == choices[8] && choices[5] == "5") {
//                     choice = 5;
//                 }
//                 else if (choices[5] == choices[8] && choices[2] == "2") {
//                     choice = 2;
//                 }

//                 else if (choices[3] == choices[6] && choices[9] == "9") {
//                     choice = 9;
//                 }
//                 else if (choices[3] == choices[9] && choices[6] == "6") {
//                     choice = 6;
//                 }
//                 else if (choices[6] == choices[9] && choices[3] == "3") {
//                     choice = 3;
//                 }

//                 // If no immediate winning move
//                 else {
//                     if (choices[5] == "5"){
//                         choice = 5;
//                     }
//                     else {
//                         choice = random.Next(1,9);
//                     }
//                 } 
//                 validSelection = IsValidSelection(choice);
//             }
//             choices[choice] = mark;   
//             player++;
//         }