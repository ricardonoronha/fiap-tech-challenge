using System;

namespace TechChallengeFIAP.Domain.Entities
{
    public class EventStore
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string EntidadeId { get; set; }
        public string NomeEvento { get; set; }
        public string DadosEvento { get; set; }
    }
}
