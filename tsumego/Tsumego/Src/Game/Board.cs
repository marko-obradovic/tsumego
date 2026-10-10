namespace Tsumego.Src.Game;

public class Board
{
  public int XSize { get; }
  public int YSize { get; }
  public Stone?[,] Positions { get; }
  public const int MaxBoardSize = 19;
  private Stone?[,]? _koForbiddenPosition;

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

  private Stone?[,] ClonePosition()
  {
    return (Stone?[,])Positions.Clone();
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

    // Save the board before the move
    var positionBeforeMove = ClonePosition();

    // Place stone
    Positions[position.X, position.Y] = stone;

    List<Coordinate> adjacentPositions = GetAdjacentPositions(position);

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

      if (!HasLiberty(enemyStoneGroup))
      {
        RemoveStoneGroup(enemyStoneGroup);
      }

    }
    
    // Check for ko
    if (_koForbiddenPosition is not null &&
        PositionsAreEqual(Positions, _koForbiddenPosition))
    {
      RestorePosition(positionBeforeMove);
      Console.WriteLine("Illegal move. Ko rule prevents this repitition.");
      return false;
    }
    
    // Check for Suicide
    var ownStoneGroup = GetStoneGroup(position);

    if (!HasLiberty(ownStoneGroup))
    {
      RestorePosition(positionBeforeMove);
      Console.WriteLine("Illegal move. Stone would have no liberties");
      return false;
    }

    // The move should be legal by this point.
    // Save the previous board state as the position
    // that the next move cannot recreate.
    _koForbiddenPosition = positionBeforeMove;

    return true;
  }

  private void RemoveStoneGroup(List<Coordinate> stoneGroup)
  {
    foreach (var position in stoneGroup)
    {
      Positions[position.X, position.Y] = null;
    }
  }

  private bool HasLiberty(List<Coordinate> stoneGroup)
  {
    foreach (var position in stoneGroup)
    {
      foreach (var adjacent in GetAdjacentPositions(position))
      {
        if (Positions[adjacent.X, adjacent.Y] is null)
        {
          return true;
        }
      }
    }

    return false;
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

        if (adjacentStone.Color != stone.Color)
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

  private bool PositionsAreEqual(Stone?[,] first, Stone?[,] second)
  {
    for (int x = 0; x < XSize; x++)
    {
      for (int y = 0; y < YSize; y++)
      {
        if (first[x, y]?.Color != second[x, y]?.Color)
        {
          return false;
        }
      }
    }
    return true;
  }

  private void RestorePosition(Stone?[,] snapshot)
  {
    Array.Copy(snapshot, Positions, Positions.Length);
  }
}
