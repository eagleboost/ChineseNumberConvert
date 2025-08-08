namespace ChineseNumberConvert;
using static NumberConvert.ChineseToArabicImpl;
using static NumberConvert.ArabicToChineseImpl;
using static NumberConvert.EnglishToArabicImpl;
using static NumberConvert.EnglishToArabicStackImpl;
using static ChineseToArabicMethod;

public static partial class NumberConvert
{
  public static long ChineseToArabic(string chineseNumber, ChineseToArabicMethod method = Default)
  {
    return method switch
    {
      Gpt => ConvertChineseToArabic_GPT_o1_mini(chineseNumber),
      Gpt5V1 => ConvertChineseToArabic_GPT_5_v1(chineseNumber),
      Gpt5V2 => ConvertChineseToArabic_GPT_5_v2(chineseNumber),
      Gpt5V3 => ConvertChineseToArabic_GPT_5_v3(chineseNumber),
      DeepSeek => ConvertChineseToArabic_DeepSeek(chineseNumber),
      Claude => ConvertChineseToArabic_Claude4(chineseNumber),
      _ => ConvertChineseToArabic(chineseNumber),
    };
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