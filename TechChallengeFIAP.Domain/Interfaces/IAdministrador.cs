﻿namespace TechChallengeFIAP.Domain.Interfaces;

public interface IAdministrador
{
    string NomeCompleto { get; set; }
    string NomeAdministrador { get; set; }
    string EmailAdministrador { get; set; }
    DateTime DataNascimento { get; set; }
    string SenhaAdministrador { get; set; }
}
