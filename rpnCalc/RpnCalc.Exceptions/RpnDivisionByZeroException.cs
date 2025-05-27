namespace RpnCalc.Exceptions;

public class RpnDivisionByZeroException : RpnException
{
    public RpnDivisionByZeroException() : base("Division by zero")
    {
    }

    public RpnDivisionByZeroException(string message) : base(message)
    {
    }
}