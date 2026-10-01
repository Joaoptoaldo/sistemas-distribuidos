using Servidor.Comunicacao;
using Servidor.Controllers;
using System.Net.Sockets;

const int porta = 5000;

using Comunicador comunicador = new(porta);

ServidorController controller = new();

Console.WriteLine($"Servidor UDP aguardando na porta {porta}...");

while (true)
{
    try
    {
        var resultado = comunicador.Receber();

        Console.WriteLine(
            $"Mensagem recebida de {resultado.Remetente}: {resultado.Mensagem}"
        );

        string resposta = controller.ProcessarMensagem(
            resultado.Mensagem
        );

        Console.WriteLine($"Resposta enviada: {resposta}");

        comunicador.Enviar(
            resposta,
            resultado.Remetente
        );
    }
    catch (SocketException ex)
    {
        Console.WriteLine($"Erro de comunicação UDP: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado no servidor: {ex}");
    }
}