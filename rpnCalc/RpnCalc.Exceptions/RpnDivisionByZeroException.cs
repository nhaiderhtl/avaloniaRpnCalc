namespace RpnCalc.Exceptions;

public class RpnDivisionByZeroException : Exception
{
    public RpnDivisionByZeroException() : base("Division by zero")
    {
    }

    public RpnDivisionByZeroException(string message) : base(message)
    {
    }
}