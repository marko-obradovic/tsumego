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
var stone = new Stone(Color.Black);
var stone2 = new Stone(Color.White);
var stone3 = new Stone(Color.Black);
var stone4 = new Stone(Color.White);
var stone5 = new Stone(Color.Black);
var stone6 = new Stone(Color.White);
var stone7 = new Stone(Color.Black);
var stone8 = new Stone(Color.White);

board.PlayMove(5, 5, stone);
board.PlayMove(5, 4, stone2);
board.PlayMove(3, 3, stone3);
board.PlayMove(5, 6, stone4);
board.PlayMove(3, 7, stone5);
board.PlayMove(4, 5, stone6);
board.PlayMove(7, 3, stone7);
board.PlayMove(6, 5, stone8);

board.PrintBoard();

// check if move is legal - do this inside the PlayMove() method
//    - KO - infinite loop
//    - is there already a stone there
//    - you cannot place a stone in the middle of 4 stones
//      - you can if the stone has no liberties (then its a capture)
// place stone (via playmove)
// capture stones
//  - should be able to capture on edges
