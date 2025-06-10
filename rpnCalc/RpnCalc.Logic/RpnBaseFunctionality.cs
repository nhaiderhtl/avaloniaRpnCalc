using RpnCalc.Core;
using RpnCalc.Exceptions;

namespace RpnCalc.Logic;

public class RpnBaseFunctionality : IRpnCalculator
{
    public IReadOnlyCollection<double> Stack => _stack;
    private Stack<double> _stack = new Stack<double>();

    public void Push(double value)
    {
        _stack.Push(value);
    }
    //TODO LET IT BE ABLE TO PUSH A LIST

    public double Pop()
    {
        if (_stack.Count == 0)
        {
            throw new RpnStackUnderflowException();
        }

        return _stack.Pop();
    }

    public void Add()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException();
        }

        var a = _stack.Pop();
        var b = _stack.Pop();
        _stack.Push(a + b);
    }

    public void Subtract()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException();
        }

        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a - b);
    }

    public void Multiply()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException();
        }

        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a * b);
    }

    public void Divide()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException();
        }

        var b = _stack.Pop();
        var a = _stack.Pop();
        if (b == 0) throw new RpnDivisionByZeroException();
        _stack.Push(a / b);
    }

    public void Swap()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException();
        }

        var a = _stack.Pop();
        var b = _stack.Pop();
        _stack.Push(a);
        _stack.Push(b);
    }

    public void Clear()
    {
        _stack.Clear();
    }

    public double[] GetStackSnapshot()
    {
        return _stack.ToArray();
    }
}