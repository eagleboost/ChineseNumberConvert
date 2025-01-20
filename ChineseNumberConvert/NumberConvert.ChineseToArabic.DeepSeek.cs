namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_DeepSeek(string chineseNumber)
    {
      long total = 0;
      long currentSegment = 0;
      int currentNumber = 0;
      long lastUnit = long.MaxValue;

      foreach (char c in chineseNumber)
      {
        if (TryGetNumber(c, out int num))
        {
          currentNumber = num;
        }
        else
        {
          var unit = GetUnit(c);
          if (unit >= 10000) // 大单位（亿/万）
          {
            total += (currentSegment + currentNumber) * unit;
            currentSegment = 0;
            currentNumber = 0;
            lastUnit = long.MaxValue;
          }
          else // 小单位（千/百/十）
          {
            if (unit > lastUnit)
              throw new ArgumentException($"单位顺序错误: {c}");

            if (currentNumber == 0)
              currentNumber = 1;

            currentSegment += currentNumber * unit;
            currentNumber = 0;
            lastUnit = unit;
          }
        }
        // 零被隐式跳过（既不匹配单位也不匹配数字）
      }

      return total + currentSegment + currentNumber;
    }
  }
}