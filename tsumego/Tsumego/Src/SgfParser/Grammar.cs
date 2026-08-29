using Sprache;

namespace Tsumego.Src.SgfParser;

internal static class Grammar
{
    private static readonly Parser<char> BackslashEscape = Parse.Char('\\');
    private static readonly Parser<char> PropertyDelimiterBegin = Parse.Char('[');
    private static readonly Parser<char> PropertyDelimiterEnd = Parse.Char(']');
    private static readonly Parser<char> ChildrenDelimiterBegin = Parse.Char('(');
    private static readonly Parser<char> ChildrenDelimiterEnd = Parse.Char(')');
    private static readonly Parser<char> InstructionStart = Parse.Char(';');

    private static Parser<T> Escaped<T>(Parser<T> following) =>
        from _ in BackslashEscape.Once()
        from f in following
        select f;

    private static readonly Parser<char> PropertyContent = 
        Escaped(PropertyDelimiterBegin)
        .Or(Escaped(PropertyDelimiterEnd))
        .Or(Escaped(BackslashEscape))
        .Or(Parse.AnyChar.Except(PropertyDelimiterBegin).Except(PropertyDelimiterEnd));

    private static readonly Parser<string> Property =
        from open in PropertyDelimiterBegin
        from content in PropertyContent.Many().Text()
        from end in PropertyDelimiterEnd
        select content.Replace("\\t", " ").Replace("\\n", "\n");

    private static readonly Parser<string> Key = Parse.Many(Parse.Char(char.IsUpper, "Only upper keys allowed")).Text();

    private static readonly Parser<KeyValuePair<string, IEnumerable<string>>> KeyProperties =
        from key in Key
        from properties in Property.AtLeastOnce()
        select new KeyValuePair<string, IEnumerable<string>>(key, properties);

    private static readonly Parser<Dictionary<string, string[]>> ValueProperties =
        from keyProperties in KeyProperties.Many()
        select keyProperties.ToDictionary(keyProperties => keyProperties.Key, 
            keyProperties => keyProperties.Value.ToArray());


    public static readonly Parser<SgfTree> Children =
        from _ in ChildrenDelimiterBegin
        from node in Node
        from _2 in ChildrenDelimiterEnd
        select node;

    private static readonly Parser<SgfTree> Node =
        from _ in InstructionStart
        from value in ValueProperties
        from children in Node.Once().Or(Children.Many())
        select new SgfTree(value, children.ToArray());
}
