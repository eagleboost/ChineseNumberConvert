namespace ChineseTests;

using static ChineseNumberConvert.NumberConvert;

public class EnglishNumberTests
{
  public static object[] TestCases =
  {
    new object[] { "one", 1 },
    new object[] { "thirty-one million, eighty-four thousand, nine hundred and ninety", 31084990 },
    new object[] { "forty-seven million, two hundred and forty-one thousand, nine hundred and five", 47241905 },
    new object[] { "sixty-eight million, two hundred and eighty-eight thousand, sixty-one", 68288061 },
    new object[] { "thirty-six million, five hundred and nine thousand, two hundred and ninety-four", 36509294 },
    new object[] { "twenty million, four hundred and thirty thousand, two hundred and eighty", 20430280 },
    new object[] { "eighteen million, thirty-eight thousand, four hundred and seven", 18038407 },
    new object[] { "thirty-one million, two hundred and seventy-eight thousand, two hundred and thirty", 31278230 },
    new object[] { "sixty-two million, nine hundred and thirteen thousand, five hundred and ninety-eight", 62913598 },
    new object[] { "seventeen million, six hundred and three thousand, six hundred and six", 17603606 },
    new object[] { "thirty-nine million, four hundred and eighty-five thousand, three hundred and seventy-one", 39485371 },
    new object[] { "ten million, nine hundred and fourteen thousand, sixty-nine", 10914069 },
    new object[] { "ninety-two million, seven hundred and thirty-nine thousand, three hundred and fifty-seven", 92739357 },
    new object[] { "eight million, seven hundred and thirty-one thousand, seven hundred and thirteen", 8731713 },
    new object[] { "five million, six hundred and fifteen thousand, sixty-one", 5615061 },
    new object[] { "seventy-seven million, four hundred and eighty-one thousand, ninety-six", 77481096 },
    new object[] { "twenty-two million, four hundred and forty-four thousand, thirty-four", 22444034 },
    new object[] { "ninety-six million, seven hundred and ninety-one thousand, thirty-four", 96791034 },
    new object[] { "six million, three hundred and eighty-four thousand, four hundred and four", 6384404 },
    new object[] { "fifty-seven million, four hundred and forty-five thousand, forty-nine", 57445049 },
    new object[] { "fifty-one million, six hundred and eleven thousand, five hundred and twenty-four", 51611524 },
    new object[] { "seventy-two million, five hundred and forty-one thousand, three hundred and eighty-eight", 72541388 },
  };

  [Test]
  [TestCaseSource(nameof(TestCases))]
  public void EnglishToArabicTest(string enNumber, int expected)
  {
    Assert.That(EnglishToArabic(enNumber) == expected);
  }
  
  [Test]
  [TestCaseSource(nameof(TestCases))]
  public void EnglishToArabicStackTest(string enNumber, int expected)
  {
    Assert.That(EnglishToArabicStack(enNumber) == expected);
  }
}