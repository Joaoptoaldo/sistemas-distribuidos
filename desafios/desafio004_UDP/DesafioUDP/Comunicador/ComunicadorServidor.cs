using System.Net;
using System.Net.Sockets;

namespace Comunicador;

/// <summary>
/// Comunicador UDP do servidor: bind na porta e expõe o remetente de cada datagrama.
/// </summary>
public class ComunicadorServidor : IComunicador
{
    private readonly UdpClient _udpClient;

    public ComunicadorServidor(int porta)
    {
        _udpClient = new UdpClient(porta);
    }

    public (string Mensagem, IPEndPoint Remetente) Receber()
    {
        IPEndPoint remetente = new(IPAddress.Any, 0);

        byte[] dados = _udpClient.Receive(ref remetente);

        return (CodecUdp.Decodificar(dados), remetente);
    }

    public void Enviar(string mensagem, IPEndPoint destinatario)
    {
        byte[] dados = CodecUdp.Codificar(mensagem);

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
