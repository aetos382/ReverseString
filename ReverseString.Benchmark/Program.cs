namespace ReverseString.Benchmark;

using BenchmarkDotNet.Running;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<ReverseStringBenchmark>();
    }
}
