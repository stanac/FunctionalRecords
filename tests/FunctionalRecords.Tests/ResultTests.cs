using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FunctionalRecords.Tests
{
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
        public void Result_Success_SetsSuccessToTrue_And_ErrorsToEmtpy_And_ExceptionToNone()
        {
            Result r = Result.Success();
            r.IsSuccess.Should().BeTrue();
            r.IsFailure.Should().BeFalse();
            r.Errors.Should().NotBeNull();
            r.Errors.Should().BeEmpty();
            r.Exception.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Result_FailureWithErrors_SetsIsFailureToTrue_And_ErrorsToEmtpy_And_ExceptionToNone()
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

        private static IEnumerable<string> GetErrors()
        {
            yield return "a";
            yield return "b";
        }
    }
}
