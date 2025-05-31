using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Data.Repositorios;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Data
{
    public class UnitOfWork(ApplicationDbContext DbContext) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
