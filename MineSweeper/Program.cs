public class Minesweeper
{
    static void Main()
    {
        Console.WriteLine("Welcome to Minesweeper !");
        Console.WriteLine();

        int gridSize;
        bool updatedGrid = false;
        bool invalidMove = false;
        while (true)
        {
            Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid):");
            // Gets input from the user for the grid size while also checking if the inputted value is an integer
            if (!int.TryParse(Console.ReadLine(), out gridSize))
            {
                Console.WriteLine("Incorrect input.");
            }
            // Checking if the inputted grid size is not less than the minimum size which is 2
            else if (gridSize < 2)
            {
                Console.WriteLine("Minimum size of grid is 2.");
            }
            // Checking if the inputted grid size is not more than the maximum size which is 10
            else if (gridSize > 10)
            {
                Console.WriteLine("Maximum size of grid is 10.");
            }
            else if (gridSize >= 2 && gridSize <= 10)
            {
                break;
            }
        }

        // Getting the 35% of the total squares of the inputted grid size of the user
        int maxMineCount = (int)(0.35 * gridSize * gridSize);
        int mineCount;
        while (true)
        {
            Console.WriteLine("Enter the number of mines to display on the grid (maximum is 35% of the total squares):");
            // Gets input from the user for the number of mines while also checking if the inputted value is an integer
            if (!int.TryParse(Console.ReadLine(), out mineCount))
            {
                Console.WriteLine("Incorrect input.");
            }
            // Checking if the inputted number of mines is not less than 1
            else if (mineCount < 1)
            {
                Console.WriteLine("There must be at least 1 mine.");
            }
            // Checking if the inputted number of mines is not more than the 35% of the total squares
            else if (mineCount > maxMineCount)
            {
                Console.WriteLine("Maximum number is 35% of total squares.");
            }
            else if (mineCount >= 1 && mineCount <= maxMineCount)
            {
                break;
            }
        }

        char[,] grid = InitializeGrid(gridSize);
        char[,] mines = InsertMines(gridSize, mineCount);

        // Calculate the number of moves allowed based on the inputted number of mines and grid size
        int movesLeft = (gridSize * gridSize) - mineCount;

        // Loop until the user wins or loses the game
        while (true)
        {
            // If the input is incorrect/invalid, grid will not be displayed
            if (!invalidMove)
            {
                Console.WriteLine();
                // If the grid has already been updated, system will display ""Here is your minefield:". Otherwise, it will display "Here is your updated minefield:"
                if (!updatedGrid)
                {
                    updatedGrid = true;
                    Console.WriteLine("Here is your minefield:");
                }
                else
                {
                    Console.WriteLine("Here is your updated minefield:");
                }
                DisplayGrid(grid);
            }

            // If there is no more moves left, it means that the user just won the game
            if (movesLeft == 0)
            {
                Console.WriteLine("Congratulations, you have won the game!");
                break;
            }

            Console.Write("Select a square to reveal (e.g. A1): ");
            string input = Console.ReadLine().ToUpper();

            // Checking if the inputted value is valid
            if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
            {
                Console.WriteLine("Incorrect input.");
                invalidMove = true;
                continue;
            }

            int row = input[0] - 'A';
            int col = input[1] - '1';

            // Checking if the inputted value is a valid coordinate
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                Console.WriteLine("Invalid move. Please enter valid coordinates.");
                invalidMove = true;
                continue;
            }

            // Checking if the inputted value has an equivalent mine in mines[,] variable
            if (mines[row, col] == '*')
            {
                Console.WriteLine("Oh no, you detonated a mine! Game over.");
                break;
            }

            // Checking if the inputted value has been previously inputted already
            if (grid[row, col] != '_')
            {
                Console.WriteLine("Invalid move. Cell has already been revealed.");
                invalidMove = true;
                continue;
            }

            int adjacentMines = GetAdjacentMines(mines, row, col);
            Console.WriteLine("This square contains " + adjacentMines + " adjacent mines.");
            grid[row, col] = (char)(adjacentMines + '0');

            invalidMove = false;
            movesLeft--;
        }
    }

    public static char[,] InitializeGrid(int size)
    {
        char[,] grid = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = '_';
            }
        }

        return grid;
    }

    // Insert '*' values in mines[,] variable randomly which will serve as mines to the grid
    public static char[,] InsertMines(int size, int mineCount)
    {
        char[,] mines = new char[size, size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                mines[i, j] = '-';
            }
        }

        int minesInserted = 0;
        while (minesInserted < mineCount)
        {
            int row = random.Next(size);
            int col = random.Next(size);

            if (mines[row, col] != '*')
            {
                mines[row, col] = '*';
                minesInserted++;
            }
        }

        return mines;
    }

    // Prints the grid with the latest update (if any)
    public static void DisplayGrid(char[,] grid)
    {
        int size = grid.GetLength(0);
        Console.Write("  ");
        for (int i = 0; i < size; i++)
        {
            Console.Write((i + 1) + " ");
        }
        Console.WriteLine();

        for (int i = 0; i < size; i++)
        {
            Console.Write((char)('A' + i) + " ");

            for (int j = 0; j < size; j++)
            {
                Console.Write(grid[i, j] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    // Gets the number of adjacent mines of the inputted coordinate
    public static int GetAdjacentMines(char[,] mines, int row, int col)
    {
        int count = 0;
        int size = mines.GetLength(0);

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < size && j >= 0 && j < size && mines[i, j] == '*')
                {
                    count++;
                }
            }
        }

        return count;
    }
}
