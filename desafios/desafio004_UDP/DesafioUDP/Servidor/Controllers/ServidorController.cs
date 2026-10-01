using Servidor.Models;

namespace Servidor.Controllers;

/// <summary>
/// Processa as operações recebidas pelo servidor e mantém os cadastros e o token atual
/// </summary>
public class ServidorController
{
    private readonly List<Pessoa> _pessoas = new();

    private Token? _tokenAtual;

    /// <summary>
    /// Interpreta uma mensagem do protocolo e produz a resposta correspondente
    /// </summary>
    /// <param name="mensagem">Mensagem recebida via UDP</param>
    /// <returns>Resposta no formato definido pelo protocolo do desafio</returns>
    public string ProcessarMensagem(string mensagem)
    {
        if (string.IsNullOrWhiteSpace(mensagem))
        {
            return "ERRO: Mensagem inválida.";
        }

        string[] partes = mensagem.Split('|');

        return partes[0] switch
        {
            "CADASTRO" => ProcessarCadastro(partes),
            "TOKEN" => SolicitarToken(),
            _ => "ERRO: Operação desconhecida."
        };
    }

    private string ProcessarCadastro(string[] partes)
    {
        if (partes.Length != 3)
        {
            return "ERRO: Dados de cadastro inválidos.";
        }

        string nome = partes[1].Trim();
        string email = partes[2].Trim();

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email))
        {
            return "ERRO: Nome e e-mail são obrigatórios.";
        }

        return CadastrarPessoa(nome, email);
    }

    /// <summary>
    /// Adiciona uma pessoa quando ainda não existe cadastro com o mesmo e-mail
    /// </summary>
    private string CadastrarPessoa(string nome, string email)
    {
        // verifica se já existe uma pessoa com o mesmo e-mail
        Pessoa? pessoaExistente = _pessoas.FirstOrDefault(
            p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
        );

        if (pessoaExistente is not null)
        {
            return "ERRO: Pessoa já cadastrada.";
        }

        Pessoa pessoa = new(nome, email);

        _pessoas.Add(pessoa);

        return "SUCESSO: Pessoa cadastrada.";
    }

    /// <summary>
    /// Retorna o token global atual ou cria outro quando sua validade termina
    /// </summary>
    private string SolicitarToken()
    {
        // cria um novo token caso não exista ou o atual tenha expirado
        if (_tokenAtual == null || !_tokenAtual.EstaValido())
        {
            _tokenAtual = new Token();
        }

        return $"TOKEN|{_tokenAtual.Valor}";
    }
}