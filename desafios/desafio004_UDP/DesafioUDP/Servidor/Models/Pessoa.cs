namespace Servidor.Models;

/// <summary>
/// Representa os dados usados no cadastro de uma pessoa
/// </summary>
public class Pessoa
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public Pessoa(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}