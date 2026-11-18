namespace Encurtador.Models;

public sealed class LinkEncurtado
{
    public string Codigo { get; }
    public Uri UrlOriginal { get; }
    public DateTimeOffset CriadoEm { get; }

    // construtor que inicializa um link encurtado com o código, a URL original e a data de criação
    public LinkEncurtado(string codigo, Uri urlOriginal)
    {
        Codigo = codigo;
        UrlOriginal = urlOriginal;
        CriadoEm = DateTimeOffset.UtcNow;
    }
}
