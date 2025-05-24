using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Data.Repositorios;

public class EventStoreRepository(ApplicationDbContext DbContext) : IEventStoreRepository
{
    public void SaveEvents(Pessoa? responsavel, IEnumerable<IEvent> events)
    {
        var entities = events
            .Select(x => RegistroEvento.From(responsavel, x))
            .ToList();

        DbContext.AddRange(entities);
    }
}
