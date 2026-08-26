using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Quantidade de tarefas que serão executadas no pool de threads
        int N = 5;

        // Quantidade de números em cada lista
        int TamanhoLista = 20;

        // Lista que armazenará as tarefas
        List<Task> tarefas = new List<Task>();

        for (int i = 1; i <= N; i++)
        {
            // Armazena o valor atual do contador
            // para evitar problemas com o fechamento da variável do loop
            int idTarefa = i;

            // Envia a tarefa para o ThreadPool
            tarefas.Add(Task.Run(() =>
            {
                // Cada tarefa possui sua própria lista
                List<int> lista = new List<int>();

                // Gerador de números aleatórios próprio da tarefa
                Random random = new Random();

                // 1. Popular a lista aleatoriamente
                for (int j = 0; j < TamanhoLista; j++)
                {
                    lista.Add(random.Next(1, 100));
                }

                // 2. Exibir lista original
                Console.WriteLine(
                    $"Tarefa {idTarefa} (Original): " +
                    $"{string.Join(", ", lista)}"
                );

                // 3. Ordenar a lista
                lista.Sort();

                // 4. Exibir lista ordenada
                Console.WriteLine(
                    $"Tarefa {idTarefa} (Ordenada): " +
                    $"{string.Join(", ", lista)}"
                );
            }));
        }

        // Aguarda todas as tarefas terminarem
        await Task.WhenAll(tarefas);

        Console.WriteLine("\nTodas as tarefas foram concluídas.");
    }
}



// Conceitos trabalhados

// Isolamento Total: Como as listas são criadas dentro do escopo da própria thread/tarefa, cada thread tem seu escopo de memória
// Foco no Pool: Sem a necessidade de usar travas de segurança (locks ou synchronized), pool gerencia as threads
