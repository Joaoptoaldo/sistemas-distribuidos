using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cliente.Comunicacao;

/// <summary>
/// Encapsula o envio e o recebimento de mensagens UDP do cliente
/// </summary>
public class Comunicador : IDisposable
{
    private const int TempoLimiteRecebimentoMs = 5000;
    private readonly UdpClient _udpClient;

    public Comunicador()
    {
        _udpClient = new UdpClient();
        _udpClient.Client.ReceiveTimeout = TempoLimiteRecebimentoMs;
    }

    /// <summary>
    /// Envia uma mensagem UTF-8 para o endpoint UDP informado
    /// </summary>
    /// <param name="mensagem">Conteúdo da mensagem</param>
    /// <param name="endereco">Endereço do destinatário</param>
    /// <param name="porta">Porta UDP do destinatário</param>
    public void Enviar(string mensagem, string endereco, int porta)
    {
        byte[] dados = Encoding.UTF8.GetBytes(mensagem);

        _udpClient.Send(
            dados,
            dados.Length,
            endereco,
            porta
        );
    }

    /// <summary>
    /// Aguarda uma resposta UDP respeitando o timeout configurado no comunicador
    /// </summary>
    /// <returns>A mensagem recebida decodificada como UTF-8</returns>
    public string Receber()
    {
        IPEndPoint endereco = new(IPAddress.Any, 0);

        byte[] dados = _udpClient.Receive(ref endereco);

        return Encoding.UTF8.GetString(dados);
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}