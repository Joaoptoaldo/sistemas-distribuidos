using System.Text;

namespace Comunicador;

/// <summary>
/// Codificação UTF-8 compartilhada pelos comunicadores UDP
/// </summary>
internal static class CodecUdp
{
    public static byte[] Codificar(string mensagem) =>
        Encoding.UTF8.GetBytes(mensagem);

    public static string Decodificar(byte[] dados) =>
        Encoding.UTF8.GetString(dados);
}
