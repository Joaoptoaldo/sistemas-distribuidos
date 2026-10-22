namespace GeraPessoa.Server;

public sealed class Pessoa
{
    public Pessoa(string nome, string cpf)
    {
        Nome = nome?.Trim() ?? string.Empty;
        Cpf = cpf?.Trim() ?? string.Empty;
        Email = GerarEmail(Nome);
    }

    public string Nome { get; }
    public string Cpf { get; }
    public string Email { get; }

    private static string GerarEmail(string nomeCompleto)
    {
        var partes = nomeCompleto
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (partes.Length == 0)
        {
            return string.Empty;
        }

        if (partes.Length == 1)
        {
            return $"{partes[0].ToLowerInvariant()}@ufn.edu.br";
        }

        var primeiro = partes[0];
        var ultimo = partes[^1];

        return $"{primeiro.ToLowerInvariant()}.{ultimo.ToLowerInvariant()}@ufn.edu.br";
    }
}
