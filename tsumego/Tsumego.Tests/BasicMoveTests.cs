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

    // Check if all correct stones are there
    Assert.Equal(Color.Black, board.Positions[1, 2]?.Color);
    Assert.Equal(Color.Black, board.Positions[1, 2]?.Color);
  }

 // [Fact]
 // public void PlayMove_LargeMultiCapture_IsLegal()
 // {
 //   var board = new Board(3, 3);

 //   // Black stones surround the centre on three sides.
 //   board.PlayMove(new Coordinate(1, 0), new Stone(Color.Black));
 //   board.PlayMove(new Coordinate(0, 1), new Stone(Color.Black));
 //   board.PlayMove(new Coordinate(2, 1), new Stone(Color.Black));

 //   // White at (1, 1) has one liberty: (1, 2).
 //   board.PlayMove(new Coordinate(1, 1), new Stone(Color.White));

 //   // Black fills that liberty, capturing White.
 //   var result = board.PlayMove(
 //       new Coordinate(1, 2), new Stone(Color.Black));

 //   Assert.True(result);
 //   Assert.Null(board.Positions[1, 1]);
 //   Assert.Equal(Color.Black, board.Positions[1, 2]?.Color);
 // }

  [Fact]
  public void PlayMove_Suicide_ReturnsFalseAdRestoresBoard()
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
    board.PlayMove(new Coordinate(1, 1), new Stone(Color.White));

    // Black attempts to play (1, 2), fails because of ko rule
    var result = board.PlayMove(new Coordinate(1, 2), new Stone(Color.Black));

    Assert.False(result);
  }
}

