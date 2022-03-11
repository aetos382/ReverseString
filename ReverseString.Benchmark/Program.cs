using BenchmarkDotNet.Running;

namespace ReverseString.Benchmark;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<ReverseStringBenchmark>();
    }
}
