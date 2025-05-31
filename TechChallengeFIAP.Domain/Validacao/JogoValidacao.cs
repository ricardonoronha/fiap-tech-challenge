using FluentValidation;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Domain.Validacao
{
    public class JogoValidacao : AbstractValidator<Jogo>
    {
        public JogoValidacao()
        {
            RuleFor(j => j.NomeJogo)
                .NotEmpty().WithMessage("O nome do jogo é obrigatório")
                .MinimumLength(2).WithMessage("Deve conter no mínimo 2 letras."); ;

            RuleFor(j => j.DescricaoJogo)
                .NotEmpty().WithMessage("É necessário adicionar uma descrição")
                .MinimumLength(5).WithMessage("Deve conter no mínimo 5 letras."); ;

            RuleFor(j => j.ClassificacaoJogo)
                .NotEmpty().WithMessage("É necessário adicionar uma classificação")
                .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras."); ;

            RuleFor(j => j.ValorBase)
                .NotNull().WithMessage("O jogo precisa, obrigatoriamente, ter um preço")
                .GreaterThanOrEqualTo(0).WithMessage("O preço deve ser maior ou igual a R$ 0");
        }
    }
}
