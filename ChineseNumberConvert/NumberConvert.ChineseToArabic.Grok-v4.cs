namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_Grok_V4(string chineseNumber)
    {
      long total = 0;
      long currentSegment = 0;
      long currentNumber = 0;

      for (int i = 0; i < chineseNumber.Length; i++)
      {
        char c = chineseNumber[i];
        
        long digit = GetDigit1(c);
        if (digit >= 0)
        {
          currentNumber = digit;
          continue;
        }

        long unit = GetUnit1(c);
        if (unit < 0)
        {
          throw new ArgumentException($"Invalid character '{c}' in input.");
        }

        if (unit >= 10000)
        {
          total += (currentSegment + currentNumber) * unit;
          currentSegment = 0;
          currentNumber = 0;
        }
        else
        {
          long effectiveNumber = (currentNumber == 0) ? 1 : currentNumber;
          currentSegment += effectiveNumber * unit;
          currentNumber = 0;
        }
      }

      return total + currentSegment + currentNumber;
    }
    
    private static long GetDigit1(char c)
    {
      return c switch
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
    }
  }
}