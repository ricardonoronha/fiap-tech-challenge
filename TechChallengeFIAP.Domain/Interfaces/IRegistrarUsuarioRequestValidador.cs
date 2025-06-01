
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  TechChallengeFIAP.Domain.DTOs.Account;
using  TechChallengeFIAP.Domain.DTOs.Seguranca;

namespace TechChallengeFIAP.Domain.Interfaces;

public interface IRegistrarUsuarioRequestValidador
{
    Task<IEnumerable<RegistrarUsuarioErrorItem>> ValidarAsync(RegistrarUsuarioRequestDto request, UserInfo? userInfo);
}

