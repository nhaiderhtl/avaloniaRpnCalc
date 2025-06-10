using RpnCalc.Logic;
using RpnCalc.Exceptions;

namespace RpnCalc.Test
{
    public class ListFunctionalityTests
    {
        private readonly ListFunctionality _logic = new ListFunctionality();

        [Fact]
        public void Length_Null_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _logic.Length(null));

        [Fact]
        public void Length_Valid_ReturnsCount()
        {
            var list = new List<double> { 1,2,3 };
            Assert.Equal(3, _logic.Length(list));
        }

        [Fact]
        public void Sum_Null_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _logic.Sum(null));

        [Fact]
        public void Sum_Valid_ReturnsSum()
        {
            var list = new List<double> { 1.5,2.5,-1 };
            Assert.Equal(3.0, _logic.Sum(list), 6);
        }

        [Fact]
        public void Average_Null_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _logic.Average(null));

        [Fact]
        public void Average_Empty_ThrowsInvalidOp() =>
            Assert.Throws<InvalidOperationException>(() => _logic.Average(new List<double>()));

        [Fact]
        public void Average_Valid_ReturnsAvg()
        {
            var list = new List<double> { 2,4,6 };
            Assert.Equal(4.0, _logic.Average(list), 6);
        }

        [Fact]
        public void Map_NullList_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _logic.Map(null, "+1"));

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("+")]
        public void Map_BadOpString_Throws(string op) =>
            Assert.Throws<ArgumentException>(() => _logic.Map(new List<double>{1}, op));

        [Fact]
        public void Map_Unparsable_Throws() =>
            Assert.Throws<ArgumentException>(() => _logic.Map(new List<double>{1}, "+abc"));

        [Fact]
        public void Map_UnsupportedOp_Throws() =>
            Assert.Throws<ArgumentException>(() => _logic.Map(new List<double>{2}, "^2"));

        [Fact]
        public void Map_DivideByZero_Throws() =>
            Assert.Throws<RpnDivisionByZeroException>(() => _logic.Map(new List<double>{1,2}, "/0"));

        [Theory]
        [InlineData("+2", new[]{1.0,2.0}, new[]{3.0,4.0})]
        [InlineData("-1.5", new[]{3.0,2.0}, new[]{1.5,0.5})]
        [InlineData("*3", new[]{2.0,0.0,-1.0}, new[]{6.0,0.0,-3.0})]
        [InlineData("/2", new[]{4.0,-2.0}, new[]{2.0,-1.0})]
        public void Map_Valid_ReturnsExpected(string op, double[] input, double[] expected)
        {
            var res = _logic.Map(new List<double>(input), op);
            Assert.Equal(expected.Length, res.Count);
            for(int i=0;i<expected.Length;i++)
                Assert.Equal(expected[i], res[i], 6);
        }

        [Fact]
        public void Map_EmptyList_ReturnsEmpty() =>
            Assert.Empty(_logic.Map(new List<double>(), "*5"));

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ParseBracketList_Empty_Throws(string input) =>
            Assert.Throws<RpnInvalidBracketException>(() => _logic.ParseBracketList(input));

        [Theory]
        [InlineData("1 2 3")]
        [InlineData("[1 2 3")]
        [InlineData("1 2 3]")]
        [InlineData("{1;2;3}")]
        [InlineData("[1;2;3}")]
        public void ParseBracketList_BadBrackets_Throws(string input) =>
            Assert.Throws<RpnInvalidBracketException>(() => _logic.ParseBracketList(input));

        [Fact]
        public void ParseBracketList_EmptyBrackets_ReturnsEmpty()
        {
            var outp = _logic.ParseBracketList("[]");
            Assert.Empty(outp);
        }

        [Theory]
        [InlineData("[42]", new[]{42.0})]
        [InlineData("[ 3.14 ]", new[]{3.14})]
        public void ParseBracketList_SingleVal_Returns(string input, double[] exp)
        {
            var outp = _logic.ParseBracketList(input);
            Assert.Equal(exp.Length, outp.Count);
            Assert.Equal(exp[0], outp[0],6);
        }

        [Fact]
        public void ParseBracketList_SemicolonSeparated_ReturnsAll()
        {
            var outp = _logic.ParseBracketList("[1;2;3.5;-4]");
            Assert.Equal(new List<double>{1,2,3.5,-4}, outp);
        }

        [Fact]
        public void ParseBracketList_SpaceSeparated_ReturnsAll()
        {
            var outp = _logic.ParseBracketList("[1 2 3]");
            Assert.Equal(new List<double>{1,2,3}, outp);
        }

        [Fact]
        public void ParseBracketList_MixedSep_ReturnsAll()
        {
            var outp = _logic.ParseBracketList("[1; 2 3;4]");
            Assert.Equal(new List<double>{1,2,3,4}, outp);
        }

        [Theory]
        [InlineData("[1; foo ;3]")]
        [InlineData("[bar]")]
        [InlineData("[1 two 3]")]
        public void ParseBracketList_NonNumeric_Throws(string input) =>
            Assert.Throws<RpnInvalidBracketException>(() => _logic.ParseBracketList(input));
    }
}
