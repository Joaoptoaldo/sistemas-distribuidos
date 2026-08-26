using DesafioThreadPool.Model;

namespace DesafioThreadPool.View;

/// <summary>
/// responsável por exibir o resultado final do processamento dos logs
/// </summary>
public class LogView
{
    /// <summary>
    /// método que exibe o resultado final do processamento dos logs
    /// </summary>
    /// <param name="resultado"></param>
    public void Exibir(ResultadoFinal resultado)
    {
        Console.WriteLine();
        Console.WriteLine("--- Análise de Logs ---");
        Console.WriteLine();

        Console.WriteLine(
            $"Pedaços processados: {resultado.QuantidadePedacos}"
        );

        Console.WriteLine(
            $"Registros processados: {resultado.QuantidadeRegistros}"
        );

        Console.WriteLine();

        Console.WriteLine("Quantidade por código:");
        Console.WriteLine($"Código 0: {resultado.Codigo0}");
        Console.WriteLine($"Código 1: {resultado.Codigo1}");
        Console.WriteLine($"Código 2: {resultado.Codigo2}");
        Console.WriteLine($"Código 3: {resultado.Codigo3}");

        Console.WriteLine();

    }
}