namespace RpnCalc.Core;

public interface IListFunctionality
{
    /// <summary>
    /// Returns the number of elements in the list.
    /// </summary>
    int Length(List<double> list);

    /// <summary>
    /// Returns the sum of all elements in the list.
    /// </summary>
    double Sum(List<double> list);

    /// <summary>
    /// Returns the average of the elements in the list.
    /// </summary>
    double Average(List<double> list);

    /// <summary>
    /// Applies a provided operation to each element and returns a new list of results.
    /// </summary>
    List<double> Map(List<double> list, string operation);

    /// <summary>
    /// Parses a string to a list of numbers
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    List<double> ParseBracketList(string input);
}