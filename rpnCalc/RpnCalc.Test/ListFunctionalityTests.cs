using RpnCalc.Logic;

namespace RpnCalc.Test
{
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
    }
}
