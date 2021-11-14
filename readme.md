# FunctionalRecords

C# library with functional records: Maybe, Choice, ValueRecord (value object), Result

## Install from nuget

```
dotnet add package FunctionalRecords
```

Support for `System.Text.Json` serialization

```
dotnet add package FunctionalRecords.Serialization.Json
```

## Hot to use

```
ℹ All examples are available in /examples directory
```


### Maybe&lt;T&gt;

`Maybe<T>` is a `readonly record struct` of `<T>`. Can hold a value or be `None`. Has members:
- `bool IsSome` - true if set
- `bool IsNone` - false if not set
- `void Match(Action<T>, Action)` - Calls first parameter `Action<T>` if `IsSome`, and if `IsNone` calls second parameter `Action`
- `void Match(Action<T>)` - Calls `Action<T>` if `IsSome`
- `void Match(Action)` - Calls `Action` if `IsNone`
- `T? ValueOrDefault` - returns value or default value if IsNone
- `T Value` - returns value if IsSome, otherwise throws `InvalidOperationException`
- `static Maybe<T> None` - returns `None` of `Maybe<T>`
- `static Maybe<T> From(T)` - returns `Maybe<T>` from value
- `static implicit operator Maybe<T>(T)` - converts value of `T` to `Maybe<T>`

```csharp
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
string value = s.Value; // !!!⚠!!! Exception will be thrown if IsNone

s.Match(() => Console.WriteLine("s is set")); // action called only when s has value

int sLength = s.Match(
    some: value => value.Length,
    none: () => -1
    );

Console.WriteLine($"sLength (-1 if null): {sLength}");

```

### Result and Result&lt;T&gt;

`Result` and `Result<T>` are `readonly record struct`s. Both can be success or failure.

`Result` members:
- `bool IsSuccess` - true or false
- `bool IsFailure` - opposite of `IsSuccess`
- `IReadOnlyList<string> Errors` - error list, empty by default, cannot be null
- `Maybe<Exception> Exception` - optional exception

`Result<T>` members:
- `bool IsSuccess` - true or false
- `bool IsFailure` - opposite of `IsSuccess`
- `IReadOnlyList<string> Errors` - error list, empty by default, cannot be null
- `Maybe<Exception> Exception` - optional exception
- `T Value` - balue of `T`

`Result` methods:
- `static Result Success()`
- `static Result<TValue> Success<TValue>(TValue value)`
- `static Result Failure()`
- `static Result Failure(params string[] errors)`
- `static Result Failure(IEnumerable<string> errors)`
- `static Result Failure(Maybe<Exception> exception, params string[] errors)`
- `static Result Failure(Maybe<Exception> exception, IEnumerable<string> errors)`
- `static Result Failure(Maybe<Exception> exception, IReadOnlyList<string> errors)`
- `static Result<TValue> Failure<TValue>(params string[] errors)`
- `static Result<TValue> Failure<TValue>(IEnumerable<string> errors)`
- `static Result<TValue> Failure<TValue>(IReadOnlyList<string> errors)`
- `static Result<TValue> Failure<TValue>(Exception exception, IEnumerable<string> errors)`
- `static Result<TValue> Failure<TValue>(Exception exception, params string[] errors)`
- `static Result<TValue> Failure<TValue>(Exception exception, IReadOnlyList<string> errors)`

```csharp
Result rSuccess = Result.Success();
Result rFailure = Result.Failure();

rFailure = Result.Failure("Error description 1", "Error description 2");
rFailure = Result.Failure(new FileNotFoundException("File missing", "myFile.txt"), "Error description 1", "Error description 2");

Result<int> rSuccessWithValue = Result.Success<int>(4);
Result<int> rFailureWithNoValue = Result.Failure<int>("Something went wrong");
Console.WriteLine(rSuccessWithValue.Value.ValueOrDefault);

Result<int> rFailureWithValue = Result.Failure<int>(new InvalidOperationException("message"), "error 1", "error 2", "error3");

```

### ValueRecord&lt;T&gt;

`ValueRecord` is `abstract record` that can be used to implement DDD-like Value Objects.

Members:
- `ValueRecord(T value)` - constructor
- `T Value` holds value
- `abstract IEnumerable<string> GetValidationErrors(T value)` - Must be implemented and should return validation errors
- `virtual void AfterSuccessfulValidation()` - Can be implemented to throw custom exception after validation passes
- `Validate(T value)` - called in constructor
   - If `GetValidationErrors` returns anything new `FunctionalRecords.ValidationException` will be thrown
   - If validation passes `AfterSuccessfulValidation` is called
- `virtual T TransformValue(T value)` - optionally value can be transformed before validated and assigned to `Value` property
   - If implemented transformed value is passed to `Validate` method

Simple example where we want to have integer record that is always greater than zero:

```csharp
public record PositiveInteger : ValueRecord<int>
{
    public PositiveInteger(int value) : base(value)
    {
    }

    protected override IEnumerable<string> GetValidationErrors(int value)
    {
        if (value < 1)
        {
            yield return "Value cannot be less than 1";
        }
    }

    public static implicit operator PositiveInteger(int i) => new(i);
}
```

More complex example where persons last name is transformed to be upper-case:

```csharp
// ⚠ must be a record and NOT a class
public record PersonName : ValueRecord<(string FirstName, string LastName)>
{
    public PersonName((string FirstName, string LastName) value) : base(value)
    {
    }
    
    protected override IEnumerable<string> GetValidationErrors((string FirstName, string LastName) value)
    {
        // abstract method
        // if something is returned from this method FunctionalRecords.ValidationException will be thrown

        if (string.IsNullOrEmpty(value.FirstName))
        {
            yield return "FirstName not set";
        }

        if (string.IsNullOrEmpty(value.LastName))
        {
            yield return "LastName not set";
        }
    }

    protected override void AfterSuccessfulValidation()
    {
        // virtual method, optionally throw some other exception here
        // called only if GetValidationErrors does not return anything
    }

    protected override (string FirstName, string LastName) TransformValue((string FirstName, string LastName) value)
    {
        // virtual method called before validation
        // optionally transform object
        return (value.FirstName, value.LastName?.ToUpper());
    }

    // optionally implement conversion
    public static implicit operator PersonName ((string, string) value) => new(value);

    // optionally override to string
    public override string ToString() => $"Person: {Value.LastName}, {Value.FirstName}";
}

```

### Choice&lt;T1, T2&gt;

Records:
- `Choice<T1, T2>`
- `Choice<T1, T2, T3>`
- `Choice<T1, T2, T3, T4>`
- `Choice<T1, T2, T3, T4, T5>`
- `Choice<T1, T2, T3, T4, T5. T6>`

Choice records are `readonly record struct`s. They hold one of given values.

Members:
- `static Choice<T1, T2> From(T1 value)` - value cannot be null
- `static Choice<T1, T2> From(T2 value)` - value cannot be null
- `Value` - value assigned to choice
- `Type GetChosenType()` - return `typeof(T1)` or `typeof(T2)`
- `Is<T>()` - returns `true` if `T` is of give type of `Value`
- `void MatchMatch(Action<T1> matchT1, Action<T2> matchT2)` - calls one of the action, dependeing on chosen type
- `TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2)` - calls one of the functions, dependeing on chosen type

```csharp
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

```

### Serialization

Package `FunctionalRecords.Serialization.Json` needs to be referenced if (de)serialization of records is required.

```csharp
public static class SerializationExample
{
    public class JsonTestObj
    {
        public ValueRecordExample.PersonName PersonName { get; set; }
        public Maybe<int> MaybeInt1 { get; set; }
        public Maybe<int> MaybeInt2 { get; set; }
        public Result<Guid> Result { get; set; }
        public Choice<string, int> Choice2 { get; set; }
    }

    public static void Example()
    {
        JsonTestObj t = new JsonTestObj
        {
            PersonName = ("Jane", "Doe"),
            Choice2 = "Somethig",
            Result = Result.Success(Guid.NewGuid()),
            MaybeInt1 = Maybe<int>.None,
            MaybeInt2 = -5548
        };

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
        serializerOptions.WriteIndented = true;
        foreach (JsonConverter c in FunctionalRecords.Serialization.Json.Converters.AllConverters)
        {
            serializerOptions.Converters.Add(c);
        }

        string json = JsonSerializer.Serialize(t, serializerOptions);
        Console.WriteLine(json);
    }
}
```

`ValueTuple`s are serialized as JSON arrays, `Maybe` is serialized as `null` or as value, `Choice` as JSON array where
first element is selected type (1-indexed) and second element is value.

Console output from previous code:

```json
{
  "PersonName": {
    "Value": [
      "Jane",
      "DOE"
    ]
  },
  "MaybeInt1": null,
  "MaybeInt2": -5548,
  "Result": {
    "IsSuccess": true,
    "Exception": null,
    "Errors": [],
    "Value": "1f72f41b-484a-4001-82d0-6bf7d3f8f571"
  },
  "Choice2": [
    1,
    "Somethig"
  ]
}
```