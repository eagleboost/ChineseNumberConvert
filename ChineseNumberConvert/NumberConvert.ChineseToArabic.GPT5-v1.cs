namespace ChineseNumberConvert;

using System.Runtime.CompilerServices;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DigitValue(char c)
    {
      // 仅支持常用汉字数字（可按需扩展“两”等）
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsDigitChar(char c) => DigitValue(c) >= 0;

    public static long ConvertChineseToArabic_GPT_5_v1(string chineseNumber)
    {
      var s = chineseNumber.AsSpan();
      // total 汇总整段（包含万/亿后累积）
      int total = 0;
      // section 表示当前 "万/亿" 以内的部分（千百十个位的累加）
      int section = 0;
      // curNum 表示当前遇到的数字（1-9），零会被处理为占位
      int curNum = 0;

      for (int i = 0; i < s.Length; i++)
      {
        char ch = s[i];

        if (IsDigitChar(ch))
        {
          curNum = DigitValue(ch);
          continue;
        }

        switch (ch)
        {
          case '十':
          {
            // 当没有显式数字在十前时（如 "十" 或 "十二"），视为 1 十
            int n = curNum == 0 ? 1 : curNum;
            section += n * 10;
            curNum = 0;
            break;
          }
          case '百':
          {
            int n = curNum == 0 ? 1 : curNum;
            section += n * 100;
            curNum = 0;
            break;
          }
          case '千':
          {
            int n = curNum == 0 ? 1 : curNum;
            section += n * 1000;
            curNum = 0;
            break;
          }
          case '万':
          {
            // 在遇到 万 时，把当前 section + curNum 作为这一段数值，然后乘以 10000 加入 total
            section += curNum;
            total += section * 10_000;
            section = 0;
            curNum = 0;
            break;
          }
          case '亿':
          {
            // 同理，乘以 100_000_000
            section += curNum;
            total += section * 100_000_000;
            section = 0;
            curNum = 0;
            break;
          }
          case '零':
          {
            // 零 只作为占位：清除 curNum（它通常为0），继续
            curNum = 0;
            break;
          }
          default:
          {
            // 遇到未知字符：忽略或抛出。这里选择忽略（便于应对特殊空白等）
            break;
          }
        }
      }

      // 循环结束，处理残留的数字
      section += curNum;
      total += section;

      return total;
    }
  }
}