namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static readonly Dictionary<string, long> SmallNumbers = new()
  {
    { "zero", 0 },
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 },
    { "ten", 10 },
    { "eleven", 11 },
    { "twelve", 12 },
    { "thirteen", 13 },
    { "fourteen", 14 },
    { "fifteen", 15 },
    { "sixteen", 16 },
    { "seventeen", 17 },
    { "eighteen", 18 },
    { "nineteen", 19 }
  };

  public static readonly Dictionary<string, long> Tens = new()
  {
    { "twenty", 20 },
    { "thirty", 30 },
    { "forty", 40 },
    { "fifty", 50 },
    { "sixty", 60 },
    { "seventy", 70 },
    { "eighty", 80 },
    { "ninety", 90 }
  };

  public static readonly Dictionary<string, long> LargeNumbers = new()
  {
    { "thousand", 1000 },
    { "million", 1000000 },
    { "billion", 1000000000 }
  };
}