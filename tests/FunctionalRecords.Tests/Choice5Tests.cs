using FluentAssertions;

namespace FunctionalRecords.Tests;

public class Choice5Tests
{
    [Fact]
    public void Choice5_SetToT1_SetsSelectedIndexTo1()
    {
        Choice<int, string, Guid, bool, DateTime> c = 3;
        c.SelectedIndex.Should().Be(1);
    }

    [Fact]
    public void Choice5_SetToT2_SetsSelectedIndexTo2()
    {
        Choice<int, string, Guid, bool, DateTime> c = "a";
        c.SelectedIndex.Should().Be(2);
    }

    [Fact]
    public void Choice5_SetToT3_SetsSelectedIndexTo3()
    {
        Choice<int, string, Guid, bool, DateTime> c = Guid.Empty;
        c.SelectedIndex.Should().Be(3);
    }

    [Fact]
    public void Choice5_SetToT4_SetsSelectedIndexTo4()
    {
        Choice<int, string, Guid, bool, DateTime> c = false;
        c.SelectedIndex.Should().Be(4);
    }

    [Fact]
    public void Choice5_SetToT5_SetsSelectedIndexTo5()
    {
        Choice<int, string, Guid, bool, DateTime> c = DateTime.Now;
        c.SelectedIndex.Should().Be(5);
    }

    [Fact]
    public void Choice5_SetToT1_IsT1_ReturnsTrue()
    {
        Choice<int, string, Guid, bool, DateTime> c = 3;
        c.Is<int>().Should().BeTrue();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeFalse();
        c.Is<bool>().Should().BeFalse();
        c.Is<DateTime>().Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT2_IsT2_ReturnsTrue()
    {
        Choice<int, string, Guid, bool, DateTime> c = "a";
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeTrue();
        c.Is<Guid>().Should().BeFalse();
        c.Is<bool>().Should().BeFalse();
        c.Is<DateTime>().Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT3_IsT3_ReturnsTrue()
    {
        Choice<int, string, Guid, bool, DateTime> c = Guid.Empty;
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeTrue();
        c.Is<bool>().Should().BeFalse();
        c.Is<DateTime>().Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT4_IsT4_ReturnsTrue()
    {
        Choice<int, string, Guid, bool, DateTime> c = false;
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeFalse();
        c.Is<bool>().Should().BeTrue();
        c.Is<DateTime>().Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT5_IsT5_ReturnsTrue()
    {
        Choice<int, string, Guid, bool, DateTime> c = DateTime.Now;
        c.Is<int>().Should().BeFalse();
        c.Is<string>().Should().BeFalse();
        c.Is<Guid>().Should().BeFalse();
        c.Is<bool>().Should().BeFalse();
        c.Is<DateTime>().Should().BeTrue();
    }


    [Fact]
    public void Choice5_SetToT1_MatchActionCallsAction1()
    {
        Choice<int, string, Guid, bool, DateTime> c = 3;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;
        bool called4 = false;
        bool called5 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true),
            new Action<bool>(_ => called4 = true),
            new Action<DateTime>(_ => called5 = true)
            );

        called1.Should().BeTrue();
        called2.Should().BeFalse();
        called3.Should().BeFalse();
        called4.Should().BeFalse();
        called5.Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT2_MatchActionCallsAction2()
    {
        Choice<int, string, Guid, bool, DateTime> c = "";
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;
        bool called4 = false;
        bool called5 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true),
            new Action<bool>(_ => called4 = true),
            new Action<DateTime>(_ => called5 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeTrue();
        called3.Should().BeFalse();
        called4.Should().BeFalse();
        called5.Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT3_MatchActionCallsAction3()
    {
        Choice<int, string, Guid, bool, DateTime> c = Guid.Empty;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;
        bool called4 = false;
        bool called5 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true),
            new Action<bool>(_ => called4 = true),
            new Action<DateTime>(_ => called5 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeFalse();
        called3.Should().BeTrue();
        called4.Should().BeFalse();
        called5.Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT4_MatchActionCallsAction4()
    {
        Choice<int, string, Guid, bool, DateTime> c = false;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;
        bool called4 = false;
        bool called5 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true),
            new Action<bool>(_ => called4 = true),
            new Action<DateTime>(_ => called5 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeFalse();
        called3.Should().BeFalse();
        called4.Should().BeTrue();
        called5.Should().BeFalse();
    }

    [Fact]
    public void Choice5_SetToT5_MatchActionCallsAction4()
    {
        Choice<int, string, Guid, bool, DateTime> c = DateTime.Now;
        bool called1 = false;
        bool called2 = false;
        bool called3 = false;
        bool called4 = false;
        bool called5 = false;

        c.Match(
            new Action<int>(_ => called1 = true),
            new Action<string>(_ => called2 = true),
            new Action<Guid>(_ => called3 = true),
            new Action<bool>(_ => called4 = true),
            new Action<DateTime>(_ => called5 = true)
            );

        called1.Should().BeFalse();
        called2.Should().BeFalse();
        called3.Should().BeFalse();
        called4.Should().BeFalse();
        called5.Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetToT1_MatchFuncCallsFunc1()
    {
        Choice<int, string, Guid, bool, DateTime> c = 3;
        
        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3),
            new Func<bool, int>(_ => 4),
            new Func<DateTime, int>(_ => 5)
            );

        called.Should().Be(1);
    }

    [Fact]
    public void Choice5_SetToT2_MatchFuncCallsFunc2()
    {
        Choice<int, string, Guid, bool, DateTime> c = "";

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3),
            new Func<bool, int>(_ => 4),
            new Func<DateTime, int>(_ => 5)
            );

        called.Should().Be(2);
    }

    [Fact]
    public void Choice5_SetToT3_MatchFuncCallsFunc3()
    {
        Choice<int, string, Guid, bool, DateTime> c = Guid.Empty;

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3),
            new Func<bool, int>(_ => 4),
            new Func<DateTime, int>(_ => 5)
            );

        called.Should().Be(3);
    }

    [Fact]
    public void Choice5_SetToT4_MatchFuncCallsFunc4()
    {
        Choice<int, string, Guid, bool, DateTime> c = false;

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3),
            new Func<bool, int>(_ => 4),
            new Func<DateTime, int>(_ => 5)
            );

        called.Should().Be(4);
    }

    [Fact]
    public void Choice5_SetToT5_MatchFuncCallsFunc5()
    {
        Choice<int, string, Guid, bool, DateTime> c = DateTime.Now;

        int called = c.Match(
            new Func<int, int>(_ => 1),
            new Func<string, int>(_ => 2),
            new Func<Guid, int>(_ => 3),
            new Func<bool, int>(_ => 4),
            new Func<DateTime, int>(_ => 5)
            );

        called.Should().Be(5);
    }

    [Fact]
    public void Choice5_SetToT1Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<string, int> c = Choice<string, int>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice5_SetToT2Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, string, Guid, bool, DateTime> c = Choice<int, string, Guid, bool, DateTime>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice5_SetToT3Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, Guid, string> c = Choice<int, Guid, string>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice5_SetToT4Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, float, Guid, string, DateTime> c = Choice<int, float, Guid, string, DateTime>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice5_SetToT5Null_ThrowsException()
    {
        string s = null;
        Action a = () => { Choice<int, float, Guid, DateTime, string> c = Choice<int, float, Guid, DateTime, string>.From(s); };
        a.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Choice5_SetToT1_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float, Guid, DateTime> c = 2;
        c.GetChosenType().Should().Be(typeof(int));
    }

    [Fact]
    public void Choice5_SetToT2_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float, Guid, DateTime> c = "a";
        c.GetChosenType().Should().Be(typeof(string));
    }

    [Fact]
    public void Choice5_SetToT3_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float, Guid, DateTime> c = 0.2f;
        c.GetChosenType().Should().Be(typeof(float));
    }

    [Fact]
    public void Choice5_SetToT4_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float, Guid, DateTime> c = Guid.Empty;
        c.GetChosenType().Should().Be(typeof(Guid));
    }

    [Fact]
    public void Choice5_SetToT5_GetChosenType_ReturnsCorrectType()
    {
        Choice<int, string, float, Guid, DateTime> c = DateTime.Now;
        c.GetChosenType().Should().Be(typeof(DateTime));
    }

    [Fact]
    public void Choice5_SetTo1_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float, Guid, DateTime> c = 2;
        c.Value.Should().Be(2);
    }

    [Fact]
    public void Choice5_SetTo2_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float, Guid, DateTime> c = "a";
        c.Value.Should().Be("a");
    }

    [Fact]
    public void Choice5_SetTo3_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float, Guid, DateTime> c = 1.1f;
        c.Value.Should().Be(1.1f);
    }

    [Fact]
    public void Choice5_SetTo4_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float, Guid, DateTime> c = Guid.Empty;
        c.Value.Should().Be(Guid.Empty);
    }

    [Fact]
    public void Choice5_SetTo5_Value_ReturnsExpectedValue()
    {
        Choice<int, string, float, Guid, DateTime> c = DateTime.MaxValue;
        c.Value.Should().Be(DateTime.MaxValue);
    }
}
