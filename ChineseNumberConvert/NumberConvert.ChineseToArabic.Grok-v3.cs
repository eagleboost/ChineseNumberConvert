namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_Grok_V3(string chineseNumber)
    {
      long result = 0;
      long section = 0;
      long number = 0;
      bool lastWasZero = false;

      for (int i = 0; i < chineseNumber.Length; i++)
      {
        char c = chineseNumber[i];
        long digit = c switch
        {
          '零' => 0,
          '一' => 1,
          '二' => 2,
          '三' => 3,
          '四' => 4,
          '五' => 5,
          '六' => 6,
          '七' => 7,
          '八' => 8,
          '九' => 9,
          _ => -1
        };
        if (digit >= 0)
        {
          number = digit;
          lastWasZero = (digit == 0);
          continue;
        }

        long unit = c switch
        {
          '十' => 10,
          '百' => 100,
          '千' => 1000,
          '万' => 10000,
          '亿' => 100000000,
          _ => -1
        };
        if (unit < 0)
        {
          throw new ArgumentException($"Invalid character '{c}' in input.");
        }

        if (unit == 10 || unit == 100 || unit == 1000)
        {
          long effectiveNumber = (number == 0 && !lastWasZero) ? 1 : number;
          section += effectiveNumber * unit;
          number = 0;
        }
        else if (unit == 10000)
        {
          section += number;
          result += section * 10000L;
          section = 0;
          number = 0;
        }
        else if (unit == 100000000)
        {
          section += number;
          result += section;
          result *= 100000000L;
          section = 0;
          number = 0;
        }

        lastWasZero = false;
      }

      section += number;
      result += section;

      return result;
    }
  }
}