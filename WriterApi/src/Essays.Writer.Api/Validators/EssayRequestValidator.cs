using Essays.Writer.Contracts.Requests;
using FluentValidation;

namespace Essays.Writer.Api.Validators;

public class EssayRequestValidator : AbstractValidator<EssayRequest>
{
    public EssayRequestValidator()
    {
        RuleFor(er => er.Title)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(er => er.Body)
            .NotEmpty();

        RuleFor(er => er.Author)
            .MaximumLength(255)
            .NotEmpty();
    }
}