using GeraPessoaServer = GeraPessoa.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GeraPessoaServer.GeraPessoa>();
app.MapGet("/", () => "Servidor gRPC em execução. Use o cliente para gerar uma Pessoa.");

app.Run();
