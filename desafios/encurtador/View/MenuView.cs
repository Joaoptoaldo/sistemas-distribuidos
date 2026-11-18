using Encurtador.Services;

namespace Encurtador.View;

public class MenuView
{
    private readonly EncurtadorService _servico;

    public MenuView(EncurtadorService servico)
    {
        _servico = servico;
    }

    public void Executar()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine();
            Console.WriteLine("ENCURTADOR DE URL");
            Console.WriteLine("1 - Encurtar URL");
            Console.WriteLine("2 - Consultar link");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine() ?? "";

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    EncurtarUrl();
                    break;

                case "2":
                    ConsultarLink();
                    break;

                case "0":
                    executando = false;
                    Console.WriteLine("Encerrando o programa...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    // método que solicita ao usuário uma URL e a encurta
    private void EncurtarUrl()
    {
        Console.Write("Digite a URL: ");

        string entrada = Console.ReadLine() ?? "";

        if (!ValidadorUrl.TentarNormalizar(entrada, out Uri? url))
        {
            Console.WriteLine("URL inválida.");
            return;
        }

        var link = _servico.Criar(url!);
        string urlEncurtada = _servico.ObterUrlEncurtada(link);

        Console.WriteLine();
        Console.WriteLine("URL encurtada com sucesso!");
        Console.WriteLine($"Original: {link.UrlOriginal}");
        Console.WriteLine($"Encurtada: {urlEncurtada}");
    }

    // método que solicita ao usuário um código e consulta o link encurtado
    private void ConsultarLink()
    {
        Console.Write("Digite o código do link: ");

        string codigo = Console.ReadLine() ?? "";

        if (_servico.TentarBuscar(codigo, out var link) && link is not null)
        {
            Console.WriteLine();
            Console.WriteLine("Link encontrado!");
            Console.WriteLine($"Código: {link.Codigo}");
            Console.WriteLine($"URL original: {link.UrlOriginal}");
        }
        else
        {
            Console.WriteLine("Link não encontrado.");
        }
    }
}
