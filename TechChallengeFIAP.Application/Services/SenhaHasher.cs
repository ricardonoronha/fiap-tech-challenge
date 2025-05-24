using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Application.Services;

public class SenhaHasher : ISenhaHasher
{
    public string HashSenha(string senha)
    {
        // Gera um salt de 128 bits (16 bytes)
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // Gera o hash usando PBKDF2 com HMACSHA256
        byte[] hash = KeyDerivation.Pbkdf2(
            password: senha,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100_000,
            numBytesRequested: 32);

        // Concatena salt + hash para armazenar juntos (em Base64)
        var resultado = Convert.ToBase64String(Combine(salt, hash));
        return resultado;
    }

    public bool VerificarSenha(string senhaDigitada, string hashArmazenado)
    {
        var fullBytes = Convert.FromBase64String(hashArmazenado);

        byte[] salt = fullBytes[..16];
        byte[] hashOriginal = fullBytes[16..];

        // Gera o hash novamente com o mesmo salt
        byte[] novoHash = KeyDerivation.Pbkdf2(
            password: senhaDigitada,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100_000,
            numBytesRequested: 32);

        return CryptographicOperations.FixedTimeEquals(novoHash, hashOriginal);
    }

    private static byte[] Combine(byte[] a, byte[] b)
    {
        byte[] resultado = new byte[a.Length + b.Length];
        Buffer.BlockCopy(a, 0, resultado, 0, a.Length);
        Buffer.BlockCopy(b, 0, resultado, a.Length, b.Length);
        return resultado;
    }
}