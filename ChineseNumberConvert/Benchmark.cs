namespace ChineseNumberConvert;

using BenchmarkDotNet.Attributes;
using static ChineseToArabicMethod;

[MemoryDiagnoser]
public class Benchmark
{
  private const string ChineseNumber = "七千二百五十四万一千三百八十八";
  private const string EnglishNumber = "seventy-two million, five hundred and forty thousand, three hundred and eighty-eight";
  
  [Benchmark]
  public void CN_To_Arabic()
  {
    NumberConvert.ChineseToArabic(ChineseNumber);
  }

  [Benchmark]
  public void CN_To_Arabic_GPT()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, Gpt);
  }

  [Benchmark]
  public void CN_To_Arabic_DeepSeek()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, DeepSeek);
  }

  [Benchmark]
  public void CN_To_Arabic_Claude()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, Claude);
  }
  
  [Benchmark]
  public void CN_To_Arabic_GPT5_v1()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, Gpt5V1);
  }
  
  [Benchmark]
  public void CN_To_Arabic_GPT5_v2()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, Gpt5V2);
  }
    
  [Benchmark]
  public void CN_To_Arabic_GPT5_v3()
  {
    NumberConvert.ChineseToArabic(ChineseNumber, Gpt5V3);
  }
  
  [Benchmark]
  public void EN_To_Arabic_V1()
  {
    NumberConvert.EnglishToArabic(EnglishNumber);
  }
  
  [Benchmark]
  public void EN_To_Arabic_V2()
  {
    NumberConvert.EnglishToArabicStack(EnglishNumber);
  }
}