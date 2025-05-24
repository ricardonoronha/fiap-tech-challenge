namespace TechChallengeFIAP.Domain.DTOs.Promocao
{
    public class CriarPromocaoRequestDto
    {
        public Guid JogoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal PercentualDesconto { get; set; }
    }

}
