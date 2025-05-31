using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Entidades
{
    public class Promocao
    {
        public Guid Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }
        public bool EhCancelada { get; set; }
        public decimal PercentualDesconto { get; set; }
    }
}
