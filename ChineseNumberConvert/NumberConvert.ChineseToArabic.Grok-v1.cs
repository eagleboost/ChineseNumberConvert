namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    private static readonly char[] ChineseDigits = { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
    private static readonly char[] Units = { '十', '百', '千', '万', '亿' };
    
    public static long ConvertChineseToArabic_Grok_V1(string chineseNumber)
    {
      if (chineseNumber.Length == 0)
        return 0;

      long result = 0;
      long currentNumber = 0;
      int lastUnitIndex = -1;

      for (int i = 0; i < chineseNumber.Length; i++)
      {
        char c = chineseNumber[i];
            
        // Handle digits
        int digitIndex = Array.IndexOf(ChineseDigits, c);
        if (digitIndex >= 0)
        {
          currentNumber = digitIndex;
          continue;
        }

        // Handle units
        switch (c)
        {
          case '十':
            currentNumber = currentNumber == 0 ? 10 : currentNumber * 10;
            lastUnitIndex = 1;
            break;
          case '百':
            currentNumber *= 100;
            lastUnitIndex = 2;
            break;
          case '千':
            currentNumber *= 1000;
            lastUnitIndex = 3;
            break;
          case '万':
            result += currentNumber * 10000;
            currentNumber = 0;
            lastUnitIndex = 4;
            break;
          case '亿':
            result += currentNumber * 100000000;
            currentNumber = 0;
            lastUnitIndex = 5;
            break;
        }

        // If we encounter a smaller unit than the previous one, add currentNumber to result
        if (i + 1 < chineseNumber.Length)
        {
          int nextUnitIndex = GetUnitIndex(chineseNumber[i + 1]);
          if (nextUnitIndex != -1 && nextUnitIndex < lastUnitIndex)
          {
            result += currentNumber;
            currentNumber = 0;
          }
        }
      }

      // Add the final number
      result += currentNumber;
      return result;
    }
    
    private static int GetUnitIndex(char c)
    {
      return c switch
      {
        '十' => 1,
        '百' => 2,
        '千' => 3,
        '万' => 4,
        '亿' => 5,
        _ => -1
      };
    }
  }
}