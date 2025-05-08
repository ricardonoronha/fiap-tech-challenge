using TechChallengeFIAP.Interface;

namespace TechChallengeFIAP.Models
{
    public abstract class Pessoas : IUsuarios
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime DataNascimento { get; set; }
        public string SenhaUsuario { get; set; }
        public Permissoes Permissoes { get; set; }

        public bool Ativo = true;

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return NomeCompleto.ToString();
        }

    }

}
