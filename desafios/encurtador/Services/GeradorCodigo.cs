
using System.Security.Cryptography;

namespace Encurtador.Services;

public static class GeradorCodigo
{
    // caracteres válidos para o código
    private const string Alfabeto = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private const int TamanhoCodigo = 6;

    // método que gera um código aleatório de 6 caracteres
    public static string Gerar()
    {
        Span<char> caracteres = stackalloc char[TamanhoCodigo];

        for (int i = 0; i < caracteres.Length; i++)
        {
            int indice = RandomNumberGenerator.GetInt32(
                Alfabeto.Length
            );
            
            // seleciona um caractere aleatório do alfabeto
            caracteres[i] = Alfabeto[indice];
        }

        return new string(caracteres);
    }
}
