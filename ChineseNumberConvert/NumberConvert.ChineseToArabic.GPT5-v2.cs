namespace ChineseNumberConvert;

using System.Runtime.CompilerServices;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static class UnsafeChineseNumberParser
    {
      private static readonly sbyte[] DigitMap;
      private static readonly int[] UnitMap;

      static UnsafeChineseNumberParser()
      {
        int size = 0x9FA6; // 覆盖常用汉字区
        DigitMap = new sbyte[size];
        UnitMap = new int[size];
        for (int i = 0; i < size; i++) DigitMap[i] = -1;

        // 数字
        DigitMap['零'] = 0;
        DigitMap['一'] = 1;
        DigitMap['二'] = 2;
        DigitMap['三'] = 3;
        DigitMap['四'] = 4;
        DigitMap['五'] = 5;
        DigitMap['六'] = 6;
        DigitMap['七'] = 7;
        DigitMap['八'] = 8;
        DigitMap['九'] = 9;

        // 单位
        UnitMap['十'] = 10;
        UnitMap['百'] = 100;
        UnitMap['千'] = 1000;
        UnitMap['万'] = 10_000;
        UnitMap['亿'] = 100_000_000;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static unsafe int Parse(string s)
      {
        fixed (char* p = s)
        fixed (sbyte* digitPtr = DigitMap)
        fixed (int* unitPtr = UnitMap)
        {
          char* c = p;
          char* end = p + s.Length;

          int total = 0;
          int section = 0;
          int num = 0;

          while (c < end)
          {
            char ch = *c;
            if (ch < DigitMap.Length)
            {
              int d = digitPtr[ch];
              if (d >= 0)
              {
                num = d;
                c++;
                continue;
              }

              int unit = unitPtr[ch];
              if (unit != 0)
              {
                if (unit == 10_000 || unit == 100_000_000)
                {
                  section += num;
                  total += section * unit;
                  section = 0;
                  num = 0;
                }
                else
                {
                  if (num == 0) num = 1;
                  section += num * unit;
                  num = 0;
                }
              }
            }

            c++;
          }

          return total + section + num;
        }
      }
    }

    public static long ConvertChineseToArabic_GPT_5_v2(string chineseNumber)
    {
      return UnsafeChineseNumberParser.Parse(chineseNumber);
    }
  }
}