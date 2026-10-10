namespace Tsumego.Src.CliGraphics;

using Tsumego.Src.Game;

public class CliGraphics
{
  private readonly Board _board;

  public CliGraphics(Board board)
  {
    _board = board;
  }

  public void PrintBoard()
  {
    PrintRowLetters();

    for (int y = 0; y < _board.YSize; y++)
    {
      for (int x = 0; x < _board.XSize; x++)
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
      Console.Write(Board.GetLetter(position.Y));
    }

    if (_board.Positions[position.X, position.Y] is null)
    {
      Console.Write(" ·");
    }
    else
    {
      Console.Write(_board.Positions[position.X, position.Y]!.Color == Color.Black ? " ○" : " ●");
    }
  }

  private void PrintRowLetters()
  {
    Console.Write(" ");

    for (int x = 0; x < _board.XSize; x++)
    {
      Console.Write($" {Board.GetLetter(x)}");
    }
    Console.WriteLine();
  }
}

