namespace ChineseNumberConvert;

using System.Globalization;

public static partial class NumberConvert
{
  public static class EnglishToArabicImpl
  {
    public static long ConvertEnglishToArabic(string englishNumber)
    {
      englishNumber = englishNumber.Replace("-", " ").ToLower();
      var words = englishNumber.Split(new[] { ' ', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
    
      long result = 0;
      long currentValue = 0;
    
      foreach (var word in words)
      {
        if (SmallNumbers.TryGetValue(word, out var smallValue))
        {
          currentValue += smallValue;
        }
        else if (Tens.TryGetValue(word, out var tensValue))
        {
          currentValue += tensValue;
        }
        else if (word == "hundred")
        {
          currentValue *= 100;
        }
        else if (LargeNumbers.TryGetValue(word, out var largeValue))
        {
          currentValue *= largeValue;
          result += currentValue;
          currentValue = 0;
        }
        else if (word == "and")
        {
          // Do nothing, just ignore 'and'
        }
        else
        {
          throw new ArgumentException(word);
        }
      }
    
      result += currentValue;
      return result;
    }
  }
}