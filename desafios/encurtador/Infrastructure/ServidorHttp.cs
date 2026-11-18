using Encurtador.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Encurtador.Infrastructure;

public sealed class ServidorHttp : IAsyncDisposable
{
    private WebApplication? _aplicacao;

    public async Task IniciarAsync(EncurtadorService servico)
    {
        if (_aplicacao is not null)
            return;

        var construtor = WebApplication.CreateBuilder();

        construtor.WebHost.UseUrls("http://localhost:5000");

        // Compartilha a mesma instância utilizada pelo Windows Forms.
        construtor.Services.AddSingleton(servico);

        var aplicacao = construtor.Build();

        // Rota inicial para verificar se o servidor está funcionando.
        aplicacao.MapGet("/", () =>
            Results.Ok("Servidor do Encurtador está funcionando!")
        );

        // Rota responsável por localizar o código e redirecionar.
        aplicacao.MapGet("/{codigo}", (
            string codigo,
            EncurtadorService encurtador) =>
        {
            if (!encurtador.TentarBuscar(codigo, out var link))
            {
                return Results.NotFound("Link não encontrado.");
            }

            return Results.Redirect(link!.UrlOriginal.AbsoluteUri);
        });

        await aplicacao.StartAsync();

        _aplicacao = aplicacao;
    }

    public async ValueTask DisposeAsync()
    {
        if (_aplicacao is null)
            return;

        await _aplicacao.StopAsync();
        await _aplicacao.DisposeAsync();

        _aplicacao = null;
    }
}
