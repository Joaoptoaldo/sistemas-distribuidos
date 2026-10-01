using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor.Comunicacao;

/// <summary>
/// Encapsula a comunicação UDP do servidor com os clientes
/// </summary>
public class Comunicador : IDisposable
{
    private readonly UdpClient _udpClient;

    public Comunicador(int porta)
    {
        _udpClient = new UdpClient(porta);
    }

    /// <summary>
    /// Aguarda um datagrama e informa também o endpoint que o enviou
    /// </summary>
    /// <returns>A mensagem recebida e o respectivo remetente</returns>
    public (string Mensagem, IPEndPoint Remetente) Receber()
    {
        IPEndPoint endereco = new(IPAddress.Any, 0);

        byte[] dados = _udpClient.Receive(ref endereco);

        string mensagem = Encoding.UTF8.GetString(dados);

        return (mensagem, endereco);
        
    }


    /// <summary>
    /// Envia uma mensagem UTF-8 ao endpoint UDP indicado
    /// </summary>
    /// <param name="mensagem">Conteúdo da mensagem</param>
    /// <param name="destinatario">Endpoint do cliente destinatário</param>
    public void Enviar(string mensagem, IPEndPoint destinatario)
    {
        byte[] dados = Encoding.UTF8.GetBytes(mensagem);

        _udpClient.Send(
            dados,
            dados.Length,
            destinatario
        );
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}