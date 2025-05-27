namespace RpnCalc.Exceptions;

public class RpnStackUnderflowException : RpnException
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