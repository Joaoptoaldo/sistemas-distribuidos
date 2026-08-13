using System.Linq;
using ex01.Controller;
using ex01.Model;
using ex01.View;

Dados dados = new();

List<int> numeros = dados.GerarNumeros();

List<List<int>> partes = dados.DividirEmPartes(numeros);

SomaController controller = new();

int somaConcorrente = await controller.CalcularSomaTotalAsync(partes);

int somaSequencial = numeros.Sum();

ResultadosView view = new();

view.MostrarResultado(
    numeros.Count,
    partes,
    somaConcorrente,
    somaSequencial);
