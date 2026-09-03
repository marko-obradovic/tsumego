namespace Tsumego.Src.Game;

public readonly record struct Coordinate(int X, int Y)
{
  public static Coordinate ConvertSgfToIndexCoordinate(string value)
  {
    return new Coordinate(
        value[0] - 'a',
        value[1] - 'a'
        );
  }
}

