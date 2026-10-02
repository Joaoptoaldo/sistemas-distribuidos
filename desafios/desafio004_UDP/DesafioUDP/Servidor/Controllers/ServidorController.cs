using Comunicador;
using Servidor.Models;

namespace Servidor.Controllers;

/// <summary>
/// Processa as operações recebidas pelo servidor e mantém os cadastros e o token atual.
/// Interpreta o protocolo definido em <see cref="Protocolo"/> — não conhece sockets.
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

        string[] partes = mensagem.Split(Protocolo.SeparadorCampos);

        return partes[0] switch
        {
            Protocolo.ComandoCadastro => ProcessarCadastro(partes),
            // TOKEN é válido apenas na forma exata "TOKEN" (sem campos extras)
            Protocolo.ComandoToken when partes.Length == 1 => SolicitarToken(),
            Protocolo.ComandoToken => "ERRO: Mensagem inválida.",
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

        return $"{Protocolo.PrefixoSucesso}: Pessoa cadastrada.";
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

        return $"{Protocolo.PrefixoToken}{_tokenAtual.Valor}";
    }
}