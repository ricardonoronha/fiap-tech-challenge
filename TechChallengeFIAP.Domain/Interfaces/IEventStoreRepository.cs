using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Interfaces
{
    public interface IEventStoreRepository
    {
        public void SaveEvents(IEnumerable<IEvent> events);
    }
}
