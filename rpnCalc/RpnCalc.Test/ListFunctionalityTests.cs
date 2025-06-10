using RpnCalc.Exceptions;
using RpnCalc.Logic;

namespace RpnCalc.Test;

public class ListFunctionalityTests
{
    private readonly ListFunctionality _logic = new ListFunctionality();

    [Fact]
    public void Map_NullList_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _logic.Map(null, "+1"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("+")]
    public void Map_InvalidOperationString_ThrowsArgumentException(string operation)
    {
        var sample = new List<double> { 1, 2, 3 };
        Assert.Throws<ArgumentException>(() => _logic.Map(sample, operation));
    }

    [Fact]
    public void Map_UnparsableNumber_ThrowsArgumentException()
    {
        var sample = new List<double> { 5 };
        Assert.Throws<ArgumentException>(() => _logic.Map(sample, "+abc"));
    }

    [Fact]
    public void Map_UnsupportedOperator_ThrowsArgumentException()
    {
        var sample = new List<double> { 2 };
        Assert.Throws<ArgumentException>(() => _logic.Map(sample, "^2"));
    }

    [Fact]
    public void Map_DivideByZero_ThrowsDivideByZeroException()
    {
        var sample = new List<double> { 1, 2, 3 };
        Assert.Throws<DivideByZeroException>(() => _logic.Map(sample, "/0"));
    }

    [Theory]
    [InlineData("+2", new double[] { 1, 2, 3 }, new double[] { 3, 4, 5 })]
    [InlineData("-1.5", new double[] { 3, 2, 1 }, new double[] { 1.5, 0.5, -0.5 })]
    [InlineData("*3", new double[] { 2, 0, -1 }, new double[] { 6, 0, -3 })]
    [InlineData("/2", new double[] { 4, 2, -2 }, new double[] { 2, 1, -1 })]
    public void Map_ValidOperations_ReturnsExpected(string operation, double[] input, double[] expected)
    {
        var list = new List<double>(input);
        var result = _logic.Map(list, operation);

        Assert.Equal(expected.Length, result.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i], 6);
        }
    }

    [Fact]
    public void Map_EmptyList_ReturnsEmptyList()
    {
        var result = _logic.Map(new List<double>(), "+5");
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ParseBracketList_EmptyOrWhitespace_ThrowsInvalidBracket(string input)
    {
        Assert.Throws<RpnInvalidBracketException>(() =>
            _logic.ParseBracketList(input));
    }

    [Theory]
    [InlineData("1 2 3")]
    [InlineData("[1 2 3")]
    [InlineData("1 2 3]")]
    [InlineData(" {1 2 3}")]
    [InlineData("[1 2 3}")]
    public void ParseBracketList_MissingOrBadBrackets_ThrowsInvalidBracket(string input)
    {
        Assert.Throws<RpnInvalidBracketException>(() =>
            _logic.ParseBracketList(input));
    }

    [Fact]
    public void ParseBracketList_ExactEmptyBrackets_ReturnsEmptyList()
    {
        var result = _logic.ParseBracketList("[]");
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("[42]", new double[] { 42 })]
    [InlineData("[  3.14 ]", new double[] { 3.14 })]
    public void ParseBracketList_SingleValue_ReturnsListWithOne(string input, double[] expected)
    {
        var result = _logic.ParseBracketList(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseBracketList_MultipleSpaceSeparatedValues_ReturnsAll()
    {
        var src = "[1 2 3.5 -4]";
        var expected = new List<double> { 1, 2, 3.5, -4 };
        var result = _logic.ParseBracketList(src);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseBracketList_TabSeparatedValues_ReturnsAll()
    {
        var src = "[10\t20\t30]";
        var expected = new List<double> { 10, 20, 30 };
        var result = _logic.ParseBracketList(src);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseBracketList_SemicolonSeparatedValues_ReturnsAll()
    {
        var src = "[5;6;7.5]";
        var expected = new List<double> { 5, 6, 7.5 };
        var result = _logic.ParseBracketList(src);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("[1 2 foo]")]
    [InlineData("[bar]")]
    [InlineData("[1; two;3]")]
    public void ParseBracketList_NonNumericToken_ThrowsInvalidBracket(string input)
    {
        Assert.Throws<RpnInvalidBracketException>(() =>
            _logic.ParseBracketList(input));
    }

    [Fact]
    public void ParseBracketList_ExtraWhitespace_PreservesCorrectParsing()
    {
        var src = "[   1    2\t\t3   ;4  ]";
        // splits on space, tab or semicolon
        var expected = new List<double> { 1, 2, 3, 4 };
        var result = _logic.ParseBracketList(src);
        Assert.Equal(expected, result);
    }
}