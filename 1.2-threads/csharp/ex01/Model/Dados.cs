using System;
using System.Collections.Generic;

namespace ex01.Model;

public class Dados
{
    private const int QtdTotal = 10000;
    private const int NumPartes = 4;

    public List<int> GerarNumeros()
    {
        List<int> numeros = new();

        for (int i = 0; i < QtdTotal; i++)
        {
            numeros.Add(Random.Shared.Next(1, 101));
        }

        return numeros;
    }

    public List<List<int>> DividirEmPartes(List<int> lista)
    {
        List<List<int>> partes = new();

        int tamanhoBase = lista.Count / NumPartes;
        int resto = lista.Count % NumPartes;
        int inicio = 0;

        for (int i = 0; i < NumPartes; i++)
        {
            int tamanhoParte = tamanhoBase + (i < resto ? 1 : 0);

            partes.Add(lista.GetRange(inicio, tamanhoParte));

            inicio += tamanhoParte;
        }

        return partes;
    }
}
