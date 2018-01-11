using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace StringBuilderTest
{

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
