namespace Tsumego.Src.SgfParser;

public class SgfTree
{
  public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
  {
    if (data.Keys.Any(key => key.ToUpper() != key)) throw new ArgumentException();
    Data = data;
    Children = children;
  }

  public IDictionary<string, string[]> Data { get; }
  public SgfTree[] Children { get; }

  public override bool Equals(object other)
  {
      var otherSgfTree = other as SgfTree;

      return Data.Keys.SequenceEqual(otherSgfTree.Data.Keys) && Data.Keys.All(key => Data[key].SequenceEqual(otherSgfTree.Data[key]))
           && otherSgfTree.Children.SequenceEqual(Children);
  }

  public override int GetHashCode() => HashCode.Combine(Data, Children);
}
