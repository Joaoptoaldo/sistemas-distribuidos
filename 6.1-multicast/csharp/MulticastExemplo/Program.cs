using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Exemplo de Multicast");
Console.WriteLine("Grupo: 10.104.12.13");
Console.WriteLine("Porta: 1099");
Console.WriteLine("Digite mensagens e pressione Enter");
Console.WriteLine("Digite 'sair' para encerrar a aplicação.");
Console.WriteLine();

const string GrupoMulticast = "10.104.12.13";
const int PortaMulticast = 1099;

var grupo = IPAddress.Parse(GrupoMulticast);
var cancelamento = new CancellationTokenSource();

var tarefaReceptor = Task.Run(() => ReceptorMulticast(grupo, PortaMulticast, cancelamento.Token));
var tarefaEmissor = Task.Run(() => EmissorMulticast(grupo, PortaMulticast, cancelamento.Token));

Console.WriteLine("Aplicação iniciada. Aguarde mensagens ou escreva uma nova mensagem...");
Console.ReadLine();

cancelamento.Cancel();
Console.WriteLine("Encerrando aplicação...");

try
{
    Task.WaitAll(tarefaReceptor, tarefaEmissor);
}
catch (AggregateException ex)
{
    foreach (var erro in ex.InnerExceptions)
    {
        Console.WriteLine($"Erro: {erro.Message}");
    }
}


static void ReceptorMulticast(IPAddress grupo, int porta, CancellationToken token)
{
    using var socket = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Dgram,
        ProtocolType.Udp);

    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    socket.Bind(new IPEndPoint(IPAddress.Any, porta));

    socket.SetSocketOption(
        SocketOptionLevel.IP,
        SocketOptionName.AddMembership,
        new MulticastOption(grupo, IPAddress.Any));

    Console.WriteLine("Receptor ativo: aguardando mensagens do grupo...");

    while (!token.IsCancellationRequested)
    {
        try
        {
            var buffer = new byte[1024];
            EndPoint origem = new IPEndPoint(IPAddress.Any, 0);

            int qtdBytes = socket.ReceiveFrom(buffer, ref origem);
            var mensagem = Encoding.UTF8.GetString(buffer, 0, qtdBytes);

            Console.WriteLine($"Recebido de {origem}: {mensagem}");
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
        {
            // o cancelamento da tarefa pode gerar essa interrupção
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no receptor: {ex.Message}");
        }
    }
}


static void EmissorMulticast(IPAddress grupo, int porta, CancellationToken token)
{
    using var clienteUdp = new UdpClient();

    while (!token.IsCancellationRequested)
    {
        try
        {
            Console.Write("Você: ");
            string? mensagem = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(mensagem))
            {
                continue;
            }

            if (mensagem.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            byte[] dados = Encoding.UTF8.GetBytes(mensagem);
            clienteUdp.Send(dados, dados.Length, grupo.ToString(), porta);

            Console.WriteLine($"Mensagem enviada para {grupo}:{porta}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no emissor: {ex.Message}");
        }
    }
}

