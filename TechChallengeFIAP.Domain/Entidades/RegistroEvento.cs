using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Domain.Entidades
{
    public class RegistroEvento
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Guid? PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
        public string Evento { get; set; } = string.Empty;
        public string DadosEvento { get; set; } = string.Empty;

        protected RegistroEvento()
        { }


        public static RegistroEvento From(Pessoa? pessoa, object evento)
        {
            string dadosEvento = JsonSerializer.Serialize(evento);

            return new()
            {
                Id = Guid.NewGuid(),
                Data = DateTime.Now,
                PessoaId = pessoa?.Id,
                Evento = evento.GetType().Name,
                DadosEvento = dadosEvento
            };
        }

    }
}
