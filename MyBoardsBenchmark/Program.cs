// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using MyBoardsBenchmark;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<TrackingBenchmark>();