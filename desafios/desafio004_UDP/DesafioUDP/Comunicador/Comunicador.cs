using System.Net;

namespace Comunicador;

/// <summary>
/// Fachada da comunicação UDP — nome exigido pelo desafio.
/// Não implementa socket: delega para <see cref="ComunicadorCliente"/> ou <see cref="ComunicadorServidor"/>.
/// </summary>
public class Comunicador : IComunicador
{
    private readonly IComunicador _implementacao;

    /// <summary>
    /// Modo servidor: vincula à porta indicada
    /// </summary>
    public Comunicador(int porta)
    {
        _implementacao = new ComunicadorServidor(porta);
    }

    /// <summary>
    /// Modo cliente: socket efêmero com destino padrão do servidor
    /// </summary>
    public Comunicador(
        string enderecoServidor = Protocolo.EnderecoServidor,
        int portaServidor = Protocolo.PortaServidor)
    {
        _implementacao = new ComunicadorCliente(enderecoServidor, portaServidor);
    }

    public (string Mensagem, IPEndPoint Remetente) Receber() =>
        _implementacao.Receber();

    public void Enviar(string mensagem, IPEndPoint destinatario) =>
        _implementacao.Enviar(mensagem, destinatario);

    public void Dispose() =>
        _implementacao.Dispose();
}
