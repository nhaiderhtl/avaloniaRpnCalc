using RpnCalc.Exceptions;
using RpnCalc.Logic;

namespace RpnCalc.Test
{
    public class RpnBaseFunctionalityTests
    {
        [Fact]
        public void Push_Pop_ReturnsSameValue()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(3.14);
            Assert.Equal(3.14, calc.Pop());
        }

        [Fact]
        public void Pop_EmptyStack_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            Assert.Throws<RpnStackUnderflowException>(() => calc.Pop());
        }

        [Fact]
        public void Add_WithTwoValues_PushesSum()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(2);
            calc.Push(5);
            calc.Add();
            Assert.Equal(new double[] { 7 }, calc.GetStackSnapshot());
        }

        [Fact]
        public void Add_InsufficientValues_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            Assert.Throws<RpnStackUnderflowException>(() => calc.Add());
        }

        [Fact]
        public void Subtract_WithTwoValues_PushesDifference()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(10);
            calc.Push(4);
            calc.Subtract();
            Assert.Equal(new double[] { 6 }, calc.GetStackSnapshot());
        }

        [Fact]
        public void Subtract_InsufficientValues_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            Assert.Throws<RpnStackUnderflowException>(() => calc.Subtract());
        }

        [Fact]
        public void Multiply_WithTwoValues_PushesProduct()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(3);
            calc.Push(4);
            calc.Multiply();
            Assert.Equal(new double[] { 12 }, calc.GetStackSnapshot());
        }

        [Fact]
        public void Multiply_InsufficientValues_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            Assert.Throws<RpnStackUnderflowException>(() => calc.Multiply());
        }

        [Fact]
        public void Divide_WithTwoValues_PushesQuotient()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(20);
            calc.Push(4);
            calc.Divide();
            Assert.Equal(new double[] { 5 }, calc.GetStackSnapshot());
        }

        [Fact]
        public void Divide_ByZero_ThrowsDivisionByZeroException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(5);
            calc.Push(0);
            Assert.Throws<RpnDivisionByZeroException>(() => calc.Divide());
        }

        [Fact]
        public void Divide_InsufficientValues_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            Assert.Throws<RpnStackUnderflowException>(() => calc.Divide());
        }

        [Fact]
        public void Swap_WithTwoValues_SwapsTopTwo()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            calc.Push(2);
            calc.Swap();
            var snapshot = calc.GetStackSnapshot();
            Assert.Equal(1, snapshot[0]);
            Assert.Equal(2, snapshot[1]);
        }

        [Fact]
        public void Swap_InsufficientValues_ThrowsUnderflowException()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            Assert.Throws<RpnStackUnderflowException>(() => calc.Swap());
        }

        [Fact]
        public void Clear_EmptiesStack()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            calc.Push(2);
            calc.Clear();
            Assert.Empty(calc.GetStackSnapshot());
        }

        [Fact]
        public void GetStackSnapshot_ReturnsStackArrayInLifoOrder()
        {
            var calc = new RpnBaseFunctionality();
            calc.Push(1);
            calc.Push(2);
            calc.Push(3);
            Assert.Equal(new double[] { 3, 2, 1 }, calc.GetStackSnapshot());
        }
    }
}