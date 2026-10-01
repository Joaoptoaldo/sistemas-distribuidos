using Cliente.Comunicacao;
using System.Net.Sockets;

namespace Cliente.Controllers;

/// <summary>
/// Coordena as operações do cliente e traduz respostas UDP para a View
/// </summary>
public class ClienteController : IDisposable
{
    private readonly Comunicador _comunicador;

    public ClienteController()
    {
        _comunicador = new Comunicador();
    }

    /// <summary>
    /// Solicita o cadastro de uma pessoa ao servidor
    /// </summary>
    /// <param name="nome">Nome completo da pessoa</param>
    /// <param name="email">E-mail usado para identificar o cadastro</param>
    /// <returns>Indica se o cadastro foi aceito e fornece a mensagem resultante</returns>
    public (bool Sucesso, string Mensagem) CadastrarPessoa(string nome, string email)
    {
        string mensagem = $"CADASTRO|{nome}|{email}";

        try
        {
            _comunicador.Enviar(mensagem, "127.0.0.1", 5000);
            string resposta = _comunicador.Receber();

            return resposta.StartsWith("SUCESSO", StringComparison.Ordinal)
                ? (true, resposta)
                : (false, resposta);
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
            _comunicador.Enviar("TOKEN", "127.0.0.1", 5000);
            string resposta = _comunicador.Receber();

            if (!resposta.StartsWith("TOKEN|", StringComparison.Ordinal))
            {
                return (false, resposta);
            }

            return (true, resposta["TOKEN|".Length..]);
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