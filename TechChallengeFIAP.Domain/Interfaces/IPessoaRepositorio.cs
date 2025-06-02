using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Domain.Interfaces;

public interface IPessoaRepositorio
{
    Task<Pessoa?> GetByEmail(string email);
    void AddPessoa(Pessoa pessoa);

    Task<bool> VerificarEhEmailIndisponivel(string email);
}
