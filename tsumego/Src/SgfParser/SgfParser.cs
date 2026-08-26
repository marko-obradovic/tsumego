using Sprache;

namespace Tsumego.Src.SgfParser;

public class SgfParser
{
  public static SgfTree ParseTree(string input)
  {
    try
    {
      return Grammar.Children.Parse(input);
    }
    catch
    {
      throw new ArgumentException();
    }
  }
}
