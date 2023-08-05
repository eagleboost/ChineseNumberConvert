// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ChineseNumberConvert;

BenchmarkRunner.Run<Benchmark>();
BenchmarkRunner.Run<BenchmarkSmall>();