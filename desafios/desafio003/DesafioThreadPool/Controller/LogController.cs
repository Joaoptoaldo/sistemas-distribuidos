using DesafioThreadPool.Model;
using DesafioThreadPool.Service;

namespace DesafioThreadPool.Controller;

/// <summary>
/// responsável por controlar o fluxo de processamento dos logs
/// </summary>
public class LogController
{
    // fila de entrada para os pedaços de logs
    private readonly LeitorLogs leitor;
    private readonly WorkerPool pool;
    private readonly AgregadorResultados agregador;

    public LogController(int quantidadeTrabalhadores)
    {
        leitor = new LeitorLogs();
        pool = new WorkerPool(quantidadeTrabalhadores);
        agregador = new AgregadorResultados();
    }

    /// <summary>
    /// processa os logs, dividindo em pedaços, processando cada pedaço e agregando os resultados
    /// </summary>
    /// <param name="caminhoArquivo"> o caminho do arquivo de log a ser processado </param>
    /// <param name="tamanhoPedaco"> o tamanho de cada pedaço de log </param>
    /// <returns> o resultado final do processamento </returns>
    public ResultadoFinal Processar(
        string caminhoArquivo,
        int tamanhoPedaco)
    {
        pool.Iniciar();

        var quantidadePedacos = 0;

        foreach (var pedaco in leitor.LerPedacos(
            caminhoArquivo,
            tamanhoPedaco))
        {
            pool.Adicionar(pedaco);
            quantidadePedacos++;
        }

        pool.Finalizar();

        var resultados = new List<ResultadoParcial>();

        for (int i = 0; i < quantidadePedacos; i++)
        {
            resultados.Add(pool.ObterResultado());
        }

        return agregador.Agregar(resultados);
    }
}