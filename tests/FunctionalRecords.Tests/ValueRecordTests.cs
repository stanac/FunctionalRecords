using System;
using FluentAssertions;
using Xunit;

namespace FunctionalRecords.Tests;

public class ValueRecordTests
{
    [Theory]
    [InlineData("", "Doe")]
    [InlineData("Jane", null)]
    [InlineData(" ", " ")]
    public void ValidationErrorExists_ThrowsValidationException(string firstName, string lastName)
    {
        // ReSharper disable once ObjectCreationAsStatement
        Action a = () => new PersonName((firstName, lastName));

        a.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Constructor_CallsTransformValueBeforeSettingValue()
    {
        PersonName pn = new PersonName(("Jane", "Doe"));
        pn.Value.LastName.Should().Be("DOE");
    }

    [Fact]
    public void TwoStructurallyEqualObject_AreEqual_ReturnsTrue()
    {
        PersonName pn1 = new PersonName(("Jane", "Doe"));
        PersonName pn2 = new PersonName(("Jane", "Doe"));

        pn1.Equals(pn2).Should().BeTrue();
        (pn1 == pn2).Should().BeTrue();
    }

    [Fact]
    public void TransformValueNotOverriden_DoesNotTransformValue()
    {
        PositiveInteger i = 3;
        i.Value.Should().Be(3);
    }
}
