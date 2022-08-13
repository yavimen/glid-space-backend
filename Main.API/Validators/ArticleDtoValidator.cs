using FluentValidation;
using Main.API.DtoModels;

namespace Main.API.Validators
{
    public class ArticleDtoValidator: AbstractValidator<ArticleDto>
    {
        public ArticleDtoValidator()
        {
            RuleFor(article => article.Id).NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(article => article.Title).NotNull().NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(article => article.Content).NotNull().NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(article => article.PublicationDate).NotEmpty().NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");
        }
    }
}
