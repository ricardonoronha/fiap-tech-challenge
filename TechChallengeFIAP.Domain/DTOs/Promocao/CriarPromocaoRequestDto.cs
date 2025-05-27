using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Domain.DTOs.Promocao
{
    public class CriarPromocaoRequestDto
    {
        [Required(ErrorMessage = "JogoId é obrigatório.")]
        public Guid JogoId { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de fim é obrigatória.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("DataInicio", ErrorMessage = "DataFim deve ser maior que DataInicio.")]
        public DateTime DataFim { get; set; }

        [Range(1, 100, ErrorMessage = "Percentual de desconto deve estar entre 1 e 100.")]
        public decimal PercentualDesconto { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException($"Propriedade '{_comparisonProperty}' não encontrada.");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue != null && comparisonValue != null)
            {
                if (currentValue <= comparisonValue)
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.MemberName} deve ser maior que {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }

}
