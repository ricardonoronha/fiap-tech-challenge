namespace TechChallengeFIAP.Domain.DTOs.Promocao
{
    public class CriarPromocaoResponseDto
    {
        public Guid Id { get; set; }
        public Guid JogoId { get; set; }
        public decimal PercentualDesconto { get; set; }
    }

}
