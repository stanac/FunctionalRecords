using FluentAssertions;
using System;
using Xunit;

namespace FunctionalRecords.Tests;

public class Choice3Tests
{
    [Fact]
    public void Choice3_SetToT1_SetsSelectedIndexTo1()
    {
        Choice<int, string, Guid> c = 3;
        c.SelectedIndex.Should().Be(1);
    }

    [Fact]
    public void Choice3_SetToT2_SetsSelectedIndexTo2()
    {
        Choice<int, string, Guid> c = "a";
        c.SelectedIndex.Should().Be(2);
    }

    [Fact]
    public void Choice3_SetToT3_SetsSelectedIndexTo3()
    {
        Choice<int, string, Guid> c = Guid.Empty;
        c.SelectedIndex.Should().Be(3);
    }

    [Fact]
    public void Choice3_SetToT1_IsT1_ReturnsTrue()
    {
        Choice<int, string, Guid> c = 3;
        c.Is<int>().Should().BeTrue();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeFalse();
    }

    [Fact]
    public void Choice3_SetToT2_IsT2_ReturnsTrue()
    {
        Choice<int, string, Guid> c = "a";
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeTrue();
        c.Is<Guid>().Should().BeFalse();
    }

    [Fact]
    public void Choice3_SetToT3_IsT3_ReturnsTrue()
    {
        Choice<int, string, Guid> c = Guid.Empty;
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeTrue();
    }

    [Fact]
    public void Choice3_SetToT1_MatchActionCallsAction1()
    {
        Choice<int, string, Guid> c = 3;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true)
            );

        called1.Should().BeTrue();
        called2.Should().BeFalse();
        called3.Should().BeFalse();
    }

    [Fact]
    public void Choice3_SetToT2_MatchActionCallsAction2()
    {
        Choice<int, string, Guid> c = "";
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeTrue();
        called3.Should().BeFalse();
    }

    [Fact]
    public void Choice3_SetToT3_MatchActionCallsAction3()
    {
        Choice<int, string, Guid> c = Guid.Empty;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeFalse();
        called3.Should().BeTrue();
    }

    [Fact]
    public void Choice3_SetToT1_MatchFuncCallsFunc1()
    {
        Choice<int, string, Guid> c = 3;
        
        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3)
            );

        called.Should().Be(1);
    }

    [Fact]
    public void Choice3_SetToT2_MatchFuncCallsFunc2()
    {
        Choice<int, string, Guid> c = "";

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3)
            );

        called.Should().Be(2);
    }

    [Fact]
    public void Choice3_SetToT3_MatchFuncCallsFunc3()
    {
        Choice<int, string, Guid> c = Guid.Empty;

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3)
            );

        called.Should().Be(3);
    }

    [Fact]
    public void Choice3_SetToT1Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<string, int> c = Choice<string, int>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice3_SetToT2Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, string, Guid> c = Choice<int, string, Guid>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice3_SetToT3Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, Guid, string> c = Choice<int, Guid, string>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice3_SetToT1_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float> c = 2;
        c.GetChosenType().Should().Be(typeof(int));
    }

    [Fact]
    public void Choice3_SetToT2_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float> c = "a";
        c.GetChosenType().Should().Be(typeof(string));
    }

    [Fact]
    public void Choice3_SetToT3_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float> c = 0.2f;
        c.GetChosenType().Should().Be(typeof(float));
    }

    [Fact]
    public void Choice3_SetTo1_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float> c = 2;
        c.Value.Should().Be(2);
    }

    [Fact]
    public void Choice3_SetTo2_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float> c = "a";
        c.Value.Should().Be("a");
    }

    [Fact]
    public void Choice3_SetTo3_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float> c = 1.1f;
        c.Value.Should().Be(1.1f);
    }
}
