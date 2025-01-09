namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    private static readonly Dictionary<char, int> ChineseNumMap = new()
    {
      {'零', 0},
      {'一', 1},
      {'二', 2},
      {'三', 3},
      {'四', 4},
      {'五', 5},
      {'六', 6},
      {'七', 7},
      {'八', 8},
      {'九', 9},
      {'十', 10},
      {'百', 100},
      {'千', 1000},
      {'万', 10000},
      {'亿', 100000000}
    };
    
    public static int ConvertChineseToArabic_DeepSeek(string chineseNumber)
    {
      if (string.IsNullOrEmpty(chineseNumber))
        throw new ArgumentException("输入字符串不能为空。");
    
      int total = 0;
      chineseNumber = chineseNumber.Trim();
    
      // 处理“亿”
      int yiIndex = chineseNumber.IndexOf("亿", StringComparison.Ordinal);
      if (yiIndex != -1)
      {
        string yiPart = chineseNumber.Substring(0, yiIndex);
        total += ConvertSmallUnit(yiPart) * 100000000;
        chineseNumber = chineseNumber.Substring(yiIndex + 1);
      }
    
      // 处理“万”
      int wanIndex = chineseNumber.IndexOf("万", StringComparison.Ordinal);
      if (wanIndex != -1)
      {
        string wanPart = chineseNumber.Substring(0, wanIndex);
        total += ConvertSmallUnit(wanPart) * 10000;
        chineseNumber = chineseNumber.Substring(wanIndex + 1);
      }
    
      // 处理剩余部分
      total += ConvertSmallUnit(chineseNumber);
    
      return total;
    }
    
    private static int ConvertSmallUnit(string part)
    {
      int result = 0;
      int temp = 0;
    
      for (var i = 0; i < part.Length; i++)
      {
        if (ChineseNumMap.TryGetValue(part[i], out var value))
        {
          if (value >= 10)
          {
            if (temp == 0)
            {
              if (value == 10)
                temp = 10;
              else if (value == 100)
                temp = 100;
              else if (value == 1000)
                temp = 1000;
            }
            else
            {
              if (value == 10)
                temp *= 10;
              else if (value == 100)
                temp *= 100;
              else if (value == 1000)
                temp *= 1000;
            }

            result += temp;
            temp = 0;
          }
          else
          {
            temp = value;
          }
        }
        else
        {
          throw new ArgumentException("无效的中文数字字符: " + part[i]);
        }
      }
    
      result += temp;
      return result;
    }
  }
}