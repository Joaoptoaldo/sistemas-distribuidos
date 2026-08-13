using System;
using System.Collections.Generic;

namespace ex01.View;

public class ResultadosView
{
    public void MostrarResultado(int totalNumeros, List<List<int>> partes, int somaConcorrente, int somaSequencial)
    {
        Console.WriteLine("EXERCÍCIO 1: SOMA DE SUBLISTAS");

        Console.WriteLine($"Total de números gerados: {totalNumeros}");
        Console.WriteLine($"Quantidade de partes: {partes.Count}");

        for (int i = 0; i < partes.Count; i++)
        {
            Console.WriteLine($"Parte {i + 1}: {partes[i].Count} elementos");
        }

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Soma Concorrente: {somaConcorrente}");
        Console.WriteLine($"Soma Sequencial: {somaSequencial}");

        bool valida = somaConcorrente == somaSequencial;

        Console.WriteLine($"Status da Validação: {(valida ? "sucesso" : "falha")}");
    }
}

