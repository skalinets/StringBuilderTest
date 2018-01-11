using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace StringBuilderTest
{
  public class Md5VsSha256
  {
    private const int N = 10000;
    private readonly byte[] data;

    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();

    public Md5VsSha256()
    {
      data = new byte[N];
      new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
  }

  public class StringBuilderBenchmarks
  {

    List<string> GetItems(int n) =>
         Enumerable.Range(1, n)
            .Select(_ => Guid.NewGuid().ToString())
            .ToList(); 

    [Params(2, 5, 10, 20, 50, 100)]
    public int NumberOfItems { get; set; }

    [Benchmark]
    public string StringBuilderAction() 
    {
        var sb = new StringBuilder();

        GetItems(NumberOfItems).ForEach(_ => sb.Append(_));
        return sb.ToString();
    }

    [Benchmark]
    public string StringConcatenationAction(){
        var s = "";
        GetItems(NumberOfItems).ForEach(_ => s += _);
        return s;
    }
  }
  public class Program
  {
    public static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<StringBuilderBenchmarks>();
    }
  }
}
