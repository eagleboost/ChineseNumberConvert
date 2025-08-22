namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_DeepSeek(string chineseNumber)
    {
      long total = 0;
      long currentSection = 0;
      int currentNumber = 0;

      for (var i = 0; i < chineseNumber.Length; i++)
      {
        var c = chineseNumber[i];
        if (TryGetNumber(c, out var num))
        {
          currentNumber = num;
        }
        else
        {
          var unit = GetUnit(c);
          if (unit >= 10000) // 大单位（亿/万）
          {
            total += (currentSection + currentNumber) * unit;
            currentSection = 0;
            currentNumber = 0;
          }
          else // 小单位（千/百/十）
          {
            if (currentNumber == 0)
              currentNumber = 1;

            currentSection += currentNumber * unit;
            currentNumber = 0;
          }
        }
        // 零被隐式跳过（既不匹配单位也不匹配数字）
      }

      return total + currentSection + currentNumber;
    }
  }
}