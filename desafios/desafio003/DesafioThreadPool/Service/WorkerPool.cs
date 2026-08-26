using System.Collections.Concurrent;
using DesafioThreadPool.Model;

namespace DesafioThreadPool.Service;

/// <summary>
/// responsável por gerenciar um pool de trabalhadores para processar pedaços de logs
/// </summary>
public class WorkerPool
{
    // fila de entrada para os pedaços de logs 
    private readonly BlockingCollection<PedacoLog> filaEntrada;
    private readonly BlockingCollection<ResultadoParcial> filaSaida;
    private readonly int quantidadeTrabalhadores;

    public WorkerPool(int quantidadeTrabalhadores)
    {
        this.quantidadeTrabalhadores = quantidadeTrabalhadores;

        filaEntrada = new BlockingCollection<PedacoLog>();
        filaSaida = new BlockingCollection<ResultadoParcial>();
    }

    public void Iniciar()
    {
        for (int i = 0; i < quantidadeTrabalhadores; i++)
        {
            ThreadPool.QueueUserWorkItem(_ => Trabalhar());
        }
    }

    public void Adicionar(PedacoLog pedaco)
    {
        filaEntrada.Add(pedaco);
    }

    public void Finalizar()
    {
        filaEntrada.CompleteAdding();
    }

    public ResultadoParcial ObterResultado()
    {
        return filaSaida.Take();
    }

    /// <summary>
    /// método que representa o trabalho de cada trabalhador, 
    /// processando os pedaços de logs da fila de entrada e adicionando os resultados na fila de saída
    /// </summary>
    private void Trabalhar()
    {
        foreach (var pedaco in filaEntrada.GetConsumingEnumerable())
        {
            try
            {
                var resultado = Processar(pedaco);
                filaSaida.Add(resultado);
            }
            catch (Exception ex)
            {
                // devolve um resultado marcado como erro em vez de deixar a thread morrer
                filaSaida.Add(new ResultadoParcial(pedaco.Id, 0, 0, 0, 0, 0));
                Console.Error.WriteLine($"Erro ao processar pedaço {pedaco.Id}: {ex.Message}");
            }
        }
    }


    /// <summary>
    /// processa um pedaço de log, contando a quantidade de registros e a quantidade de cada código
    /// </summary>
    /// <param name="pedaco">o pedaço de log a ser processado</param>
    /// <returns>o resultado parcial do processamento </returns>
    private ResultadoParcial Processar(PedacoLog pedaco)
    {

        var codigo0 = 0;
        var codigo1 = 0;
        var codigo2 = 0;
        var codigo3 = 0;

        // percorre cada registro do pedaço de log e conta a quantidade de cada código
        foreach (var registro in pedaco.Registros)
        {
            switch (registro.Codigo)
            {
                case 0:
                    codigo0++;
                    break;

                case 1:
                    codigo1++;
                    break;

                case 2:
                    codigo2++;
                    break;

                case 3:
                    codigo3++;
                    break;
            }
        }

    return new ResultadoParcial(
        pedaco.Id,
        pedaco.Registros.Count,
        codigo0,
        codigo1,
        codigo2,
        codigo3
    );
}
}