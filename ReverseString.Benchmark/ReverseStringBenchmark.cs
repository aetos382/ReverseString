using System.Text;

using BenchmarkDotNet.Attributes;

namespace ReverseString.Benchmark;

[MemoryDiagnoser]
[ShortRunJob]
public class ReverseStringBenchmark
{
    public string Value;

    [Params(100, 1000, 1000, 10000, 100000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        const string str = "𩸽";

        var count = this.Count;
        var builder = new StringBuilder(str.Length * count);

        for (int i = 0; i < count; ++i)
        {
            builder.Append(str);
        }

        this.Value = builder.ToString();
    }

    [Benchmark(Baseline = true)]
    public string ReverseString1()
    {
        var str = this.Value;
        return str.Reverse();
    }

    // [Benchmark]
    public string ReverseString2()
    {
        var str = this.Value;
        return str.ReverseRecursive();
    }

    [Benchmark]
    public string ReverseString3()
    {
        var str = this.Value;
        return str.ReverseByLinq();
    }
}
