
using Encurtador.Models;

namespace Encurtador.Services;

public class EncurtadorService
{   
    // dois dicionários para armazenar os links encurtados e os códigos por URL
    private readonly Dictionary<string, LinkEncurtado> _links = new();

    private readonly Dictionary<string, string> _codigosPorUrl = new();

    // metodo que cria um link encurtado a partir de uma URL original
    public LinkEncurtado Criar(Uri urlOriginal)
    {
        string chaveUrl = urlOriginal.AbsoluteUri;

        // verifica se a URL já foi cadastrada
        if (_codigosPorUrl.TryGetValue(chaveUrl, out string? codigoExistente))
        {
            return _links[codigoExistente];
        }

        const int maxTentativas = 5;

        for (int tentativa = 0; tentativa < maxTentativas; tentativa++)
        {
            string codigo = GeradorCodigo.Gerar();

            var link = new LinkEncurtado(codigo, urlOriginal);

            if (_links.TryAdd(codigo, link))
            {
                _codigosPorUrl.Add(chaveUrl, codigo);

                return link;
            }
        }

        throw new InvalidOperationException(
            "Não foi possível gerar um código exclusivo."
        );
    }

    // metodo que tenta buscar um link encurtado pelo código
    public bool TentarBuscar(
        string codigo,
        out LinkEncurtado? link
    )

    {
        return _links.TryGetValue(codigo, out link);
    }

    // método que obtém a URL encurtada a partir de um link encurtado
    public string ObterUrlEncurtada(LinkEncurtado link)
    {
        return $"http://localhost:5000/{link.Codigo}";
    }

}
