namespace FunctionalRecords.Examples;

public static class MaybeExample
{
    public static void Example()
    {
        Maybe<string> s0 = Maybe<string>.None;
        Maybe<string> s1 = null;
        Maybe<string> s2 = Maybe<string>.From(null);
        Maybe<string> s3 = Maybe<string>.From("s");

        Maybe<string> s = "a";

        if (s.IsSome) // true when set
        {
            Console.WriteLine($"S value: {s.Value}");
        }
        
        if (s.IsNone) // true when not set
        {
            Console.WriteLine("S is not set");
        }

        string valueOrDefault = s.ValueOrDefault; // returns value if present or default of <T>

        s.Match(() => Console.WriteLine("s is set")); // action called only when s has value

        int sLength = s.Match(
            some: value => value.Length,
            none: () => -1
            );

        Console.WriteLine($"sLength (-1 if null): {sLength}");
    }
}
