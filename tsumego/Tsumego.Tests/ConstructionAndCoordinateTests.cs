namespace Tsumego.Tests;

using Tsumego.Src.Game;

public class ConstructionAndCoordinateTests
{
    [Fact]
    public void NewBoard_HasCorrectDimensions()
    {
        var board = new Board(9, 13);

        Assert.Equal(9, board.XSize);
        Assert.Equal(13, board.YSize);
    }

    [Fact]
    public void NewBoard_StartsEmpty()
    {
        var board = new Board(9, 9);

        foreach (var position in board.Positions)
        {
            Assert.Null(position);
        }
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(9, 9)]
    [InlineData(19, 19)]
    [InlineData(9, 19)]
    public void NewBoard_ValidDimensions_Succeeds(int x, int y)
    {
        var board = new Board(x, y);

        Assert.Equal(x, board.XSize);
        Assert.Equal(y, board.YSize);
    }

    [Theory]
    [InlineData(0, 9)]
    [InlineData(20, 9)]
    public void NewBoard_InvalidWidth_Throws(int x, int y)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Board(x, y));

        Assert.Equal("xSize", exception.ParamName);
    }

    [Theory]
    [InlineData(9, 0)]
    [InlineData(9, 20)]
    public void NewBoard_InvalidHeight_Throws(int x, int y)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Board(x, y));

        Assert.Equal("ySize", exception.ParamName);
    }

    [Theory]
    [InlineData(0, 'a')]
    [InlineData(1, 'b')]
    [InlineData(25, 'z')]
    public void GetLetter_ValidIndex_ReturnsExpectedLetter(
        int index, char expected)
    {
        Assert.Equal(expected, Board.GetLetter(index));
    }
}

