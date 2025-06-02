using System;
using System.Linq;
using TechChallengeFIAP.Domain.DTOs.Seguranca; 
using TechChallengeFIAP.Domain.Interfaces;

public interface IUserInfoService
{
    UserInfo? ObterUsuario();
}