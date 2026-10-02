namespace Comunicador;

/// <summary>
/// Fonte única do protocolo de aplicação do desafio.
/// Cliente e servidor leem e escrevem o mesmo formato — evita drift de string mágica
/// </summary>
public static class Protocolo
{
    public const string SeparadorCampos = "|";

    public const string ComandoCadastro = "CADASTRO";
    public const string ComandoToken = "TOKEN";

    public const string PrefixoSucesso = "SUCESSO";
    public const string PrefixoToken = "TOKEN|";

    public const string EnderecoServidor = "127.0.0.1";
    public const int PortaServidor = 5000;

    /// <summary>
    /// Monta a mensagem de cadastro no formato CADASTRO|nome|email
    /// </summary>
    public static string MontarCadastro(string nome, string email) =>
        $"{ComandoCadastro}{SeparadorCampos}{nome}{SeparadorCampos}{email}";

    /// <summary>
    /// Monta a mensagem de solicitação de token
    /// </summary>
    public static string MontarTokenPedido() => ComandoToken;

    /// <summary>
    /// Indica se a resposta representa sucesso no protocolo
    /// </summary>
    public static bool EhSucesso(string resposta) =>
        resposta.StartsWith(PrefixoSucesso, StringComparison.Ordinal);

    /// <summary>
    /// Extrai o valor do token de uma resposta TOKEN|valor.
    /// Rejeita respostas sem prefixo ou com token vazio (TOKEN|).
    /// </summary>
    public static bool TentarExtrairToken(string resposta, out string token)
    {
        token = string.Empty;

        if (!resposta.StartsWith(PrefixoToken, StringComparison.Ordinal))
        {
            return false;
        }

        string valor = resposta[PrefixoToken.Length..];

        if (string.IsNullOrEmpty(valor))
        {
            return false;
        }

        token = valor;
        return true;
    }
}
