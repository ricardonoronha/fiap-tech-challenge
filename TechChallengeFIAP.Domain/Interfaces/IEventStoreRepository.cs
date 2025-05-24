using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Domain.Interfaces
{
    public interface IEventStoreRepository
    {
        public void SaveEvents(Pessoa? responsavel, IEnumerable<IEvent> events);
    }
}
