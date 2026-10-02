using Comunicador;
using System.Net;
using System.Net.Sockets;

namespace Cliente.Controllers;

/// <summary>
/// Coordena as operações do cliente e traduz respostas UDP para a View.
/// Depende de <see cref="IComunicador"/>, não da implementação de rede.
/// </summary>
public class ClienteController : IDisposable
{
    private readonly IComunicador _comunicador;

    /// <summary>
    /// Cria a classe Comunicador UDP apontada para o servidor do protocolo
    /// </summary>
    public ClienteController()
        : this(new Comunicador.Comunicador(Protocolo.EnderecoServidor, Protocolo.PortaServidor))
    {
    }

    /// <summary>
    /// Injeção de dependência: permite testar o controller sem rede real
    /// </summary>
    public ClienteController(IComunicador comunicador)
    {
        _comunicador = comunicador;
    }

    private static IPEndPoint DestinoServidor() =>
        new(IPAddress.Parse(Protocolo.EnderecoServidor), Protocolo.PortaServidor);

    /// <summary>
    /// Solicita o cadastro de uma pessoa ao servidor
    /// </summary>
    /// <param name="nome">Nome completo da pessoa</param>
    /// <param name="email">E-mail usado para identificar o cadastro</param>
    /// <returns>Indica se o cadastro foi aceito e fornece a mensagem resultante</returns>
    public (bool Sucesso, string Mensagem) CadastrarPessoa(string nome, string email)
    {
        string mensagem = Protocolo.MontarCadastro(nome, email);

        try
        {
            _comunicador.Enviar(mensagem, DestinoServidor());
            var (resposta, _) = _comunicador.Receber();

            return (Protocolo.EhSucesso(resposta), resposta);
        }
        catch (SocketException ex) when (
            ex.SocketErrorCode == SocketError.TimedOut ||
            ex.SocketErrorCode == SocketError.ConnectionReset ||
            ex.SocketErrorCode == SocketError.ConnectionRefused)
        {
            return (false, "ERRO: O servidor não respondeu dentro do tempo limite.");
        }
        catch (Exception ex)
        {
            return (false, $"ERRO: Falha na comunicação com o servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Solicita ao servidor o token atualmente válido
    /// </summary>
    /// <returns>Indica se a resposta contém um token e fornece seu valor ou uma mensagem de erro</returns>
    public (bool Sucesso, string Mensagem) SolicitarToken()
    {
        try
        {
            _comunicador.Enviar(Protocolo.MontarTokenPedido(), DestinoServidor());
            var (resposta, _) = _comunicador.Receber();

            if (!Protocolo.TentarExtrairToken(resposta, out string token))
            {
                return (false, resposta);
            }

            return (true, token);
        }
        catch (SocketException ex) when (
            ex.SocketErrorCode == SocketError.TimedOut ||
            ex.SocketErrorCode == SocketError.ConnectionReset ||
            ex.SocketErrorCode == SocketError.ConnectionRefused)
        {
            return (false, "ERRO: O servidor não respondeu dentro do tempo limite.");
        }
        catch (Exception ex)
        {
            return (false, $"ERRO: Falha na comunicação com o servidor: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _comunicador.Dispose();
    }
}
