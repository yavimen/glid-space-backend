using FluentValidation;
using Main.API.DtoModels;
namespace Main.API.Validators
{
    public class ArticleForUpdateDtoValidator: AbstractValidator<ArticleForUpdateDto>
    {
        public ArticleForUpdateDtoValidator()
        {
            RuleFor(article => article.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");
        }
    }
}
