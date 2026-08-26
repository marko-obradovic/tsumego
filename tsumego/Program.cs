using System.Linq;
using Tsumego.Src.SgfParser;

var tree = SgfParser.ParseTree("(;GM[1]FF[4]SZ[9];B[cc];W[dd](;B[ee])(;B[ff]))");

foreach (var item in tree.Data)
{
    Console.WriteLine($"Key: {item.Key}");

    foreach (var value in item.Value)
    {
        Console.WriteLine($"  Value: {value}");
    }
}

// Final parsing test
// Console.WriteLine(SgfParser.ParseTree("(;AB[bq][cq][co][dp][ep][en][fo][go][hn][hp][ip][jp][iq][ar]AW[br][cr][dq][eq][gp][gq][hq][ir][jq][kq][lq][lo][kn][in][hm][gm][em][dm][dn][bm];B[fq];W[fr];B[gr];W[hr];B[er];W[dr];B[fs];W[fp];B[ds])"));
//
// TODO: Make one for multi-path SGFs
