using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Domain.DTOs.Jogo
{
    public class CriarJogoRequestDto
    {
        [Required(ErrorMessage = "Nome do jogo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome do jogo deve ter entre 3 e 100 caracteres.")]
        public string NomeJogo { get; set; }

        [Required(ErrorMessage = "Descrição do jogo é obrigatória.")]
        [StringLength(1000, ErrorMessage = "Descrição do jogo não pode exceder 1000 caracteres.")]
        public string DescricaoJogo { get; set; }

        [Required(ErrorMessage = "Classificação do jogo é obrigatória.")]
        [RegularExpression(@"^[L|10|12|14|16|18]$", ErrorMessage = "Classificação deve ser um dos valores permitidos: L, 10, 12, 14, 16, 18.")]
        public string ClassificacaoJogo { get; set; }

        [Required(ErrorMessage = "Data de lançamento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Valor base deve ser maior que zero.")]
        public decimal ValorBase { get; set; }
    }
}
