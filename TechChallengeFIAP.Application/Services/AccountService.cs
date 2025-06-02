using TechChallengeFIAP.Data;
using TechChallengeFIAP.Data.Migrations;
using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.DTOs.Seguranca;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Application.Services;

public class AccountService(
    IPessoaRepositorio PessoaRepositorio,
    IRegistrarUsuarioRequestValidador RegistrarUsuarioRequestValidador,
    ISenhaHasher SenhaHasher,
    IUnitOfWork UnitOfWork) : IAccountService
{
    public async Task<IRegistrarUsuarioResponseDto> RegistrarUsuario(RegistrarUsuarioRequestDto request, UserInfo? userInfo, CancellationToken cancellationToken)
    {
        var validacaoRequest = await RegistrarUsuarioRequestValidador.ValidarAsync(request, userInfo);

        if (validacaoRequest.Any())
            return new FalhaAoRegistraUsuarioResponseDto(validacaoRequest);

        var pessoaId = Guid.NewGuid();

        var pessoa = new Pessoa()
        {
            Id = pessoaId,
            NomeUsuario = request.NomeCompleto,
            NomeCompleto = request.NomeCompleto,
            EmailUsuario = request.Email,
            HashSenha = SenhaHasher.HashSenha(request.Senha),
            EhAdministrador = request.EhAdministrador,
            DataNascimento = request.DataNascimento!.Value.ToDateTime(TimeOnly.MinValue),
            DataCriacao = DateTime.Now,
            EhAtivo = true
        };

        PessoaRepositorio.AddPessoa(pessoa);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UsuarioRegistradoResponseDto(pessoaId, pessoa.EhAdministrador);
    }



}