using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.DTOs.Seguranca;
using TechChallengeFIAP.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace TechChallengeFIAP.Application.Services;

public class RegistrarUsuarioRequestValidador(
    IPessoaRepositorio PessoaRepositorio,
    ISenhaValidator SenhaValidador)
    : IRegistrarUsuarioRequestValidador
{
    public async Task<IEnumerable<RegistrarUsuarioErrorItem>> ValidarAsync(RegistrarUsuarioRequestDto request, UserInfo? userInfo)
    {
        var resultado = new List<RegistrarUsuarioErrorItem>(5);
        await AddValidacaoEmailAsync(request.Email, resultado);
        AddValidacaoNome(request.NomeCompleto, resultado);
        AddValidacaoSenha(request.Senha, request.SenhaConfirmada, resultado);
        AddValidacaoDataNascimento(request.DataNascimento, resultado);
        AddValidacaoEhAdministrador(request.EhAdministrador, userInfo, resultado);
        return resultado;
    }

    public async Task AddValidacaoEmailAsync(string email, List<RegistrarUsuarioErrorItem> resultadoValidacao)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Email),
                Errors = [new() { Codigo = "NaoInformado", Descricao = "Campo 'E-mail' é obrigatório" }]
            });

            return;
        }

        bool ehEmailValido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (!ehEmailValido)
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Email),
                Errors = [new() { Codigo = "FormatoInvalido", Descricao = "Formato de e-mail inválido" }]
            });
            return;
        }

        if (await PessoaRepositorio.VerificarEhEmailIndisponivel(email))
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Email),
                Errors = [new() { Codigo = "Indisponivel", Descricao = "E-mail informado já está em uso" }]
            });
        }

    }

    public void AddValidacaoNome(string nome, List<RegistrarUsuarioErrorItem> resultadoValidacao)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.NomeCompleto),
                Errors = [new() { Codigo = "NaoInformado", Descricao = "Campo 'Nome Completo' é obrigatório" }]
            });

            return;
        }
    }

    public void AddValidacaoSenha(string senha, string senhaConfirmada, List<RegistrarUsuarioErrorItem> resultadoValidacao)
    {
        var resultadoCampo = new List<RegistrarUsuarioErrorItem>();

        if (string.IsNullOrWhiteSpace(senha))
        {
            resultadoCampo.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Senha),
                Errors = [new() { Codigo = "NaoInformado", Descricao = "Campo 'Senha' é obrigatório" }]
            });
        }

        if (string.IsNullOrWhiteSpace(senhaConfirmada))
        {
            resultadoCampo.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.SenhaConfirmada),
                Errors = [new() { Codigo = "NaoInformado", Descricao = "Campo 'Senha Confirmada' é obrigatório" }]
            });
        }

        if (resultadoCampo.Any())
        {
            resultadoValidacao.AddRange(resultadoCampo);
            return;
        }

        if (senha != senhaConfirmada)
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Senha),
                Errors = [new() { Codigo = "ConfirmacaoDiferente", Descricao = "Campos 'Senha' e 'Senha Confirmada' são diferentes" }]
            });

            return;
        }

        var errosFormatoSenha = SenhaValidador.Validar(senha);
        var lista = errosFormatoSenha.ToList();

        if (errosFormatoSenha.Any())
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.Senha),
                Errors = errosFormatoSenha.ToList()
            });
        }
    }

    public void AddValidacaoDataNascimento(DateOnly? dataNascimento, List<RegistrarUsuarioErrorItem> resultadoValidacao)
    {
        if (!dataNascimento.HasValue)
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.DataNascimento),
                Errors = [new() { Codigo = "NaoInformado", Descricao = "Campo 'Data de Nascimento' é obrigatório" }]
            });
        }

    }

    public void AddValidacaoEhAdministrador(bool ehAdministrador, UserInfo? userInfo, List<RegistrarUsuarioErrorItem> resultadoValidacao)
    {
        bool ehCadastroPorAdministrador = userInfo?.EhAdministrador ?? false;

        if (ehAdministrador && !ehCadastroPorAdministrador)
        {
            resultadoValidacao.Add(new()
            {
                Campo = nameof(RegistrarUsuarioRequestDto.EhAdministrador),
                Errors = [new() { Codigo = "Proibido", Descricao = "Somente administradores podem cadastrar usuários administradores" }]
            });
        }
    }
}