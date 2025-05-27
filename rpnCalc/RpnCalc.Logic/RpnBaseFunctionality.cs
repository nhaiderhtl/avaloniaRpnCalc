using RpnCalc.Core;

namespace RpnCalc.Logic;

public class RpnBaseFunctionality : IRpnCalculator
{
    public IReadOnlyCollection<double> Stack { get; }

    public void Push(double value)
    {
        throw new NotImplementedException();
    }

    public double Pop()
    {
        throw new NotImplementedException();
    }

    public void Add()
    {
        throw new NotImplementedException();
    }

    public void Subtract()
    {
        throw new NotImplementedException();
    }

    public void Multiply()
    {
        throw new NotImplementedException();
    }

    public void Divide()
    {
        throw new NotImplementedException();
    }

    public void Swap()
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public double[] GetStackSnapshot()
    {
        throw new NotImplementedException();
    }
}