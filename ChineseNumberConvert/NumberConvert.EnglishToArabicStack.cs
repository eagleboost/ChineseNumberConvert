namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static class EnglishToArabicStackImpl
  {
    private static readonly Dictionary<int, long> SmallNumbersMap;
    private static readonly Dictionary<int, long> TensMap;
    private static readonly KeyValuePair<int, long>[] LargeNumbersMap;
    private static readonly int Hundred;
    private static readonly int And;
    
    static EnglishToArabicStackImpl()
    {
      SmallNumbersMap = SmallNumbers.ToDictionary(i => GetCharHash(i.Key), i => i.Value);
      TensMap = Tens.ToDictionary(i => GetCharHash(i.Key), i => i.Value);
      LargeNumbersMap = LargeNumbers.Select(i => new KeyValuePair<int, long>(GetCharHash(i.Key), i.Value)).ToArray();
      Hundred = GetCharHash("hundred");
      And = GetCharHash("and");
    }

    private static int GetCharHash(string str)
    {
      var result = str[0].GetHashCode();
      for (var i = 1; i < str.Length; i++)
      {
        result = (result * 397) ^ str[i].GetHashCode();
      }

      return result;
    }
    
    private static int GetCharHash(ReadOnlySpan<char> span)
    {
      var result = span[0].GetHashCode();
      for (var i = 1; i < span.Length; i++)
      {
        result = (result * 397) ^ span[i].GetHashCode();
      }

      return result;
    }
    
    public static long ConvertEnglishToArabicStack(string englishNumber)
    {
      long result = 0;
      long currentValue = 0;
      
      var span = englishNumber.AsSpan();
      var wordStart = 0;
      var isCommaPending = false;
      for (var i = 0; i < span.Length; i++)
      {
        var c = span[i];
        if (c == ',')
        {
          isCommaPending = true;
          continue;
        }
        
        if (c is '-' or ' ')
        {
          ReadOnlySpan<char> word;
          if (isCommaPending)
          {
            isCommaPending = false;
            word = span.Slice(wordStart, i - wordStart - 1);
          }
          else
          {
            word = span.Slice(wordStart, i - wordStart);
          }
          
          wordStart = i + 1;
          
          if (SmallNumbersMap.TryGetValue(GetCharHash(word), out var smallValue))
          {
            currentValue += smallValue;
          }
          else if (TensMap.TryGetValue(GetCharHash(word), out var tensValue))
          {
            currentValue += tensValue;
          }
          else if (GetCharHash(word) == Hundred)
          {
            currentValue *= 100;
          }
          else if (TryGetLargeNumber(GetCharHash(word), out var largeValue))
          {
            currentValue *= largeValue;
            result += currentValue;
            currentValue = 0;
          }
          else if (GetCharHash(word) == And)
          {
            // Do nothing, just ignore 'and'
          }
          else
          {
            throw new ArgumentException(word.ToString());
          }
        }
      }

      if (wordStart < span.Length)
      {
        var word = span.Slice(wordStart, span.Length - wordStart);
        if (SmallNumbersMap.TryGetValue(GetCharHash(word), out var smallValue))
        {
          currentValue += smallValue;
        }
        else if (TensMap.TryGetValue(GetCharHash(word), out var tensValue))
        {
          currentValue += tensValue;
        }
        else if (GetCharHash(word) == Hundred)
        {
          currentValue *= 100;
        }
      }
      
      result += currentValue;
      return result;
    }

    private static bool TryGetLargeNumber(int hash, out long value)
    {
      for (var i = 0; i < LargeNumbersMap.Length; i++)
      {
        var n = LargeNumbersMap[i];
        if (n.Key == hash)
        {
          value = n.Value;
          return true;
        }
      }

      value = 0;
      return false;
    }
  }
}