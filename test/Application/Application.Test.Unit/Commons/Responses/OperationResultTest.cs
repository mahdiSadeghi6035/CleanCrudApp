using Application.Commons.Responses;
using FluentAssertions;

namespace Application.Test.Unit.Commons.Responses;

public class OperationResultTest
{

    [Fact]
    public void Should_SuccessOperation()
    {
        //act
        OperationResult operationResult = OperationResult.Success();

        //assert

        operationResult.IsSuccess.Should().BeTrue();
        operationResult.Messages.Should().BeEmpty();
    }


    [Fact]
    public void Should_FailureOperation()
    {
        //arrange
        string[] messages = new string[] { "Record not found" };

        //act
        OperationResult result = OperationResult.Failure(messages);

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Equal(messages);
    }

}
