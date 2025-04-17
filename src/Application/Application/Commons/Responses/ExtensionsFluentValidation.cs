using FluentValidation.Results;

namespace Application.Commons.Responses;

public static class ExtensionsFluentValidation
{
    public static OperationResult ToResult(this ValidationResult validationResult) => validationResult.IsValid
            ? OperationResult.Success()
            : OperationResult.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
}
