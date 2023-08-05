namespace ChineseNumberConvert;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class Benchmark
{
  [Benchmark]
  public void CN_To_Arabic()
  {
    NumberConvert.ChineseToArabic("七千二百五十四万一千三百八十八");
  }

  [Benchmark]
  public void EN_To_Arabic_V1()
  {
    NumberConvert.EnglishToArabic("seventy-two million, five hundred and forty thousand, three hundred and eighty-eight");
  }
  
  [Benchmark]
  public void EN_To_Arabic_V2()
  {
    NumberConvert.EnglishToArabicStack("seventy-two million, five hundred and forty thousand, three hundred and eighty-eight");
  }
}