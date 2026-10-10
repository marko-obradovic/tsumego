using System.Linq;
using Tsumego.Src.SgfParser;
using Tsumego.Src.Game;
using Tsumego.Src.CliGraphics;

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
var cliGraphics = new CliGraphics(board);

Console.WriteLine($"Board Size: {board.XSize} x {board.YSize}\n------");

var blackStone = new Stone(Color.Black);
var whiteStone = new Stone(Color.White);

void PlayMovePrintAndPause(string letterCoordinates, Stone stone)
{
  board.PlayMove(Coordinate.ConvertSgfToIndexCoordinate(letterCoordinates), stone);
  cliGraphics.PrintBoard();
  Console.WriteLine("----------------------");
  System.Threading.Thread.Sleep(500);
}

void GenericPlacementTest()
{
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
}

void LargeCaptureTest()
{
  PlayMovePrintAndPause("ee", blackStone);
  PlayMovePrintAndPause("ce", whiteStone);

  PlayMovePrintAndPause("ed", blackStone);
  PlayMovePrintAndPause("dd", whiteStone);

  PlayMovePrintAndPause("ef", blackStone);
  PlayMovePrintAndPause("ec", whiteStone);

  PlayMovePrintAndPause("de", blackStone);
  PlayMovePrintAndPause("fd", whiteStone);

  PlayMovePrintAndPause("fe", blackStone);
  PlayMovePrintAndPause("ge", whiteStone);

  PlayMovePrintAndPause("ff", whiteStone);
  PlayMovePrintAndPause("eg", whiteStone);
  PlayMovePrintAndPause("df", whiteStone);
}

void SmallCornerCaptureTest()
{
  PlayMovePrintAndPause("hi", blackStone);
  PlayMovePrintAndPause("ii", whiteStone);
  PlayMovePrintAndPause("ih", blackStone);
}

void MediumCornerSuicidePreventionTest()
{
  PlayMovePrintAndPause("gi", blackStone);
  PlayMovePrintAndPause("hi", whiteStone);
  PlayMovePrintAndPause("hh", blackStone);
  PlayMovePrintAndPause("ih", whiteStone);
  PlayMovePrintAndPause("ig", blackStone);
  PlayMovePrintAndPause("ii", whiteStone);
}

void MediumCornerCaptureTest()
{
  PlayMovePrintAndPause("hi", whiteStone);
  PlayMovePrintAndPause("ih", whiteStone);
  PlayMovePrintAndPause("ii", whiteStone);
  PlayMovePrintAndPause("gi", blackStone);
  PlayMovePrintAndPause("hh", blackStone);
  PlayMovePrintAndPause("ig", blackStone);
}

void SmallKoTest()
{
  PlayMovePrintAndPause("ed", whiteStone);
  PlayMovePrintAndPause("ef", whiteStone);
  PlayMovePrintAndPause("de", whiteStone);
  PlayMovePrintAndPause("fe", whiteStone);

  PlayMovePrintAndPause("ge", blackStone);
  PlayMovePrintAndPause("fd", blackStone);
  PlayMovePrintAndPause("ff", blackStone);
  PlayMovePrintAndPause("ee", blackStone);

  // Here it should say that Ko is not allowed
  PlayMovePrintAndPause("fe", whiteStone);

  // Random move just to see if it continues
  PlayMovePrintAndPause("aa", blackStone);
}

// LargeCaptureTest();
// SmallCornerCaptureTest();
// MediumCornerCaptureTest();
// MediumCornerSuicidePreventionTest();
SmallKoTest();
