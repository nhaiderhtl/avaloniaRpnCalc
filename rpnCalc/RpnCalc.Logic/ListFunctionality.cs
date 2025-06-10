using System.Globalization;
using RpnCalc.Core;
using RpnCalc.Exceptions;

namespace RpnCalc.Logic;

public class ListFunctionality : IListFunctionality
{
    public int Length(List<double> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        return list.Count;
    }

    public double Sum(List<double> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        return list.Sum();
    }

    public double Average(List<double> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        if (list.Count == 0) throw new InvalidOperationException("Cannot get average of an empty list.");
        return list.Average();
    }

    public List<double> Map(List<double> list, string operation)
    {
        ArgumentNullException.ThrowIfNull(list);
        if (string.IsNullOrWhiteSpace(operation) || operation.Length < 2)
            throw new ArgumentException("Operation must be like '+N', '-N', '*N' or '/N'.", nameof(operation));

        var op = operation[0];
        if (!double.TryParse(operation.AsSpan(1), NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException("Could not parse number after operator.", nameof(operation));

        var result = new List<double>(list.Count);
        foreach (var x in list)
        {
            double transformed;
            switch (op)
            {
                case '+':
                    transformed = x + value;
                    break;
                case '-':
                    transformed = x - value;
                    break;
                case '*':
                    transformed = x * value;
                    break;
                case '/':
                    if (value == 0)
                        throw new RpnDivisionByZeroException("Cannot divide by zero.");
                    transformed = x / value;
                    break;
                default:
                    throw new ArgumentException("Operator must be +, -, * or /.", nameof(operation));
            }

            result.Add(transformed);
        }

        return result;
    }

    public List<double> ParseBracketList(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new RpnInvalidBracketException("Input cannot be empty. Use format: [1 2 3].");

        input = input.Trim();
        if (!input.StartsWith("[") || !input.EndsWith("]"))
            throw new RpnInvalidBracketException("List must start with ‘[’ and end with ‘]’.");

        var inner = input.Substring(1, input.Length - 2).Trim();
        if (inner == "")
            return new List<double>();

        var parts = inner.Split([';', '\t'], StringSplitOptions.RemoveEmptyEntries);
        var result = new List<double>(parts.Length);

        foreach (var token in parts)
        {
            if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                throw new RpnInvalidBracketException($"Could not parse ‘{token}’ as a number.");

            result.Add(number);
        }

        return result;
    }
}