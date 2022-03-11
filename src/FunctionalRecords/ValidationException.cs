using System.Diagnostics.CodeAnalysis;

namespace FunctionalRecords;

[ExcludeFromCodeCoverage]
public class ValidationException : Exception
{
    public IReadOnlyList<string> ValidationErrors { get; private set; }
    public string? OnTypeFullName { get; }

    public ValidationException(params string[] errors)
        : this(errors.ToList())
    {
    }

    public ValidationException(IEnumerable<string> errors)
        : this(errors.ToList())
    {
    }

    public ValidationException(List<string>? errors)
        : base(GetErrorMessage(errors))
    {
        ValidationErrors = errors ?? new List<string>();
    }

    public ValidationException(Type onType, params string[] errors)
        : this(onType, errors.ToList())
    {
    }

    public ValidationException(Type onType, IEnumerable<string> errors)
        : this(onType, errors.ToList())
    {
    }

    public ValidationException(Type? onType, List<string>? errors)
        : base(GetErrorMessage(onType, errors))
    {
        if (onType != null)
        {
            OnTypeFullName = onType.FullName;
        }

        ValidationErrors = errors ?? new List<string>();
    }

    private static string GetErrorMessage(List<string>? errors)
    {
        if (errors == null || errors.Count == 0)
        {
            return "Validation error";
        }

        if (errors.Count == 1)
        {
            return $"Validation error: {errors[0]}";
        }

        return $"Validation errors: {string.Join(";", errors)}";
    }

    private static string GetErrorMessage(Type? onType, List<string>? errors)
    {
        if (onType == null)
        {
            return GetErrorMessage(errors);
        }

        if (errors == null || errors.Count == 0)
        {
            return $"Validation error on type {onType.Name}";
        }

        if (errors.Count == 1)
        {
            return $"Validation error on type {onType.Name}: {errors[0]}";
        }

        return $"Validation errors on type {onType.Name}: {string.Join(";", errors)}";
    }
}
