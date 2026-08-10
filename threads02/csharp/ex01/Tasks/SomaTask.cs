using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ex01.Tasks;

public class SomaTask
{
    private readonly List<int> _sublista;
    private readonly int _numeroParte;

    public SomaTask(List<int> sublista, int numeroParte)
    {
        _sublista = sublista;
        _numeroParte = numeroParte;
    }

    public int Executar()
    {
        if (_sublista == null || _sublista.Count == 0)
        {
            return 0;
        }

        int threadId = Environment.CurrentManagedThreadId;
        DateTime inicio = DateTime.Now;
        Stopwatch sw = Stopwatch.StartNew();

        Console.WriteLine(
            $"[Parte {_numeroParte}] [Thread {threadId}] " +
            $"Iniciando às {inicio:HH:mm:ss.fff} | {_sublista.Count} elementos");

        int soma = 0;

        foreach (int numero in _sublista)
        {
            soma += numero;
        }

        sw.Stop();
        DateTime fim = DateTime.Now;

        Console.WriteLine(
            $"[Parte {_numeroParte}] [Thread {threadId}] " +
            $"Finalizado às {fim:HH:mm:ss.fff} | Duração: {sw.ElapsedMilliseconds} ms " +
            $"| Soma parcial: {soma}");

        return soma;
    }
}
