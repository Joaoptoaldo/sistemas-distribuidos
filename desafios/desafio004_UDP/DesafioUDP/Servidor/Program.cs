using Comunicador;
using Servidor.Controllers;

// Classe Comunicador (exigida pelo desafio) — modo servidor: bind na porta
using var comunicador = new Comunicador.Comunicador(Protocolo.PortaServidor);

ServidorController controller = new();

Console.WriteLine($"Servidor UDP aguardando na porta {Protocolo.PortaServidor}...");

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
    catch (System.Net.Sockets.SocketException ex)
    {
        Console.WriteLine($"Erro de comunicação UDP: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado no servidor: {ex}");
    }
}
