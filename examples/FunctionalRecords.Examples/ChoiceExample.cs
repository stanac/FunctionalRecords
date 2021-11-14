namespace FunctionalRecords.Examples;

public static class ChoiceExample
{
    public static void Example()
    {
        Choice<string, int> stringOrInt1 = 4;
        Choice<string, int> stringOrInt2 = "abcd";
        Choice<string, int> stringOrInt3 = Choice<string, int>.From("a");
        Choice<string, int> stringOrInt4 = Choice<string, int>.From(3);

        Choice<int, float, double> choice3 = Choice<int, float, double>.From(3);
        Choice<int, float, double, decimal> choice4 = Choice<int, float, double, decimal>.From(3);
        Choice<int, float, double, decimal, string> choice5 = Choice<int, float, double, decimal, string>.From(3);
        Choice<int, float, double, decimal, string, bool> choice6 = 3;

        Console.WriteLine(choice6.Value); // Writes "3";

        // call Func<T1, TResult> or Func<T2, TResult>
        int stringLength1 = stringOrInt1.Match(
            s => s.Length,
            i => i
            );

        bool isInt = stringOrInt1.Is<int>(); // true
        bool isString = stringOrInt1.Is<string>(); // false

        // call Action<T1> or Action<T2>
        stringOrInt2.Match(
            s => Console.WriteLine($"{nameof(stringOrInt2)} is string {s}"),
            i => Console.WriteLine($"{nameof(stringOrInt2)} is int {i}")
        );

        Type t = stringOrInt2.GetChosenType(); // returns typeof(string)
    }
}
