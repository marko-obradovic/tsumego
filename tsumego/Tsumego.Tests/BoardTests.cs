namespace Tsumego.Tests;

using Tsumego.Src.Game;

public class BoardTests
{
    [Fact]
    public void NewBoard_HasCorrectDimensions()
    {
      var board = new Board(9, 9);

      Assert.Equal(9, board.XSize);
      Assert.Equal(9, board.YSize);
    }
}
