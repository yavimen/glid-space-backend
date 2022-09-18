using FluentValidation;
using Main.API.DtoModels;

namespace Main.API.Validators
{
    public class ArticleForCreationDtoValidator: AbstractValidator<ArticleForCreationDto>
    {
        public ArticleForCreationDtoValidator()
        {
            RuleFor(article => article.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(article => article.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");
        }
    }
}
