using System;
using System.Collections.Generic;
using System.Linq;

namespace ex02.View;

public class ResultadosView
{
    public void MostrarResultado(List<string> nomesOriginais, List<List<string>> partes, List<string> resultado)
    {
        Console.WriteLine("----- EXERCÍCIO 2: FILTRO DE DADOS -----");

        Console.WriteLine($"Total de nomes originais: {nomesOriginais.Count}");
        Console.WriteLine($"Quantidade de tarefas: {partes.Count}");

        for (int i = 0; i < partes.Count; i++)
        {
            Console.WriteLine($"Parte {i + 1}: {partes[i].Count} nomes");
        }

        Console.WriteLine($"Total processado: {resultado.Count}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Exemplos após limpeza:\n");

        foreach (string nome in resultado.Take(10))
        {
            Console.WriteLine($" - {nome}");
        }

        Console.WriteLine("--------------------------------------------------");
    }
}
