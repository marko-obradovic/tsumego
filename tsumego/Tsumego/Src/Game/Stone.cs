namespace Tsumego.Src.Game;

public class Stone
{
  public Color Color { get; }

  public Stone(Color color)
  {
    Color = color;
  }
}

public enum Color
{
  Black,
  White
}
