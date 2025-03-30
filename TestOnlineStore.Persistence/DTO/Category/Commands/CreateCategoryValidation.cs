using FluentValidation;

namespace TestOnlineStore.Persistence.DTO.Category.Commands;

public class CreateCategoryValidation
    : AbstractValidator<CreateCategory>
{
    private const int MaxLengthName = 100;
    private const int MaxLengthDescription = 400;

    public CreateCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MaxLengthName);

        RuleFor(x => x.Description)
            .MaximumLength(MaxLengthDescription);
    }
}
