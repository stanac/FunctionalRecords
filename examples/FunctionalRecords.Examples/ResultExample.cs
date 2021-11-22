namespace FunctionalRecords.Examples;

public static class ResultExample
{
    public static void Example()
    {
        Result rSuccess = Result.Success();
        Result rFailure = Result.Failure();

        rFailure = Result.Failure("Error description 1", "Error description 2");
        rFailure = Result.Failure(new FileNotFoundException("File missing", "myFile.txt"), "Error description 1", "Error description 2");

        Result<int> rSuccessWithValue = Result.Success<int>(4);
        Result<int> rFailureWithNoValue = Result.Failure<int>("Something went wrong");
        Console.WriteLine(rSuccessWithValue.Value.ValueOrDefault);

        Result<int> rFailureWithValue = Result.Failure<int>(new InvalidOperationException("message"), "error 1", "error 2", "error3");

        Result<string, FileFailures> r = Result.Failure<string, FileFailures>(FileFailures.FileTooLarge | FileFailures.WrongFileExtension, "Wrong extension and size");
        r.Is(FileFailures.FileTooLarge); // true
        r.Is(FileFailures.WrongFileExtension); // true
        r.Is(FileFailures.FileNotFound); // false
        FileFailures failureType = r.FailureType.Value; 
    }

    [Flags]
    public enum FileFailures
    {
        FileNotFound,
        FileTooLarge,
        WrongFileExtension
    }
}
