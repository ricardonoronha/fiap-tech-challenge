using FluentValidation;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

public class PessoaValidacao : AbstractValidator<Pessoa>
{
    public PessoaValidacao()
    {
        RuleFor(p => p.NomeCompleto)
            .NotEmpty().WithMessage("É obrigatório o nome completo.")
            .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras.");

        RuleFor(p => p.EmailUsuario)
            .NotEmpty().WithMessage("É obrigatório o e-mail.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");

        RuleFor(p => p.HashSenha)
            .NotEmpty().WithMessage("É obrigatório a senha do usuário.")
            .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras.");
    }
}
