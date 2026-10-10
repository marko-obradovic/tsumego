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
    // Validate move
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

    // Place stone
    Positions[position.X, position.Y] = stone;

    List<Coordinate> adjacentPositions = GetAdjacentPositions(position);

    PrintAdjacentPositions(position, adjacentPositions);

    // Capture enemy groups
    foreach (var adjacentPosition in adjacentPositions)
    {
      var adjacentStone = Positions[adjacentPosition.X, adjacentPosition.Y];

      // Empty position
      if (adjacentStone is null)
      {
        continue;
      }

      // Friendly stone
      if (adjacentStone.Color == stone.Color)
      {
        continue;
      }

      var enemyStoneGroup = GetStoneGroup(adjacentPosition);
    }

    return true;
  }

  private List<Coordinate> GetStoneGroup(Coordinate start)
  {
    var stoneGroup = new List<Coordinate>(); 
    var toVisit = new Queue<Coordinate>();
    var visited = new HashSet<Coordinate>();

    var stone = Positions[start.X, start.Y];

    if (stone is null)
    {
      return stoneGroup;
    }

    toVisit.Enqueue(start);
    visited.Add(start);

    while(toVisit.Count > 0)
    {
      var current = toVisit.Dequeue();

      stoneGroup.Add(current);

      foreach (var adjacent in GetAdjacentPositions(current))
      {
        if (visited.Contains(adjacent))
        {
          continue;
        }

        var adjacentStone = Positions[adjacent.X, adjacent.Y];

        if (adjacentStone is null)
        {
          continue;
        }

        visited.Add(adjacent);
        toVisit.Enqueue(adjacent);
      }
    }

    return stoneGroup;
  }

  private List<Coordinate> GetAdjacentPositions(Coordinate position)
  {
    var adjacentPositions = new List<Coordinate>
    {
      new Coordinate(position.X + 1, position.Y),
      new Coordinate(position.X - 1, position.Y),
      new Coordinate(position.X, position.Y + 1),
      new Coordinate(position.X, position.Y - 1)
    };

    return adjacentPositions
      .Where(IsOnBoard)
      .ToList();
  }

// ----------------------------------------- //
// -------- CLI Board Visualization -------- //
// ----------------------------------------- //

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

  public void PrintAdjacentPositions(Coordinate position, List<Coordinate> adjacentPositions)
  {
    Console.WriteLine($"Adjacent positions for {position}");
    for (int i = 0; i < adjacentPositions.Count; i++)
    {
      Console.WriteLine(adjacentPositions[i]);
    }
  }

  public void PrintColumnCell(Coordinate position)
  {
    // If we're on the first row, first print the number of the row
    if (position.X == 0)
    {
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
    Console.Write(" ");

    for (int x = 0; x < XSize; x++)
    {
      Console.Write($" {GetLetter(x)}");
    }
    Console.WriteLine();
  }
}
