using GeraPessoa.Contracts;
using Grpc.Net.Client;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

using var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new GeraPessoaService.GeraPessoaServiceClient(channel);

Console.WriteLine("Cliente gRPC - Geração de Pessoa");
Console.Write("Nome completo: ");
var nome = Console.ReadLine() ?? string.Empty;

Console.Write("CPF: ");
var cpf = Console.ReadLine() ?? string.Empty;

var resposta = client.GerarPessoa(new PessoaRequest
{
    Nome = nome,
    Cpf = cpf
});

Console.WriteLine();
Console.WriteLine("Dados retornados do servidor:");
Console.WriteLine($"Nome: {resposta.Nome}");
Console.WriteLine($"CPF: {resposta.Cpf}");
Console.WriteLine($"E-mail: {resposta.Email}");
