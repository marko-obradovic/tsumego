namespace Tsumego.Tests;

using Tsumego.Src.Game;

public class BasicMoveTests
{
  [Fact]
  public void PlayMove_SingleCapture_IsLegal()
  {
    var board = new Board(3, 3);

    // Black stones surround the centre on three sides.
    board.PlayMove(new Coordinate(1, 0), new Stone(Color.Black));
    board.PlayMove(new Coordinate(0, 1), new Stone(Color.Black));
    board.PlayMove(new Coordinate(2, 1), new Stone(Color.Black));

    // White at (1, 1) has one liberty: (1, 2).
    board.PlayMove(new Coordinate(1, 1), new Stone(Color.White));

    // Black fills that liberty, capturing White.
    var result = board.PlayMove(
        new Coordinate(1, 2), new Stone(Color.Black));

    Assert.True(result);
    Assert.Null(board.Positions[1, 1]);

    // Check if all black stones are there
    Assert.Equal(Color.Black, board.Positions[1, 0]?.Color);
    Assert.Equal(Color.Black, board.Positions[0, 1]?.Color);
    Assert.Equal(Color.Black, board.Positions[2, 1]?.Color);
    Assert.Equal(Color.Black, board.Positions[1, 2]?.Color);
  }

  [Fact]
  public void PlayMove_LargeMultiCapture_IsLegal()
  {
    var board = new Board(9, 9);

    // Black fills up a diamond-shaped space
    board.PlayMove(new Coordinate(4, 4), new Stone(Color.Black));
    board.PlayMove(new Coordinate(4, 3), new Stone(Color.Black));
    board.PlayMove(new Coordinate(4, 5), new Stone(Color.Black));
    board.PlayMove(new Coordinate(3, 4), new Stone(Color.Black));
    board.PlayMove(new Coordinate(5, 4), new Stone(Color.Black));

    // White surrounds black diamond shape
    board.PlayMove(new Coordinate(2, 4), new Stone(Color.White));
    board.PlayMove(new Coordinate(3, 3), new Stone(Color.White));
    board.PlayMove(new Coordinate(4, 2), new Stone(Color.White));
    board.PlayMove(new Coordinate(5, 3), new Stone(Color.White));
    board.PlayMove(new Coordinate(6, 4), new Stone(Color.White));
    board.PlayMove(new Coordinate(5, 5), new Stone(Color.White));
    board.PlayMove(new Coordinate(4, 6), new Stone(Color.White));
    board.PlayMove(new Coordinate(3, 5), new Stone(Color.White));

    // The spots where black was should be empty
    Assert.Null(board.Positions[4, 4]);
    Assert.Null(board.Positions[4, 3]);
    Assert.Null(board.Positions[4, 5]);
    Assert.Null(board.Positions[3, 4]);
    Assert.Null(board.Positions[5, 4]);

    // The spots where white was should be populated with white stones
    Assert.Equal(Color.White, board.Positions[2, 4]?.Color);
    Assert.Equal(Color.White, board.Positions[3, 3]?.Color);
    Assert.Equal(Color.White, board.Positions[4, 2]?.Color);
    Assert.Equal(Color.White, board.Positions[5, 3]?.Color);
    Assert.Equal(Color.White, board.Positions[6, 4]?.Color);
    Assert.Equal(Color.White, board.Positions[5, 5]?.Color);
    Assert.Equal(Color.White, board.Positions[4, 6]?.Color);
    Assert.Equal(Color.White, board.Positions[3, 5]?.Color);
  }

  [Fact]
  public void PlayMove_Suicide_ReturnsFalseAndRestoresBoard()
  {
    var board = new Board(3, 3);

    // White surrounds (1, 1)
    board.PlayMove(new Coordinate(1, 0), new Stone(Color.White));
    board.PlayMove(new Coordinate(0, 1), new Stone(Color.White));
    board.PlayMove(new Coordinate(2, 1), new Stone(Color.White));
    board.PlayMove(new Coordinate(1, 2), new Stone(Color.White));

    // Try to place black stone in the middle of white stones
    var result = board.PlayMove(
        new Coordinate(1, 1), new Stone(Color.Black));

    Assert.False(result);
    Assert.Null(board.Positions[1, 1]);

    Assert.Equal(Color.White, board.Positions[1, 0]?.Color);
    Assert.Equal(Color.White, board.Positions[0, 1]?.Color);
    Assert.Equal(Color.White, board.Positions[2, 1]?.Color);
    Assert.Equal(Color.White, board.Positions[1, 2]?.Color);
  }

  [Fact]
  public void PlayMove_SuicideThatCapturesEnemy_IsLegal()
  {
    var board = new Board(3, 4);

    // Black surrounds (1, 1)
    board.PlayMove(new Coordinate(1, 0), new Stone(Color.Black));
    board.PlayMove(new Coordinate(0, 1), new Stone(Color.Black));
    board.PlayMove(new Coordinate(2, 1), new Stone(Color.Black));
    board.PlayMove(new Coordinate(1, 2), new Stone(Color.Black));

    // White surrounds (1, 2)
    board.PlayMove(new Coordinate(0, 2), new Stone(Color.White));
    board.PlayMove(new Coordinate(1, 3), new Stone(Color.White));
    board.PlayMove(new Coordinate(2, 2), new Stone(Color.White));

    // White takes Black's stone at (1, 2)
    var result = board.PlayMove(new Coordinate(1, 1), new Stone(Color.White));

    Assert.True(result);
    Assert.Equal(Color.White, board.Positions[1, 1]?.Color);
    Assert.Null(board.Positions[1, 2]?.Color);
  }

  [Fact]
  public void PlayMove_KoIsIllegal()
  {
    var board = new Board(3, 4);

    // Black surrounds (1, 1)
    board.PlayMove(new Coordinate(1, 0), new Stone(Color.Black));
    board.PlayMove(new Coordinate(0, 1), new Stone(Color.Black));
    board.PlayMove(new Coordinate(2, 1), new Stone(Color.Black));
    board.PlayMove(new Coordinate(1, 2), new Stone(Color.Black));

    // White surrounds (1, 2)
    board.PlayMove(new Coordinate(0, 2), new Stone(Color.White));
    board.PlayMove(new Coordinate(1, 3), new Stone(Color.White));
    board.PlayMove(new Coordinate(2, 2), new Stone(Color.White));
    // Placing, and checking final white piece returns true
    Assert.True(board.PlayMove(new Coordinate(1, 1), new Stone(Color.White)));

    Assert.Null(board.Positions[1, 2]);

    // Black attempts to play (1, 2), fails because of ko rule
    var result = board.PlayMove(new Coordinate(1, 2), new Stone(Color.Black));

    Assert.False(result);

    // Checking to see if it's still empty after black tries to re-capture
    Assert.Null(board.Positions[1, 2]);

    // Checking if all white pieces are present
    Assert.Equal(Color.White, board.Positions[0, 2]?.Color);
    Assert.Equal(Color.White, board.Positions[1, 3]?.Color);
    Assert.Equal(Color.White, board.Positions[2, 2]?.Color);
    Assert.Equal(Color.White, board.Positions[1, 1]?.Color);
  }
}

