using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Domain.Entidades;

public class Pessoa : IEntidadeBase
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string NomeUsuario { get; set; } = string.Empty;
    public string EmailUsuario { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string HashSenha { get; private set; } = string.Empty;
    public bool EhAdministrador { get; private set; }
    public bool EhAtivo { get; private set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioCriador { get; set; } = string.Empty;
    public DateTime? DataAtualizacao { get; set; }
    public string UsuarioAtualizador { get; set; } = string.Empty;


    public override string ToString()
    {
        return NomeCompleto.ToString();
    }
}
