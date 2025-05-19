using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Interfaces
{
    public interface IEntidadeBase
    {
        Guid Id { get; set; }

        DateTime DataCriacao { get; set; }
        string UsuarioCriador { get; set; }

        DateTime? DataAtualizacao { get; set; }
        string UsuarioAtualizador { get; set; }
    }
}
