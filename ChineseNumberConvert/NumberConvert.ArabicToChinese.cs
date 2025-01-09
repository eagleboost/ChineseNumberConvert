namespace ChineseNumberConvert;

using System.Text;

public static partial class NumberConvert
{
  public static class ArabicToChineseImpl
  {
    private const char Zero = '零';
    private const long Yi = 1_0000_0000;
    private const long Wan = 1_0000;
    private const long Qian = 1000;
    private const long Bai = 100;
    private const long Shi = 10;

    private static readonly Dictionary<long, char> SmallNumber = new()
    {
      { 0, '零' },
      { 1, '一' },
      { 2, '二' },
      { 3, '三' },
      { 4, '四' },
      { 5, '五' },
      { 6, '六' },
      { 7, '七' },
      { 8, '八' },
      { 9, '九' },
    };
    
    public static string ConvertArabicToChinese(long number)
    {
      var sb = new StringBuilder();
      var current = number;
      ConvertGroup(ref current, Yi, sb, out var hasPendingYiZero);
      if (current >= Wan && hasPendingYiZero)
      {
        AppendZero(sb);
        hasPendingYiZero = false;
      }

      ConvertGroup(ref current, Wan, sb, out var hasPendingWanZero);

      var small = SmallArabicToChinese(current);
      if (small != null && (hasPendingYiZero || hasPendingWanZero))
      {
        AppendZero(sb);
      }

      sb.Append(small);
      return sb.ToString();
    }

    private static void ConvertGroup(ref long number, long groupNumber, StringBuilder sb, out bool hasPendingZero)
    {
      hasPendingZero = false;
      if (number >= groupNumber)
      {
        var part = Math.DivRem(number, groupNumber);
        var small = SmallArabicToChinese(part.Quotient);
        sb.Append(small);
        AppendUnit(sb, GetUnit(groupNumber));
        if (part.Remainder < groupNumber / 10)
        {
          hasPendingZero = true;
        }

        number = part.Remainder;
      }
    }

    private static string? SmallArabicToChinese(long number)
    {
      if (number == 0)
      {
        return null;
      }

      if (number == 10)
      {
        return "十";
      }

      if (number < 10)
      {
        return GetNumber(number).ToString();
      }

      if (number < 20)
      {
        return "十" + GetNumber(number - 10);
      }

      var sb = new StringBuilder();

      var current = number;
      ConvertSmallGroup(ref current, Qian, sb, false, out var hasThousand);
      ConvertSmallGroup(ref current, Bai, sb, false, out var hasHundred);
      ConvertSmallGroup(ref current, Shi, sb, hasThousand && !hasHundred, out var hasTen);
      ConvertSmallGroup(ref current, 1, sb, !hasTen, out _);

      return sb.ToString();
    }

    private static void ConvertSmallGroup(ref long number, long groupNumber, StringBuilder sb, bool hasPendingZero, out bool hasGroup)
    {
      hasGroup = false;
      var part = Math.DivRem(number, groupNumber);
      number = part.Remainder;
      if (part.Quotient > 0)
      {
        if (hasPendingZero)
        {
          AppendZero(sb);
        }

        AppendNumber(sb, GetNumber(part.Quotient));
        AppendUnit(sb, GetUnit(groupNumber));
        hasGroup = true;
      }
    }

    private static void AppendZero(StringBuilder sb)
    {
      sb.Append(Zero);
    }

    private static void AppendNumber(StringBuilder sb, char number)
    {
      sb.Append(number);
    }

    private static void AppendUnit(StringBuilder sb, char unit)
    {
      if (unit != default)
      {
        sb.Append(unit);
      }
    }

    private static char GetNumber(long num)
    {
      SmallNumber.TryGetValue(num, out var result);
      return result;
    }

    private static char GetUnit(long num)
    {
      return num switch
      {
        1 => default,
        10 => '十',
        100 => '百',
        1000 => '千',
        1_0000 => '万',
        1_0000_0000 => '亿',
        _ => throw new ArgumentOutOfRangeException(nameof(num), num, null)
      };
    }
  }
}