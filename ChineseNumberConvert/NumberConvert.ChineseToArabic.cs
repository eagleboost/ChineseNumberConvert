namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic(string chineseNumber)
    {
      var unit = 1;
      var pendingUnit = 1;
      var result = default(long);
      var tempValue = default(long);
      var isLastDigitUnit = false;
      for (var i = chineseNumber.Length - 1; i >= 0; i--)
      {
        var digit = chineseNumber[i];
        if (TryGetNumber(digit, out var number))
        {
          if (number > 0)
          {
            ////当前位是非零数字，与当前单位以及还未处理的单位（比如八百万里面的‘万’）相乘并加到tempValue上
            AddToTempValue(ref tempValue, number, unit, pendingUnit);
          }
          else
          {
            ////当前位是零，tempValue计算结束，加到结果中以便开始新的计算
            AddToResultAndReset(ref result, ref tempValue);
          }

          isLastDigitUnit = false;
          continue;
        }

        var currentUnit = GetUnit(digit);
        if (currentUnit < unit)
        {
          ////当前单位比已经取得的单位小，比如八百万里面的‘百’比‘万’小，把‘万’保存到pendingUnit里面
          pendingUnit = unit;
        }
        else
        {
          if (currentUnit > pendingUnit && pendingUnit > 1)
          {
            ////遇到一个更大的单位，比如一亿零八百万里面的‘亿’比‘万’大，那么把tempValue加到结果中并把pendingUnit复位
            pendingUnit = 1;
            AddToResultAndReset(ref result, ref tempValue);
          }
        }

        unit = currentUnit; ////更新到最新的单位
        isLastDigitUnit = true;
      }

      if (isLastDigitUnit)
      {
        ////最后一位是单位，tempValue还没有计算完，1代入计算
        AddToTempValue(ref tempValue, 1, unit, pendingUnit);
      }

      ////把尚未加上的tempValue加到结果中
      AddToResultAndReset(ref result, ref tempValue);
      return result;
    }

    private static void AddToTempValue(ref long tempValue, in long number, in long unit, in long pendingUnit)
    {
      tempValue += number * unit * pendingUnit;
    }

    private static void AddToResultAndReset(ref long result, ref long tempValue)
    {
      result += tempValue;
      tempValue = 0;
    }

    private static bool TryGetNumber(char c, out int result)
    {
      result = c switch
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
        _ => -1,
      };

      return result != -1;
    }

    private static int GetUnit(char c)
    {
      return c switch
      {
        '十' => 10,
        '百' => 100,
        '千' => 1000,
        '万' => 10000,
        '亿' => 100000000,
        _ => throw new ArgumentException(c.ToString())
      };
    }
  }
}