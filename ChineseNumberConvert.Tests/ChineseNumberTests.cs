namespace ChineseNumberConvert.Tests;

using static NumberConvert;
using static ChineseToArabicMethod;

public class ChineseNumberTests
{
  [Test]
  [TestCase("一", 1)]
  [TestCase("一亿三千七百二十万六千七百八十九", 137206789)]
  [TestCase("一亿五千七百万九千二百零九", 157009209)]
  [TestCase("一亿八千九百七十一万二千三百六十一", 189712361)]
  [TestCase("一亿零一", 100000001)]
  [TestCase("一亿零二十万六千七百八十九", 1_0020_6789)]
  [TestCase("一千七百六十三", 1763)]
  [TestCase("一千三百万七千九百四十六", 13007946)]
  [TestCase("一千二百三十四", 1234)]
  [TestCase("一千六百万六千八百三十二", 16006832)]
  [TestCase("一千四百九十三", 1493)]
  [TestCase("一千零三十八万零四十", 10380040)]
  [TestCase("一百", 100)]
  [TestCase("一百一十五", 115)]
  [TestCase("一百万五千八百三十七", 1005837)]
  [TestCase("一百三十", 130)]
  [TestCase("一百八十九", 189)]
  [TestCase("一百六十三", 163)]
  [TestCase("一百四十八", 148)]
  [TestCase("一百零二万", 1020000)]
  [TestCase("一百零二万零五百九十八", 1020598)]
  [TestCase("一百零八", 108)]
  [TestCase("七十三", 73)]
  [TestCase("七千七百九十", 7790)]
  [TestCase("七千七百六十四", 7764)]
  [TestCase("七千二百五十万五千三百八十八", 72505388)]
  [TestCase("七千二百四十八", 7248)]
  [TestCase("七千八百六十一", 7861)]
  [TestCase("七百二十万零三十五", 7200035)]
  [TestCase("七百二十五万三千八百九十", 7253890)]
  [TestCase("三", 3)]
  [TestCase("三亿五千九百万九千六百八十", 359009680)]
  [TestCase("三十一", 31)]
  [TestCase("三千三百八十九", 3389)]
  [TestCase("三千四百九十万九千零六十", 34909060)]
  [TestCase("三千四百八十四", 3484)]
  [TestCase("九", 9)]
  [TestCase("九十", 90)]
  [TestCase("九十一", 91)]
  [TestCase("九十八", 98)]
  [TestCase("九千九百九十万一千三百二十一", 99901321)]
  [TestCase("九千八百", 9800)]
  [TestCase("九千八百六十三", 9863)]
  [TestCase("九千六百五十七", 9657)]
  [TestCase("九千四百五十六", 9456)]
  [TestCase("二亿六千八百万", 268000000)]
  [TestCase("二亿四千九百七十万六千七百二十九", 249706729)]
  [TestCase("二亿四千九百七十万零六千七百二十九", 249706729)]
  [TestCase("二十", 20)]
  [TestCase("二千三百八十九", 2389)]
  [TestCase("二千九百二十八", 2928)]
  [TestCase("二千零四十八", 2048)]
  [TestCase("二百一十二", 212)]
  [TestCase("二百五十五万四千八百二十三", 2554823)]
  [TestCase("二百六十九万七千零三", 2697003)]
  [TestCase("五十九", 59)]
  [TestCase("五十八万三千五百八十六", 583586)]
  [TestCase("五千六百二十一", 5621)]
  [TestCase("八十二", 82)]
  [TestCase("八千二百九十七", 8297)]
  [TestCase("八千六百五十三", 8653)]
  [TestCase("六", 6)]
  [TestCase("六十五万一千二百三十四", 651234)]
  [TestCase("六十六", 66)]
  [TestCase("六千九百万四千三百二十五", 69004325)]
  [TestCase("六千九百八十二", 6982)]
  [TestCase("六千五百二十", 6520)]
  [TestCase("六百二十四万四千零三十九", 6244039)]
  [TestCase("六百六十七万五千九百四十四", 6675944)]
  [TestCase("六百零三万九千九百七十三", 6039973)]
  [TestCase("十", 10)]
  [TestCase("十一", 11)]
  [TestCase("十六", 16)]
  [TestCase("十六亿三千八百一十万零二百四十", 1638100240)]
  [TestCase("四十四", 44)]
  [TestCase("四千二百七十四", 4274)]
  [TestCase("四百万九千五百三十七", 4009537)]
  [TestCase("四百九十二万三千五百八十六", 4923586)]
  [TestCase("零", 0)]
  public void ChineseToArabicTest(string cnNumber, int expected)
  {
    Assert.Multiple(() =>
    { 
      Assert.That(ChineseToArabic(cnNumber), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, Gpt), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, Gpt5V1), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, Gpt5V2), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, Gpt5V3), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, DeepSeek), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, Claude), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, GrokV2), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, GrokV3), Is.EqualTo(expected));
      Assert.That(ChineseToArabic(cnNumber, GrokV4), Is.EqualTo(expected));
    });
  }
}