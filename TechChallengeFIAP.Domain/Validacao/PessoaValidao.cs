using FluentValidation;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Domain.Validacao
{
    public class PessoaValidao : AbstractValidator<Pessoa>
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public PessoaValidao(IPessoaRepositorio pessoaRepositorio)
        {

            _pessoaRepositorio = pessoaRepositorio;

            RuleFor(p => p.NomeCompleto)
                .NotEmpty().WithMessage("É obrigatório o nome completo.")
                .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras.");

            RuleFor(p => p.NomeUsuario)
                .NotEmpty().WithMessage("É obrigatório o nome completo.")
                .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras.");

            RuleFor(p => p.EmailUsuario)
                .NotEmpty().WithMessage("É obrigatório o e-mail.")
                .EmailAddress().WithMessage("O e-mail informado não é valido.")
                .MustAsync(async (email, cancellation) => !await _pessoaRepositorio.EmailExistenteAsync(email)).WithMessage("Este e-mail já está em uso");

            RuleFor(p => p.HashSenha)
                .NotEmpty().WithMessage("É obrigatório a senha do usuário.")
                .MinimumLength(3).WithMessage("Deve conter no mínimo 3 letras.");
        }
    }
}
