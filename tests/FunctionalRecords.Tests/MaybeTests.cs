using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalRecords.Tests;

public class MaybeTests
{
    [Fact]
    public void ImplicitConversionFromNull_SetsMaybeToNotSet()
    {
        Maybe<string> s = null;

        s.Should().NotBeNull();
    }

    [Fact]
    public void DefaultConstructor_SetsValueToNotSet()
    {
        Maybe<int> i = Maybe<int>.None;
        i.IsNone.Should().BeTrue();
        i.IsSome.Should().BeFalse();
    }

    [Fact]
    public void MaybeNotSet_IsSome_ReturnsFalse()
    {
        Maybe<string> s = null;

        s.Should().NotBeNull();

        s.IsSome.Should().BeFalse();
    }

    [Fact]
    public void TwoSameMaybeObjects_AreEqual_ReturnsTrue()
    {
        Maybe<string> s1 = "123";
        Maybe<string> s2 = "123";

        (s1 == s2).Should().BeTrue();
    }

    [Fact]
    public void TwoNoneMaybeObjects_AreEqual_ReturnsTrue()
    {
        Maybe<string> s1 = Maybe<string>.None;
        Maybe<string> s2 = Maybe<string>.None;

        (s1 == s2).Should().BeTrue();
    }

    [Fact]
    public void ValueSet_ToString_DoesNotThrowException()
    {
        string s = Maybe<int>.From(1).ToString();
        s.Should().NotBeNull();
    }

    [Fact]
    public void ValueNotSet_ToString_DoesNotThrowException()
    {
        string s = Maybe<int>.None.ToString();
        s.Should().NotBeNull();
    }

    [Fact]
    public void MaybeNotSet_IsNone_ReturnsTrue()
    {
        Maybe<string> s = null;

        s.Should().NotBeNull();

        s.IsNone.Should().BeTrue();
    }

    [Fact]
    public void MaybeSet_IsSome_ReturnsTrue()
    {
        Maybe<string> s = "";

        s.Should().NotBeNull();

        s.IsSome.Should().BeTrue();
    }

    [Fact]
    public void MaybeSet_IsNone_ReturnsFalse()
    {
        Maybe<string> s = "";

        s.Should().NotBeNull();

        s.IsNone.Should().BeFalse();
    }

    [Fact]
    public void MaybeNotSet_Value_ThrowsException()
    {
        Maybe<string> s = null;
        Action a = () => { string g = s.Value; };
        a.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MaybeSet_Value_ReturnsValue()
    {
        Maybe<string> s = "a";
        string g = s.Value;
        g.Should().Be("a");
    }

    [Fact]
    public void MaybeNotSet_ValueOrDefault_ReturnsDefault()
    {
        Maybe<int> i = Maybe<int>.None;
        i.ValueOrDefault.Should().Be(0);
    }

    [Fact]
    public void MaybeSet_ValueOrDefault_ReturnsValue()
    {
        Maybe<int> i = Maybe<int>.From(123);
        i.ValueOrDefault.Should().Be(123);
    }

    [Fact]
    public void MaybeNotSet_ExecuteAction_CallsWhenNotSetAction()
    {
        Maybe<string> s = null;

        int value = -2;

        s.Match(
            some: new Action<string>(sArg => value = sArg.Length),
            none: new Action(() => value = -1)
        );

        value.Should().Be(-1);
    }

    [Fact]
    public void MaySet_ExecuteAction_CallsWhenSetAction()
    {
        Maybe<string> s = "1234";

        int value = -2;

        s.Match(
            some: new Action<string>(s => value = s.Length),
            none: new Action(() => value = -1)
        );

        value.Should().Be(4);
    }

    [Fact]
    public void MaybeNotSet_ExecuteFunction_CallsWhenNotSetFunction()
    {
        Maybe<string> s = null;

        int value = s.Match(
            some: sArg => sArg.Length,
            none: () => -1
        );

        value.Should().Be(-1);
    }

    [Fact]
    public void MaySet_ExecuteFunction_CallsWhenSetFunction()
    {
        Maybe<string> s = "1234";

        int value = s.Match(
            some: sArg => sArg.Length,
            none: () => -1
        );

        value.Should().Be(4);
    }

    [Fact]
    public void MaybeNotSet_ExecuteOnlyWhenSetAction_DoesNotCallAction()
    {
        Maybe<string> s = null;

        int value = -2;

        s.Match(
            some: new Action<string>(sArg => value = sArg.Length)
        );

        value.Should().Be(-2);
    }

    [Fact]
    public void MaybeNotSet_ExecuteOnlyWhenNotSetAction_CallAction()
    {
        Maybe<string> s = null;

        int value = -2;

        s.Match(
            none: new Action(() => value = 3)
        );

        value.Should().Be(3);
    }

    [Fact]
    public void MaySet_ExecuteOnlyWhenSetAction_CallsWhenSetAction()
    {
        Maybe<string> s = "1234";

        int value = -2;

        s.Match(
            some: new Action<string>(s => value = s.Length)
        );

        value.Should().Be(4);
    }

    [Fact]
    public void MaySet_ExecuteOnlyWhenNotSetAction_DoesNotCallsAction()
    {
        Maybe<string> s = "1234";

        int value = -2;

        s.Match(
            none: new Action(() => value = -3)
        );

        value.Should().Be(-2);
    }

    [Fact]
    public async Task MaybeSet_ExecuteAsyncFunc_ReturnsResult()
    {
        Maybe<string> s = "1234";

        int value = await s.Match(
            some: async (sArg) =>
            {
                await Task.Delay(0);
                return sArg.Length;
            },
            none: async () =>
            {
                await Task.Delay(0);
                return -2;
            }
        );

        value.Should().Be(4);
    }

    [Fact]
    public async Task MaybeNotSet_ExecuteAsyncFunc_ReturnsResult()
    {
        Maybe<string> s = Maybe<string>.None;

        int value = await s.Match(
            some: async (sArg) =>
            {
                await Task.Delay(0);
                return sArg.Length;
            },
            none: async () =>
            {
                await Task.Delay(0);
                return -2;
            }
        );

        value.Should().Be(-2);
    }
}
