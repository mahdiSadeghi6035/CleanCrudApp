using Application.Commons.Responses;
using FluentAssertions;
using FluentValidation.Results;

namespace Application.Test.Unit.Commons.Responses;

public class ExtensionsFluentValidationTest
{
    [Fact]
    public void Should_SuccessOperation()
    {
        //arrange
        ValidationResult validationResult = new ValidationResult()
        {

        };
        //act
        var result = validationResult.ToResult();

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Name", "Name is required")]
    [InlineData("UnitPrice", "UnitPrice is required")]
    public void Should_FailValidation(string propertyName, string message)
    {
        //arrange
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure(propertyName,message)
        });
        //act
        var result = validationResult.ToResult();

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain(message);
    }
}
