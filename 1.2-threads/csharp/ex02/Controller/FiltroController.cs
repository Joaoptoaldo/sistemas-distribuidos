using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ex02.Tasks;

namespace ex02.Controller;

public class FiltroController
{
    private const int NumThreads = 2;

    public async Task<List<string>> ProcessarAsync(List<List<string>> partes)
    {
        if (partes == null || partes.Count != NumThreads)
        {
            throw new ArgumentException("É necessário fornecer exatamente 2 partes!");
        }

        List<Task<List<string>>> tarefas = new();

        for (int i = 0; i < partes.Count; i++)
        {
            int numeroParte = i + 1;
            FiltroTask tarefa = new(partes[i], numeroParte);
            tarefas.Add(Task.Run(() => tarefa.Executar()));
        }

        List<string>[] resultadosDasPartes = await Task.WhenAll(tarefas);

        // Agrupa todas as listas retornadas pelas tarefas em uma única lista final
        List<string> resultadoFinal = resultadosDasPartes.SelectMany(r => r).ToList();

        return resultadoFinal;
    }
}
