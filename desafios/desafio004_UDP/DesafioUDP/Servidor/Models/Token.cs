namespace Servidor.Models;

/// <summary>
/// Representa o token global e o instante em que ele foi criado
/// </summary>
public class Token
{
    public string Valor { get; }
    public DateTime CriadoEm { get; }

    public Token()
    {
        // gera um token aleatório de 8 caracteres alfanuméricos
        Valor = Guid.NewGuid().ToString("N")[..8].ToUpper(); 
        CriadoEm = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica se o token ainda está dentro da janela de validade de 60 segundos
    /// </summary>
    /// <returns><see langword="true"/> enquanto o token permanecer válido</returns>
    public bool EstaValido()
    {
        // compara o instante atual com o instante de criação do token
        return DateTime.UtcNow - CriadoEm < TimeSpan.FromSeconds(60);
    }
}