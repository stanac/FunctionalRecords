using System;

namespace FunctionalRecords.Serialization.Json.Tests.TestObjects;

public static class TupleTestObject
{
    public static TupleTestObject<Guid> Create1()
    {
        return new TupleTestObject<Guid>
        {
            Value = ValueTuple.Create(Guid.NewGuid())
        };
    }

    public static TupleTestObject<Guid, string> Create2()
    {
        return new TupleTestObject<Guid, string>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test")
        };
    }

    public static TupleTestObject<Guid, string, int> Create3()
    {
        return new TupleTestObject<Guid, string, int>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test", 23)
        };
    }

    public static TupleTestObject<Guid, string, int, bool> Create4()
    {
        return new TupleTestObject<Guid, string, int, bool>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test", 23, true)
        };
    }

    public static TupleTestObject<Guid, string, int, bool, long> Create5()
    {
        return new TupleTestObject<Guid, string, int, bool, long>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test", 23, true, long.MaxValue / 2)
        };
    }

    public static TupleTestObject<Guid, string, int, bool, long, string> Create6()
    {
        return new TupleTestObject<Guid, string, int, bool, long, string>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test", 23, true, long.MaxValue / 2, "a")
        };
    }

    public static TupleTestObject<Guid, string, int, bool, long, string, string> Create7()
    {
        return new TupleTestObject<Guid, string, int, bool, long, string, string>
        {
            Value = ValueTuple.Create(Guid.NewGuid(), "test", 23, true, long.MaxValue / 2, "a", "b")
        };
    }
}

public class TupleTestObject<T1>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1> Value { get; set;}
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2> Value { get; set; }
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2, T3>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2, T3> Value { get; set; }
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2, T3, T4>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2, T3, T4> Value { get; set; }
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2, T3, T4, T5>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2, T3, T4, T5> Value { get; set; }
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2, T3, T4, T5, T6>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2, T3, T4, T5, T6> Value { get; set; }
    public int I { get; set; } = 44;
}

public class TupleTestObject<T1, T2, T3, T4, T5, T6, T7>
{
    public string S { get; set; } = "S";
    public ValueTuple<T1, T2, T3, T4, T5, T6, T7> Value { get; set; }
    public int I { get; set; } = 44;
}