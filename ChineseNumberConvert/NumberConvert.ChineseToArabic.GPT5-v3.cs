namespace ChineseNumberConvert;

using System.Runtime.CompilerServices;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static unsafe long ConvertChineseToArabic_GPT_5_v3(string chineseNumber)
    {
      fixed (char* p = chineseNumber)
      {
        char* ptr = p;
        char* end = p + chineseNumber.Length;

        long total = 0;
        long currentSegment = 0;
        int currentNumber = 0;
        long lastUnit = long.MaxValue;

        while (ptr < end)
        {
          char c = *ptr++;

          if (TryGetNumber(c, out int num))
          {
            currentNumber = num;
            continue;
          }

          var unit = GetUnit(c);
          if (unit >= 10000) // 大单位：万、亿
          {
            total += (currentSegment + currentNumber) * unit;
            currentSegment = 0;
            currentNumber = 0;
            lastUnit = long.MaxValue;
          }
          else // 小单位：十、百、千
          {
            if (unit > lastUnit)
              throw new ArgumentException($"单位顺序错误: {c}");

            if (currentNumber == 0)
              currentNumber = 1;

            currentSegment += currentNumber * unit;
            currentNumber = 0;
            lastUnit = unit;
          }
          // 零或未识别字符自动跳过
        }

        return total + currentSegment + currentNumber;
      }
    }
  }
}