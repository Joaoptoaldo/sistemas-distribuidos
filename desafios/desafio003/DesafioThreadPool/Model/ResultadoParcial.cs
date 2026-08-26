namespace DesafioThreadPool.Model;

/// <summary>
/// representa um resultado parcial do processamento de um pedaço de log
/// </summary>
public class ResultadoParcial
{
    public int IdPedaco { get; set; }
    public int QuantidadeRegistros { get; set; }
    public int Codigo0 { get; set; }
    public int Codigo1 { get; set; }
    public int Codigo2 { get; set; }
    public int Codigo3 { get; set; }

    public ResultadoParcial(
        int idPedaco,
        int quantidadeRegistros,
        int codigo0,
        int codigo1,
        int codigo2,
        int codigo3)
    {
        IdPedaco = idPedaco;
        QuantidadeRegistros = quantidadeRegistros;
        Codigo0 = codigo0;
        Codigo1 = codigo1;
        Codigo2 = codigo2;
        Codigo3 = codigo3;
    }
}