using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Domain.Validacao
{
    public class JogoValidacao : AbstractValidator<Jogo>
    {
        public JogoValidacao()
        {
            RuleFor(j => j.NomeJogo)
                .NotEmpty().WithMessage("O nome do jogo é obrigatório")
                .MinimumLength(2);

            RuleFor(j => j.DescricaoJogo)
                .NotEmpty().WithMessage("A descrição do jogo deve ser preenchida")
                .MinimumLength(5);

            RuleFor(j => j.ClassificacaoJogo)
                .NotEmpty().WithMessage("É necessária informar a clasificação indicativa do jogo")
                .MinimumLength(1);
        }
    }
}
