using FluentAssertions;

namespace FunctionalRecords.Tests;

public class ResultTests
{
    private const string ErrorMessage = "ErrorMessage";

    [Fact]
    public void Result_DefaultConstructor_SetsSuccessToFalse()
    {
        Result r = Result.Failure();
        r.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void ResultTValue_DefaultConstructor_SetsSuccessToFalse()
    {
        Result<int> r = Result.Failure<int>();
        r.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Result_Success_SetsSuccessToTrue_And_ErrorsToEmpty_And_ExceptionToNone()
    {
        Result r = Result.Success();
        r.IsSuccess.Should().BeTrue();
        r.IsFailure.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void Result_Success_EnsureValid_DoesNotThrow()
    {
        Result r = Result.Success();
        Action a = () => r.EnsureSuccess();
        a.Should().NotThrow();
    }

    [Fact]
    public void Result_Failure_EnsureValid_ThrowsException()
    {
        Result r = Result.Failure("abc");
        Action a = () => r.EnsureSuccess();
        a.Should().Throw<OperationFailedException>();
    }

    [Fact]
    public void Result_FailureWithErrors_SetsIsFailureToTrue_And_ErrorsToEmpty_And_ExceptionToNone()
    {
        Result r = Result.Failure();
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void Result_FailureWithErrors_SetsFailureToTrue_And_ErrorsToExpectedErrors_And_ExceptionToNone()
    {
        Result r = Result.Failure("a", "b");
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors.Should().HaveCount(2);
        r.Errors.Should().Contain("a");
        r.Errors.Should().Contain("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void Result_FailureWithIEnumerableErrors_SetsFailureToTrue_And_ErrorsToExpectedErrors_And_ExceptionToNone()
    {
        Result r = Result.Failure(GetErrors());
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors.Should().HaveCount(2);
        r.Errors.Should().Contain("a");
        r.Errors.Should().Contain("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void Result_FailureWithException_SetsFailureToTrue_And_ErrorsToEmpty_And_ExceptionToSome()
    {
        Exception e = new InvalidOperationException("somethig");
        Result r = Result.Failure(e);
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeFalse();
        r.Exception.Value.Should().BeOfType<InvalidOperationException>();
        r.Exception.Value.Message.Should().Be(e.Message);
    }

    [Fact]
    public void Result_FailureWithErrorsAndException_SetsFailureToTrue_And_ErrorsToErrors_And_ExceptionToSome()
    {
        Exception e = new InvalidOperationException("somethig");
        Result r = Result.Failure(e, "a", "b");
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors.Should().HaveCount(2);
        r.Errors.Should().Contain("a");
        r.Errors.Should().Contain("b");
        r.Exception.IsNone.Should().BeFalse();
        r.Exception.Value.Should().BeOfType<InvalidOperationException>();
        r.Exception.Value.Message.Should().Be(e.Message);
    }

    [Fact]
    public void Result_FailureWithIEnumerableErrorsAndException_SetsFailureToTrue_And_ErrorsToErrors_And_ExceptionToSome()
    {
        Exception e = new InvalidOperationException("somethig");
        Result r = Result.Failure(e, GetErrors());
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors.Should().HaveCount(2);
        r.Errors.Should().Contain("a");
        r.Errors.Should().Contain("b");
        r.Exception.IsNone.Should().BeFalse();
        r.Exception.Value.Should().BeOfType<InvalidOperationException>();
        r.Exception.Value.Message.Should().Be(e.Message);
    }

    [Fact]
    public void Result_FailureWithErrorListAndException_SetsFailureToTrue_And_ErrorsToErrors_And_ExceptionToSome()
    {
        Exception e = new InvalidOperationException("somethig");
        Result r = Result.Failure(e, new List<string> { "a", "b" });
        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors.Should().HaveCount(2);
        r.Errors.Should().Contain("a");
        r.Errors.Should().Contain("b");
        r.Exception.IsNone.Should().BeFalse();
        r.Exception.Value.Should().BeOfType<InvalidOperationException>();
        r.Exception.Value.Message.Should().Be(e.Message);
    }

    [Fact]
    public void ResultTValue_Success_SetsValue_And_AllOtherPropertiesToDefault()
    {
        Result<int> r = Result.Success(1);

        r.IsSuccess.Should().BeTrue();
        r.IsFailure.Should().BeFalse();
        r.Value.Value.Should().Be(1);
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }
    
    [Fact]
    public void ResultT_Success_EnsureValid_DoesNotThrow()
    {
        Result<int> r = Result.Success(1);
        Action a = () => r.EnsureSuccess();
        a.Should().NotThrow();
    }

    [Fact]
    public void ResultT_Failure_EnsureValid_ThrowsException()
    {
        Result<int> r = Result.Failure<int>("abc");
        Action a = () => r.EnsureSuccess();
        a.Should().Throw<OperationFailedException>();
    }

    [Fact]
    public void ResultTValue_Failure_SetsValueToNone_And_AllOtherPropertiesToDefault()
    {
        Result<int> r = Result.Failure<int>();

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValue_FailureWithErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int> r = Result.Failure<int>("a", "b");

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValue_FailureWithIEnumerableErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int> r = Result.Failure<int>(GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValue_FailureWithErrorList_SetsValueToNone_ErrorsToList_And_AllOtherPropertiesToDefault()
    {
        Result<int> r = Result.Failure<int>(new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValue_FailureException_SetsValueToNone_ErrorsToEmpty_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);
        Result<int> r = Result.Failure<int>(ex);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ResultTValue_FailureWithErrorListAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int> r = Result.Failure<int>(ex, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ResultTValue_FailureWithErrorIEnumerableAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int> r = Result.Failure<int>(ex, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ResultTValueTFailure_Success_SetsValue_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Success<int, Failures>(1);

        r.IsSuccess.Should().BeTrue();
        r.IsFailure.Should().BeFalse();
        r.Value.Value.Should().Be(1);
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTFailureFlag_Success_EnsureValid_DoesNotThrow()
    {
        Result<int, Failures> r = Result.Success<int, Failures>(1);
        Action a = () => r.EnsureSuccess();
        a.Should().NotThrow();
    }

    [Fact]
    public void ResultTFailureFlag_Failure_EnsureValid_ThrowsException()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2);
        Action a = () => r.EnsureSuccess();
        a.Should().Throw<OperationFailedException>();
    }
    
    [Fact]
    public void ResultTValueTFailure_Failure_SetsValueToNone_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>();

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>("a", "b");

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithIEnumerableErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithErrorList_SetsValueToNone_ErrorsToList_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
    }

    [Fact]
    public void ResultTValueTFailure_FailureException_SetsValueToNone_ErrorsToEmpty_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);
        Result<int, Failures> r = Result.Failure<int, Failures>(ex);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithErrorListAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, Failures> r = Result.Failure<int, Failures>(ex, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithErrorIEnumerableAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, Failures> r = Result.Failure<int, Failures>(ex, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
    }
    
    [Fact]
    public void ResultTValueTFailure_FailureWithFailureType_SetsValueToNone_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeWithErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, "a", "b");

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeWithIEnumerableErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeWithErrorList_SetsValueToNone_ErrorsToList_And_AllOtherPropertiesToDefault()
    {
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeException_SetsValueToNone_ErrorsToEmpty_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);
        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, ex);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeWithErrorListAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, ex, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailure_FailureWithFailureTypeWithErrorIEnumerableAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, Failures> r = Result.Failure<int, Failures>(Failures.F2, ex, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(Failures.F2);
        r.Is(Failures.F1).Should().BeFalse();
        r.Is(Failures.F2).Should().BeTrue();
        r.Is(Failures.F3).Should().BeFalse();
    }
    
    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureType_SetsValueToNone_And_AllOtherPropertiesToDefault()
    {
        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F1 | FailuresFlags.F2);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F2 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeWithErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F2 | FailuresFlags.F1, "a", "b");

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F2 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeWithIEnumerableErrors_SetsValueToNone_ErrorsToErrors_And_AllOtherPropertiesToDefault()
    {
        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F1 | FailuresFlags.F3, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F3 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeFalse();
        r.Is(FailuresFlags.F3).Should().BeTrue();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeWithErrorList_SetsValueToNone_ErrorsToList_And_AllOtherPropertiesToDefault()
    {
        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F2 | FailuresFlags.F1, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsNone.Should().BeTrue();
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F2 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeException_SetsValueToNone_ErrorsToEmpty_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);
        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F2 | FailuresFlags.F1, ex);

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().BeEmpty();
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F2 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeWithErrorListAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F2 | FailuresFlags.F1, ex, new List<string> { "a", "b" });

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.FailureType.Value.Should().Be(FailuresFlags.F2 | FailuresFlags.F1);
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }

    [Fact]
    public void ResultTValueTFailureFlags_FailureWithFailureTypeWithErrorIEnumerableAndException_SetsValueToNone_ErrorsToList_And_ExceptionToException()
    {
        Exception ex = new InvalidOperationException(ErrorMessage);

        Result<int, FailuresFlags> r = Result.Failure<int, FailuresFlags>(FailuresFlags.F2 | FailuresFlags.F1, ex, GetErrors());

        r.IsSuccess.Should().BeFalse();
        r.IsFailure.Should().BeTrue();
        r.Value.IsSome.Should().BeFalse();
        r.Errors.Should().NotBeNull();
        r.Errors.Should().NotBeEmpty();
        r.Errors[0].Should().Be("a");
        r.Errors[1].Should().Be("b");
        r.Exception.IsSome.Should().BeTrue();
        r.Exception.Value.Message.Should().Be(ErrorMessage);
        r.FailureType.IsSome.Should().BeTrue();
        r.Is(FailuresFlags.F1).Should().BeTrue();
        r.Is(FailuresFlags.F2).Should().BeTrue();
        r.Is(FailuresFlags.F3).Should().BeFalse();
    }
    
    private static IEnumerable<string> GetErrors()
    {
        yield return "a";
        yield return "b";
    }

    [Flags]
    public enum FailuresFlags
    {
        F1 = 1,
        F2 = 2,
        F3 = 4
    }

    public enum Failures
    {
        F1,
        F2,
        F3
    }
}
