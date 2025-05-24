using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Domain.DTOs.Promocao
{
    public class PromocaoResponseDto
    {

        public Guid Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal PercentualDesconto { get; set; }
        public bool EhCancelada { get; set; }

    }

}
