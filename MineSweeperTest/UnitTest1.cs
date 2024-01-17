[TestClass]
public class MinesweeperTests
{
    [TestMethod]
    public void TestInitializeGrid()
    {
        Console.Write("TestInitializeGrid");
        int size = 3;

        var grid = Minesweeper.InitializeGrid(size);

        Assert.AreEqual(size, grid.GetLength(0));
        Assert.AreEqual(size, grid.GetLength(1));
    }

    [TestMethod]
    public void TestInsertMines()
    {
        Console.Write("TestInsertMines");
        int size = 3;
        int mineCount = 2;

        var mines = Minesweeper.InsertMines(size, mineCount);

        int count = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (mines[i, j] == '*')
                {
                    count++;
                }
            }
        }
        Assert.AreEqual(mineCount, count);
    }

    [TestMethod]
    public void TestGetAdjacentMines()
    {
        Console.Write("TestGetAdjacentMines");
        char[,] mines = { { '*', '_', '_' }, { '_', '*', '_' }, { '_', '_', '*' } };
        int row = 1;
        int col = 1;

        int adjacentMines = Minesweeper.GetAdjacentMines(mines, row, col);

        Assert.AreEqual(3, adjacentMines);
    }

    [TestMethod]
    public void TestDisplayeGrid()
    {
        Console.Write("TestDisplayGrid");
        char[,] grid = { { '_', '_', '_' }, { '_', '_', '_' }, { '_', '_', '_' } };

        Minesweeper.DisplayGrid(grid);

    }
}