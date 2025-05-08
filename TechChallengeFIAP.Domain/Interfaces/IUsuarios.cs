namespace TechChallengeFIAP.Domain.Interfaces;

public interface IUsuarios
{

    string NomeCompleto { get; set; }
    string NomeUsuario { get; set; }
    string EmailUsuario { get; set; }
    DateTime DataNascimento { get; set; }
    string SenhaUsuario { get; set; }

}
