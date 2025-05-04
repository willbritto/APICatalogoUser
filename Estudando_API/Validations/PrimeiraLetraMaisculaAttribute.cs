using System.ComponentModel.DataAnnotations;

namespace Estudando_API.Validations
{
    public class PrimeiraLetraMaisculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) 
            {
                return ValidationResult.Success;
            }
            
            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra!=primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiúscula");
            }
            return ValidationResult.Success;
        }
    }
}
