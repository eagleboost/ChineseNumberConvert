namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_GPT_o1_mini(string chineseNumber)
    {
      long result = 0;
      long section = 0; // 当前段的值
      long number = 0;  // 当前数字

      foreach (var c in chineseNumber)
      {
        if (TryGetNumber(c, out var num))
        {
          number = num;
        }
        else
        {
          long unit = GetUnit(c);
          if (unit is 10000 or 100000000)
          {
            // 处理“万”或“亿”单位
            section = (section + number) * unit;
            result += section;
            section = 0;
          }
          else
          {
            // 处理“十”、“百”、“千”单位
            if (number == 0)
            {
              // 当单位前没有数字时，默认数字为1
              section += 1 * unit;
            }
            else
            {
              section += number * unit;
            }
          }
          number = 0;
        }
      }

      return result + section + number;
    }
  }
}