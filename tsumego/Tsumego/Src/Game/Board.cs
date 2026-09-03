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

    if (xSize is < 1 or > MaxBoardSize)
    {
      throw new ArgumentOutOfRangeException(
          nameof(xSize),
          xSize,
          $"Board width must be between 1 and {MaxBoardSize}."
          );
    }

    if (ySize is < 1 or > MaxBoardSize)
    {
      throw new ArgumentOutOfRangeException(
          nameof(ySize),
          ySize,
          $"Board height must be between 1 and {MaxBoardSize}."
          );
    }
  }

  private bool IsOnBoard(Coordinate position)
  {
    return position.X >= 0 &&
           position.X < XSize &&
           position.Y >= 0 &&
           position.Y < YSize;
  }

  public static char GetLetter(int index)
  {
    return (char)('a' + index);
  }

  public bool PlayMove(Coordinate position, Stone stone)
  {
    if (!IsOnBoard(position))
    {
      Console.WriteLine("Illegal move. Piece cannot be placed off the board.");
      return false;
    }

    if (Positions[position.X, position.Y] is not null)
    {
      Console.WriteLine($"Illegal move. A stone already exists at ({GetLetter(position.X)},{GetLetter(position.Y)})");
      return false;
    }

    Positions[position.X, position.Y] = stone;
    return true;
  }

  public void PrintBoard()
  {
    Console.WriteLine($"Board Size: {XSize} x {YSize}\n------");

    PrintRowLetters();

    for (int y = 0; y < YSize; y++)
    {
      for (int x = 0; x < XSize; x++)
      {
        PrintColumnCell(new Coordinate(x, y));
      }
      Console.WriteLine();
    }
  }

  public void PrintColumnCell(Coordinate position)
  {
    // If we're on the first row, first print the number of the row
    if (position.X == 0)
    {
      // If it's a single digit number, push up the number and dots so the full board aligns properly
      if ((position.Y) < 10)
      {
        Console.Write(" ");
      }

      Console.Write(GetLetter(position.Y));
    }

    if (Positions[position.X, position.Y] is null)
    {
      Console.Write(" ·");
    }
    else
    {
      Console.Write(Positions[position.X, position.Y]!.Color == Color.Black ? " ●" : " ○");
    }
  }

  private void PrintRowLetters()
  {
    Console.Write("  ");

    for (int x = 0; x < XSize; x++)
    {
      Console.Write($" {GetLetter(x)}");
    }
    Console.WriteLine();
  }
}
