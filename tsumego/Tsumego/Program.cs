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

board.PlayMove(5, 5, blackStone);
board.PlayMove(5, 4, whiteStone);
board.PlayMove(3, 3, blackStone);
board.PlayMove(5, 6, whiteStone);
board.PlayMove(3, 7, blackStone);
board.PlayMove(4, 5, whiteStone);
board.PlayMove(7, 3, blackStone);
board.PlayMove(6, 5, whiteStone);
// incorrect move:
board.PlayMove(6, 5, blackStone);

board.PrintBoard();

// check if move is legal - do this inside the PlayMove() method
//    - KO - infinite loop
//    - is there already a stone there
//    - you cannot place a stone in the middle of 4 stones
//      - you can if the stone has no liberties (then its a capture)
// place stone (via playmove)
// capture stones
//  - should be able to capture on edges
