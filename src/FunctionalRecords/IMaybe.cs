namespace FunctionalRecords;

public interface IMaybe
{
    bool IsSome { get; }
    bool IsNone { get; }
}