using System.Diagnostics.CodeAnalysis;

namespace FunctionalRecords;

[ExcludeFromCodeCoverage]
public class OperationFailedException : Exception
{
    public IReadOnlyList<string> ValidationErrors { get; private set; }
        
    public OperationFailedException(params string[] errors)
        : this(errors.ToList())
    {
    }

    public OperationFailedException(IEnumerable<string> errors)
        : this(errors.ToList())
    {
    }

    public OperationFailedException(List<string>? errors)
        : base(GetErrorMessage(errors))
    {
        ValidationErrors = errors ?? new List<string>();
    }

    public OperationFailedException(string error, Exception? innerException)
        : this(new [] { error }, innerException)
    {
    }

    public OperationFailedException(IEnumerable<string> errors, Exception? innerException)
        : this(errors.ToList(), innerException)
    {
    }

    public OperationFailedException(List<string>? errors, Exception? innerException)
        : base(GetErrorMessage(errors), innerException)
    {
        ValidationErrors = errors ?? new List<string>();
    }

    private static string GetErrorMessage(List<string>? errors)
    {
        if (errors == null || errors.Count == 0)
        {
            return "Operation failed error";
        }

        if (errors.Count == 1)
        {
            return $"Operation failed error: {errors[0]}";
        }

        return $"Operation failed errors: {string.Join(";", errors)}";
    }
}