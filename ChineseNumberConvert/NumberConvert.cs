namespace ChineseNumberConvert;
using static NumberConvert.ChineseToArabicImpl;
using static NumberConvert.ArabicToChineseImpl;
using static NumberConvert.EnglishToArabicImpl;
using static NumberConvert.EnglishToArabicStackImpl;

public static partial class NumberConvert
{
  public static long ChineseToArabic(string chineseNumber)
  {
    return ConvertChineseToArabic(chineseNumber);
  }
  
  public static string ArabicToChinese(long number)
  {
    return ConvertArabicToChinese(number);
  }
  
  public static long EnglishToArabic(string chineseNumber)
  {
    return ConvertEnglishToArabic(chineseNumber);
  }
  
  public static long EnglishToArabicStack(string chineseNumber)
  {
    return ConvertEnglishToArabicStack(chineseNumber);
  }
}