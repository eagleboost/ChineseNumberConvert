namespace ChineseNumberConvert;

public static partial class NumberConvert
{
  public static partial class ChineseToArabicImpl
  {
    public static long ConvertChineseToArabic_Claude4(string chineseNumber)
    {
        if (string.IsNullOrEmpty(chineseNumber))
            throw new ArgumentException("Input cannot be null or empty");

        chineseNumber = chineseNumber.Trim();
        
        // Handle special case for just "十" (means 10)
        if (chineseNumber == "十" || chineseNumber == "拾")
            return 10;

        long result = 0;
        long currentSection = 0;
        long currentNumber = 0;
        bool hasDigit = false;

        for (int i = 0; i < chineseNumber.Length; i++)
        {
            char ch = chineseNumber[i];

            if (TryGetNumber(ch, out var number))
            {
                currentNumber = number;
                hasDigit = true;
            }
            else
            {
                long unit = GetUnit(ch);

                if (unit >= 10000) // 万 or 亿
                {
                    if (hasDigit)
                    {
                        currentSection += currentNumber;
                    }
                    else if (currentSection == 0)
                    {
                        currentSection = 1; // Handle cases like "万" meaning "一万"
                    }

                    result += currentSection * unit;
                    currentSection = 0;
                    currentNumber = 0;
                    hasDigit = false;
                }
                else // 十, 百, 千
                {
                    if (!hasDigit)
                    {
                        // Handle cases like "十五" (fifteen) where 十 implies 一十
                        if (unit == 10 && i == 0)
                        {
                            currentNumber = 1;
                        }
                        else
                        {
                            currentNumber = 1;
                        }
                    }

                    currentSection += currentNumber * unit;
                    currentNumber = 0;
                    hasDigit = false;
                }
            }
        }

        // Add any remaining number
        if (hasDigit)
        {
            currentSection += currentNumber;
        }

        result += currentSection;
        return result;
    }
  }
}