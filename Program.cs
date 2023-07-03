using System;

namespace SudokuProject                          
{
    class SudokuBoard
    {
        private int[,] board;

        public void Initialize()
        {
           
            board = new int[,]
            {
                {5, 3, 0, 0, 7, 0, 0, 0, 0},
                {6, 0, 0, 1, 9, 5, 0, 0, 0},
                {0, 9, 8, 0, 0, 0, 0, 6, 0},
                {8, 0, 0, 0, 6, 0, 0, 0, 3},
                {4, 0, 0, 8, 0, 3, 0, 0, 1},
                {7, 0, 0, 0, 2, 0, 0, 0, 6},
                {0, 6, 0, 0, 0, 0, 2, 8, 0},
                {0, 0, 0, 4, 1, 9, 0, 0, 5},
                {0, 0, 0, 0, 8, 0, 0, 7, 9}
            };
        }
        public void InitializeRandom()
        {
            int[,] solvedBoard = new int[,]
            {
                {5, 3, 4, 6, 7, 8, 9, 1, 2},
                {6, 7, 2, 1, 9, 5, 3, 4, 8},
                {1, 9, 8, 3, 4, 2, 5, 6, 7},
                {8, 5, 9, 7, 6, 1, 4, 2, 3},
                {4, 2, 6, 8, 5, 3, 7, 9, 1},
                {7, 1, 3, 9, 2, 4, 8, 5, 6},
                {9, 6, 1, 5, 3, 7, 2, 8, 4},
                {2, 8, 7, 4, 1, 9, 6, 3, 5},
                {3, 4, 5, 2, 8, 6, 1, 7, 9}
            };

            Random random = new Random();
            board = new int[9, 9];

            
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (random.Next(2) == 0) 
                        board[row, col] = solvedBoard[row, col];
                    else
                        board[row, col] = 0;
                }
            }
        }

        public void Display()
        {
            Console.WriteLine("Sudoku Board:");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool IsValidMove(int row, int col, int value)
        {
            // Check if the move is valid by ensuring the value doesn't already exist in the row, column, or 3x3 grid
            return !IsInRow(row, value) && !IsInColumn(col, value) && !IsInGrid(row, col, value);
        }

        private bool IsInRow(int row, int value)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == value)
                    return true;
            }
            return false;
        }

        private bool IsInColumn(int col, int value)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row, col] == value)
                    return true;
            }
            return false;
        }

        private bool IsInGrid(int row, int col, int value)
        {
            int gridRow = (row / 3) * 3;
            int gridCol = (col / 3) * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[gridRow + i, gridCol + j] == value)
                        return true;
                }
            }
            return false;
        }

        public void MakeMove(int row, int col, int value)
        {
            board[row, col] = value;
        }

        public bool IsSolved()
        {
            // Check if the Sudoku board is fully solved
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return false;
                }
            }
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SudokuBoard board = new SudokuBoard();
            // board.Initialize();
            board.InitializeRandom();
            board.Display();

            while (!board.IsSolved())
            {
                Console.WriteLine("Enter the row (1-9), column (1-9), and value (1-9) separated by spaces (e.g., 3 5 7):");
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length != 3)
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                int row, col, value;
                if (!int.TryParse(input[0], out row) || !int.TryParse(input[1], out col) || !int.TryParse(input[2], out value))
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                if (!board.IsValidMove(row - 1, col - 1, value))
                {
                    Console.WriteLine("Invalid move! Please try again.");
                    continue;
                }

                board.MakeMove(row - 1, col - 1, value);
                board.Display();
            }

            Console.WriteLine("Congratulations! Sudoku solved.");
            Console.ReadLine();
        }
    }

    
}
