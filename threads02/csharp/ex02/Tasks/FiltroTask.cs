using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ex02.Tasks;

public class FiltroTask
{
    private readonly List<string> _nomes;
    private readonly int _numeroParte;

    public FiltroTask(List<string> nomes, int numeroParte)
    {
        // ArgumentNullException é o tipo correto para referência nula
        ArgumentNullException.ThrowIfNull(nomes);

        _nomes = nomes;
        _numeroParte = numeroParte;
    }


    public List<string> Executar()
    {
        int threadId = Environment.CurrentManagedThreadId;
        DateTime inicio = DateTime.Now;
        Stopwatch sw = Stopwatch.StartNew();

        Console.WriteLine(
            $"[Parte {_numeroParte}] [Thread {threadId}] " +
            $"Iniciando às {inicio:HH:mm:ss.fff} | {_nomes.Count} elementos");

        List<string> nomesLimpos = new(_nomes.Count);

        foreach (string nome in _nomes)
        {
            // Itens null são ignorados sem alterar os dados válidos
            if (nome is null)
            {
                continue;
            }

            nomesLimpos.Add(nome.Trim().ToUpperInvariant());
        }

        sw.Stop();
        DateTime fim = DateTime.Now;

        Console.WriteLine(
            $"[Parte {_numeroParte}] [Thread {threadId}] " +
            $"Finalizado às {fim:HH:mm:ss.fff} | Duração: {sw.ElapsedMilliseconds} ms " +
            $"| Elementos retornados: {nomesLimpos.Count}");

        return nomesLimpos;
    }
}
