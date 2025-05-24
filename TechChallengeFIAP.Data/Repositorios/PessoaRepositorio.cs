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
    public Task<Pessoa?> GetByLogin(string login)
    {
        return DbContext
            .Pessoa
            .SingleOrDefaultAsync(x => x.NomeUsuario == login);
    }
}
