using System;
using System.Collections.Generic;
using System.IO;
using ex02.Controller;
using ex02.Model;
using ex02.View;

string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Data", "usuarios.txt");

if (!File.Exists(caminhoArquivo))
{
    caminhoArquivo = "Data/usuarios.txt";
}

Dados dados = new();

try
{
    List<string> nomes = dados.CarregarNomes(caminhoArquivo);
    List<List<string>> partes = dados.DividirEmPartes(nomes);

    FiltroController controller = new();
    List<string> resultado = await controller.ProcessarAsync(partes);

    ResultadosView view = new();
    view.MostrarResultado(nomes, partes, resultado);
}
catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"Erro ao ler o arquivo: {e.Message}");
}
catch (Exception e)
{
    Console.Error.WriteLine($"Erro durante o processamento: {e.Message}");
}
