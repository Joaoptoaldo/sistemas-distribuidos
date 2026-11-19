using Encurtador.Models;
using Encurtador.Repositories;

namespace Encurtador.Services;

public class EncurtadorService
{
    private readonly LinkEncurtidoRepositorio _repositorio;

    public List<LinkEncurtado> ListarTodos()
    {
        return _repositorio.ListarTodos();
    }

    public bool Excluir(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            return false;

        return _repositorio.ExcluirPorCodigo(codigo);
    }

    public EncurtadorService()
        : this(new LinkEncurtidoRepositorio())
    {
    }

    public EncurtadorService(
        LinkEncurtidoRepositorio repositorio)
    {
        _repositorio = repositorio
            ?? throw new ArgumentNullException(nameof(repositorio));
    }

    // Cria um link encurtado ou recupera um já existente.
    public LinkEncurtado Criar(Uri urlOriginal)
    {
        ArgumentNullException.ThrowIfNull(urlOriginal);

        string chaveUrl = urlOriginal.AbsoluteUri;

        // Verifica se a URL já está cadastrada.
        var linkExistente = _repositorio.ObterPorUrlOriginal(
            chaveUrl
        );

        if (linkExistente is not null)
            return linkExistente;

        const int maxTentativas = 5;

        for (int tentativa = 0; tentativa < maxTentativas; tentativa++)
        {
            string codigo = GeradorCodigo.Gerar();

            var link = new LinkEncurtado(
                codigo,
                urlOriginal
            );

            if (_repositorio.TentarInserir(link))
                return link;

            // Se outra operação cadastrou a mesma URL,
            // reutiliza o registro existente.
            linkExistente = _repositorio.ObterPorUrlOriginal(
                chaveUrl
            );

            if (linkExistente is not null)
                return linkExistente;

            // Se não existe a URL, houve colisão de código.
            // A próxima tentativa gerará outro código.
        }

        throw new InvalidOperationException(
            "Não foi possível gerar um código exclusivo."
        );
    }

    // Busca um link pelo código.
    public bool TentarBuscar(
        string codigo,
        out LinkEncurtado? link)
    {
        link = _repositorio.ObterPorCodigo(codigo);

        return link is not null;
    }

    // Obtém a URL encurtada.
    public string ObterUrlEncurtada(LinkEncurtado link)
    {
        return $"http://localhost:5000/{link.Codigo}";
    }
}