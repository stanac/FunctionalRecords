using FluentAssertions;

namespace FunctionalRecords.Tests;

public class Choice2Tests
{
    [Fact]
    public void Choice2_SetToT1_SetsSelectedIndexTo1()
    {
        Choice<int, string> c = 3;
        c.SelectedIndex.Should().Be(1);
    }

    [Fact]
    public void Choice2_SetToT2_SetsSelectedIndexTo2()
    {
        Choice<int, string> c = "a";
        c.SelectedIndex.Should().Be(2);
    }

    [Fact]
    public void Choice2_SetToT1_IsT1_ReturnsTrue()
    {
        Choice<int, string> c = 3;
        c.Is<int>().Should().BeTrue();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeFalse();
    }

    [Fact]
    public void Choice2_SetToT2_IsT2_ReturnsTrue()
    {
        Choice<int, string> c = "a";
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeTrue();
        c.Is<Guid>().Should().BeFalse();
    }

    [Fact]
    public void Choice2_SetToT1_MatchActionCallsAction1()
    {
        Choice<int, string> c = 3;
        bool called1 = false;
        bool called2 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true)
            );

        called1.Should().BeTrue();
        called2.Should().BeFalse();
    }

    [Fact]
    public void Choice2_SetToT2_MatchActionCallsAction2()
    {
        Choice<int, string> c = "";
        bool called1 = false;
        bool called2 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeTrue();
    }

    [Fact]
    public void Choice2_SetToT1_MatchFuncCallsFunc1()
    {
        Choice<int, string> c = 3;
        
        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2)
            );

        called.Should().Be(1);
    }

    [Fact]
    public void Choice2_SetToT2_MatchFuncCallsFunc2()
    {
        Choice<int, string> c = "";

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2)
            );

        called.Should().Be(2);
    }

    [Fact]
    public async Task Choice2_SetToT1_MatchAsyncTaskFuncCallsFunc1()
    {
        Choice<int, string> c = 0;

        int called = await c.Match(
            new Func<int, Task<int>>(async _ => { await Task.Delay(0); return 1; }),
            new Func<string, Task<int>>(async _ => { await Task.Delay(0); return 2; })
            );

        called.Should().Be(1);
    }

    [Fact]
    public async Task Choice2_SetToT2_MatchAsyncTaskFuncCallsFunc2()
    {
        Choice<int, string> c = "";

        int called = await c.Match(
            new Func<int, Task<int>>(async _ => { await Task.Delay(0); return 1; }),
            new Func<string, Task<int>>(async _ => { await Task.Delay(0); return 2; })
            );

        called.Should().Be(2);
    }

    [Fact]
    public void Choice2_SetToT1Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<string, int> c = Choice<string, int>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice2_SetToT2Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, string> c = Choice<int, string>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice2_SetToT1_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string> c = 2;
        c.GetChosenType().Should().Be(typeof(int));
    }
    
    [Fact]
    public void Choice2_SetToT2_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string> c = "a";
        c.GetChosenType().Should().Be(typeof(string));
    }

    [Fact]
    public void Choice2_SetTo1_Value_ReturnsExpectedValue()
    {
        Choice<int, string> c = 2;
        c.Value.Should().Be(2);
    }

    [Fact]
    public void Choice2_SetTo2_Value_ReturnsExpectedValue()
    {
        Choice<int, string> c = "a";
        c.Value.Should().Be("a");
    }
}
