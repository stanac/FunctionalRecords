using System;

namespace FunctionalRecords.Serialization.Json.Tests.TestObjects;

public class ChoiceTestObject
{
    public Choice<int, string> C20A { get; set; }
    public Choice<int, string> C20B { get; set; }
    public int Id { get; set; }
    public Choice<int, float> C21 { get; set; }
    public DateTime Dt1 { get; set; }
    public Choice<int, float> C22 { get; set; }
    public Guid Id1 { get; set; }
    public Choice<bool, string, PersonName> C31 { get; set; }
    public Choice<bool, string, PersonName> C32 { get; set; }
    public string TestString1 { get; set; }
    public Choice<bool, string, PersonName> C33 { get; set; }
    public string NullString { get; set; }
    public Choice<bool, string, PersonName, Guid> C41 { get; set; }
    public Choice<bool, string, PersonName, Guid> C42 { get; set; }
    public string EmptyString1 { get; set; }
    public Choice<bool, string, PersonName, Guid> C43 { get; set; }
    public string NullString2 { get; set; }
    public Choice<bool, string, PersonName, Guid> C44 { get; set; }
    public string WhitespaceString1 { get; set; }
    public Choice<bool, string, PersonName, Guid, float> C51 { get; set; }
    public Choice<bool, string, PersonName, Guid, float> C52 { get; set; }
    public Choice<bool, string, PersonName, Guid, float> C53 { get; set; }
    public string WhitespaceString2 { get; set; }
    public Choice<bool, string, PersonName, Guid, float> C54 { get; set; }
    public Choice<bool, string, PersonName, Guid, float> C55 { get; set; }
    public string WhitespaceString3 { get; set; }
    public string IdString1 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C61 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C62 { get; set; }
    public string IdString2 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C63 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C64 { get; set; }
    public bool SomeBool1 { get; set; }
    public bool SomeBool2 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C65 { get; set; }
    public Choice<bool, string, PersonName, Guid, float, int> C66 { get; set; }

    public static ChoiceTestObject CreateTestObject()
    {
        return new ChoiceTestObject
        {
            C20A = 31,
            C20B = "abc",
            C21 = 0.1f,
            C22 = -543,
            C31 = true,
            C32 = "s13",
            C33 = new PersonName(("First", "Last")),
            C41 = true,
            C42 = "s14",
            C43 = new PersonName(("A", "B")),
            C44 = Guid.NewGuid(),
            C51 = false,
            C52 = "s15",
            C53 = new PersonName(("C", "D")),
            C54 = Guid.NewGuid(),
            C55 = 0.11f,
            C61 = true,
            C62 = "s16",
            C63 = new PersonName(("Ee", "FF")),
            C64 = Guid.NewGuid(),
            C65 = 0.11f,
            C66 = -345,
            Dt1 = DateTime.Today,
            EmptyString1 = "",
            Id = 4555,
            Id1 = Guid.NewGuid(),
            IdString1 = Guid.NewGuid().ToString(),
            IdString2 = Guid.NewGuid().ToString(),
            NullString = null,
            NullString2 = null,
            SomeBool1 = true,
            SomeBool2 = false,
            TestString1 = "test",
            WhitespaceString1 = " ",
            WhitespaceString2 = "  ",
            WhitespaceString3 = "   "
        };
    }
}