using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Domain.Events;

public record TokenGerado (string Login) : IEvent
{ }
