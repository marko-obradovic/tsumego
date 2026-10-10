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

void PlayMovePrintAndPause(string letterCoordinates, Stone stone)
{
  board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate(letterCoordinates), stone);
  board.PrintBoard();
  System.Threading.Thread.Sleep(300);
}

PlayMovePrintAndPause("ee", blackStone);
PlayMovePrintAndPause("ed", whiteStone);
PlayMovePrintAndPause("cc", blackStone);
PlayMovePrintAndPause("ef", whiteStone);
PlayMovePrintAndPause("cg", blackStone);
PlayMovePrintAndPause("de", whiteStone);
PlayMovePrintAndPause("gc", blackStone);
PlayMovePrintAndPause("fe", whiteStone);

// incorrect move:
PlayMovePrintAndPause("fe", blackStone);
// Corner move
PlayMovePrintAndPause("aa", whiteStone);
// Edge move
PlayMovePrintAndPause("ad", blackStone);
