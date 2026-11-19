namespace Encurtador.Models;

public sealed class LinkEncurtado
{
    public string Codigo { get; }
    public Uri UrlOriginal { get; }
    public DateTimeOffset CriadoEm { get; }

    public LinkEncurtado(
        string codigo,
        Uri urlOriginal,
        DateTimeOffset? criadoEm = null)
    {
        Codigo = codigo;
        UrlOriginal = urlOriginal;
        CriadoEm = criadoEm ?? DateTimeOffset.UtcNow;
    }
}