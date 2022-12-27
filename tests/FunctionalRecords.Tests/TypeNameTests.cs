using FluentAssertions;

namespace FunctionalRecords.Tests;

public class TypeNameTests
{
    [Fact]
    public void PrimitiveType_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(int));

        tn.ShortName.Should().Be("Int32");
    }

    [Fact]
    public void NonPrimitiveName_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(TypeNameTests));

        tn.ShortName.Should().Be("TypeNameTests");
    }

    [Fact]
    public void Array_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(TypeNameTests[]));

        tn.ShortName.Should().Be("TypeNameTests[]");
    }

    [Fact]
    public void List_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(List<TypeNameTests>));

        tn.ShortName.Should().Be("List<TypeNameTests>");
    }

    [Fact]
    public void Dictionary_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(IDictionary<int, TypeNameTests>));

        tn.ShortName.Should().Be("IDictionary<Int32,TypeNameTests>");
    }

    [Fact]
    public void ComplexObject_GivesExpectedName()
    {
        TypeName tn = new TypeName(typeof(IDictionary<string, TypeNameTests[]>[]));

        tn.ShortName.Should().Be("IDictionary<String,TypeNameTests[]>[]");
    }

    [Fact]
    public void EqualsTypeName_ReturnsTrue()
    {
        TypeName t1 = new TypeName(typeof(int));
        TypeName t2 = new TypeName(typeof(int));
        TypeName t3 = new TypeName(typeof(string));

        t1.Equals(t2).Should().BeTrue();
        (t1 == t2).Should().BeTrue();
        (t1 != t2).Should().BeFalse();

        t1.Equals(t3).Should().BeFalse();
        (t1 == t3).Should().BeFalse();
        (t1 != t3).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void GetTypeFromName_NameNotSet_ThrowsException(string name)
    {
        Action a = () => TypeName.GetTypeFromName(name!, typeof(int), typeof(string));
        a.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeFromName_AvailableTypesEmpty_ThrowsException()
    {
        Action a = () => TypeName.GetTypeFromName("Int32", Type.EmptyTypes);
        a.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeFromName_AvailableTypesContainsSingleItem_ThrowsException()
    {
        Action a = () => TypeName.GetTypeFromName("Int32", typeof(string));
        a.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeFromName_AvailableTypesContainsDuplicate_ThrowsException()
    {
        Action a = () => TypeName.GetTypeFromName("Int32", typeof(string), typeof(string));
        a.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeFromName_ContainsDuplicateTypeName_ThrowsException()
    {
        Type type1 = typeof(TestObjects.PersonName);
        Type type2 = typeof(TestObjects.Nested.PersonName);

        Action a = () => TypeName.GetTypeFromName("Int32", type1, type2);
        a.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeFromName_ValidName_ReturnsExpectedType()
    {
        Type type1 = typeof(TestObjects.PersonName[]);
        Type type2 = typeof(List<int>);
        Type type3 = typeof(Dictionary<int, IList<object>>);

        string name = new TypeName(type2).ShortName;

        Type t = TypeName.GetTypeFromName(name, type1, type2, type3);
        t.Should().Be(type2);
    }
}