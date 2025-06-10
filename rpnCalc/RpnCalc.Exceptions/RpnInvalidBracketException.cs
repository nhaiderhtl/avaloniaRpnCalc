namespace RpnCalc.Exceptions;

public class RpnInvalidBracketException : RpnException
{
    public RpnInvalidBracketException() : base("Invalid bracket")
    {

    }

    public RpnInvalidBracketException(string message) : base(message)
    {

    }
}