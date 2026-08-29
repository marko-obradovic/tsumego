namespace Tsumego.Src.Game;

public class Board
{
  public int XSize { get; }
  public int YSize { get; }
  public Stone?[,] Positions { get; }
  public const int MaxBoardSize = 19;

  public Board(int xSize, int ySize)
  {
    XSize = xSize;
    YSize = ySize;

    Positions = new Stone?[XSize, YSize];

    if (XSize is < 1 or > 19)
    {
      throw new ArgumentOutOfRangeException(
          nameof(XSize),
          XSize,
          "Board width must be between 1 and 19."
          );
    }

    if (YSize is < 1 or > 19)
    {
      throw new ArgumentOutOfRangeException(
          nameof(YSize),
          YSize,
          "Board height must be between 1 and 19."
          );
    }
  }

  public bool PlayMove(int x, int y, Stone stone)
  {
    Positions[x - 1 , YSize - y] = stone;
    return true;
  }

  public void PrintBoard()
  {
    Console.WriteLine($"Board Size: {XSize} x {YSize}\n------");

    for (int y = 0; y < YSize; y++)
    {
      for (int x = 0; x < XSize; x++)
      {
        PrintColumnCell(x, y);
      }
      Console.WriteLine();
    }
    PrintRowLetters();
  }

  public void PrintColumnCell(int x, int y)
  {
    // If we're on the first row, first print the number of the row
    if (x == 0)
    {
      // If it's a single digit number, push up the number and dots so the full board aligns properly
      if (y + 1 < 10)
      {
        Console.Write(" ");
      }

      Console.Write(YSize - y);
    }

    if (Positions[x, y] is null)
    {
      Console.Write(" ·");
    }
    else
    {
      Console.Write(Positions[x, y]!.Color == Color.Black ? " ●" : " ○");
    }
  }

  private void PrintRowLetters()
  {
    char[] alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];

    Console.Write("  ");

    for (int x = 0; x < XSize; x++)
    {
      Console.Write($" {alphabet[x]}");
    }
    Console.WriteLine();
  }
}
