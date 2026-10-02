using System.Net;
using System.Net.Sockets;

namespace Comunicador;

/// <summary>
/// Comunicador UDP do cliente: socket efêmero, timeout de recepção e destino padrão.
/// </summary>
public class ComunicadorCliente : IComunicador
{
    private const int TempoLimiteRecebimentoMs = 5000;

    private readonly UdpClient _udpClient;
    private readonly IPEndPoint _destinoPadrao;

    public ComunicadorCliente(
        string enderecoServidor = Protocolo.EnderecoServidor,
        int portaServidor = Protocolo.PortaServidor)
    {
        _udpClient = new UdpClient();
        _udpClient.Client.ReceiveTimeout = TempoLimiteRecebimentoMs;
        _destinoPadrao = new IPEndPoint(IPAddress.Parse(enderecoServidor), portaServidor);
    }

    /// <summary>
    /// Envia ao servidor padrão configurado no construtor
    /// </summary>
    public void Enviar(string mensagem) =>
        Enviar(mensagem, _destinoPadrao);

    public void Enviar(string mensagem, IPEndPoint destinatario)
    {
        byte[] dados = CodecUdp.Codificar(mensagem);

        _udpClient.Send(
            dados,
            dados.Length,
            destinatario
        );
    }

    public (string Mensagem, IPEndPoint Remetente) Receber()
    {
        IPEndPoint remetente = new(IPAddress.Any, 0);

        byte[] dados = _udpClient.Receive(ref remetente);

        return (CodecUdp.Decodificar(dados), remetente);
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}
