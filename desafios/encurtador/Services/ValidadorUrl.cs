namespace Encurtador.Services;

public static class ValidadorUrl
{
    // método que tenta normalizar uma URL a partir de uma entrada do usuário
    public static bool TentarNormalizar(
        string? entrada,
        out Uri? urlNormalizada
    )

    {
        urlNormalizada = null;

        if (string.IsNullOrWhiteSpace(entrada))
            return false;

        string valor = entrada.Trim();

        // acrescenta HTTPS quando não existe protocolo
        if (!TemEsquema(valor))
            valor = $"https://{valor}";

        // tenta interpretar a entrada como uma URL absoluta
        if (!Uri.TryCreate(valor, UriKind.Absolute, out Uri? candidata))
            return false;

        // permite somente HTTP e HTTPS
        if (candidata.Scheme != Uri.UriSchemeHttp && candidata.Scheme != Uri.UriSchemeHttps)
            return false;

        // verifica se existe um host
        if (string.IsNullOrWhiteSpace(candidata.Host))
            return false;

        urlNormalizada = candidata;

        return true;
    }

    // método que verifica se uma entrada possui um esquema (protocolo) válido
    private static bool TemEsquema(string valor)
    {
        int indiceDoisPontos = valor.IndexOf(':');

        if (indiceDoisPontos <= 0)
            return false;

        string prefixo = valor[..indiceDoisPontos];

        string restante = valor[(indiceDoisPontos + 1)..];

        // diferencia um endereço com porta (localhost:5000)
        // de um protocolo (mailto:, ftp:, https:)
        bool parecePorta = restante.Length > 0 && char.IsDigit(restante[0]);

        return Uri.CheckSchemeName(prefixo) && !parecePorta;
    }
}
