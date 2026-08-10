using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ex02.Model;

public class Dados
{
    private const int NumPartes = 2;

    public List<string> CarregarNomes(string caminho)
    {
        if (string.IsNullOrWhiteSpace(caminho))
        {
            throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio");
        }

        return File.ReadAllLines(caminho).ToList();
    }

    public List<List<string>> DividirEmPartes(List<string> nomes)
    {
        if (nomes == null || nomes.Count == 0)
        {
            throw new ArgumentException("A lista de nomes não pode ser nula ou vazia.");
        }

        List<List<string>> partes = new();

        int tamanhoBase = nomes.Count / NumPartes;
        int resto = nomes.Count % NumPartes;
        int inicio = 0;

        for (int i = 0; i < NumPartes; i++)
        {
            int tamanhoParte = tamanhoBase + (i < resto ? 1 : 0);

            partes.Add(nomes.GetRange(inicio, tamanhoParte));

            inicio += tamanhoParte;
        }

        return partes;
    }
}
