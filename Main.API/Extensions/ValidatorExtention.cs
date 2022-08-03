using System.Text;

namespace Main.API.Extensions
{
    public static class ValidatorExtention
    {
        public static string ToStringErrorMessages(this IEnumerable<FluentValidation.Results.ValidationFailure> validationFailures)
        {
            StringBuilder ouput = new StringBuilder();

            foreach (var item in validationFailures)
            {
                ouput.Append(item.ErrorMessage + ". ");
            }

            return ouput.ToString();
        }
    }
}
