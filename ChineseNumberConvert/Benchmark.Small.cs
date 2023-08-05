namespace ChineseNumberConvert;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class BenchmarkSmall
{
  [Benchmark]
  public void EN_108()
  {
    NumberConvert.EnglishToArabicStack("one hundred and eight");
  }
  
  [Benchmark]
  public void EN_88()
  {
    NumberConvert.EnglishToArabicStack("eighty eight");
  }

  [Benchmark]
  public void EN_1()
  {
    NumberConvert.EnglishToArabicStack("one");
  }
  
  [Benchmark]
  public void CN_108()
  {
    NumberConvert.ChineseToArabic("一百零八");
  }
  
  [Benchmark]
  public void CN_88()
  {
    NumberConvert.ChineseToArabic("八十八");
  }
  
  [Benchmark]
  public void CN_1()
  {
    NumberConvert.ChineseToArabic("一");
  }
}