using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Domain.Events;

public class TokenGerado (string Login) : IEvent
{
    public string Login { get; init; } = Login;
}
