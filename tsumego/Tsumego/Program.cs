using System.Linq;
using Tsumego.Src.SgfParser;
using Tsumego.Src.Game;

SgfTree tree = SgfParser.ParseTree("(;GM[1]FF[4]SZ[9];B[cc];W[dd](;B[ee])(;B[ff]))");

void PrintTree(SgfTree tree)
{
  foreach (var item in tree.Data)
  {
    Console.WriteLine($"Key: {item.Key}");

    foreach (var value in item.Value)
    {
      Console.WriteLine($"  Value: {value}");
    }
  }
}

// PrintTree(tree);

var board = new Board(9, 9);
var blackStone = new Stone(Color.Black);
var whiteStone = new Stone(Color.White);

// Should be out of bounds
board.PlayMove(new Coordinate(19, 19), blackStone);

board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("ee"), blackStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("ed"), whiteStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("cc"), blackStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("ef"), whiteStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("cg"), blackStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("de"), whiteStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("gc"), blackStone);
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("fe"), whiteStone);
// incorrect move:
board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate("fe"), blackStone);

board.PrintBoard();

// capture stones
//  - Check if legal then capture
//    - Ko - prevent infinite loop
//    - you cannot place a stone in the middle of 4 stones - call it IsSuicideMove()
//  - should be able to capture on edges
//      - you can if the stone has no liberties (then its a capture)
