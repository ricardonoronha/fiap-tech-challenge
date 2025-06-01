using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Data.Repositorios;

public class PessoaRepositorio(ApplicationDbContext DbContext) : IPessoaRepositorio
{
    public void AddPessoa(Pessoa pessoa)
    {
        DbContext.Add(pessoa);
    }

    public Task<Pessoa?> GetByEmail(string email)
    {
        return DbContext
            .Pessoa
            .SingleOrDefaultAsync(x => x.EmailUsuario == email);
    }

    public Task<bool> VerificarEhEmailIndisponivel(string email)
    {
        return DbContext
            .Pessoa
            .AnyAsync(x => x.EmailUsuario == email); 
    }
}
