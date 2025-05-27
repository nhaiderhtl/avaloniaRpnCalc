namespace RpnCalc.Exceptions;

public class RpnStackUnderflowException : Exception
{
    public RpnStackUnderflowException()
        : base("Insufficient items in the RPN stack for this operation.")
    {
    }

    public RpnStackUnderflowException(string message)
        : base(message)
    {
    }
}