using System.Net;

namespace Comunicador;

/// <summary>
/// Contrato de transporte compartilhado entre cliente e servidor.
/// Quem consome a rede depende deste contrato, não de <see cref="System.Net.Sockets.UdpClient"/>.
/// </summary>
public interface IComunicador : IDisposable
{
    /// <summary>
    /// Envia uma mensagem UTF-8 ao endpoint UDP indicado
    /// </summary>
    void Enviar(string mensagem, IPEndPoint destinatario);

    /// <summary>
    /// Aguarda um datagrama e devolve a mensagem com o remetente
    /// </summary>
    (string Mensagem, IPEndPoint Remetente) Receber();
}
