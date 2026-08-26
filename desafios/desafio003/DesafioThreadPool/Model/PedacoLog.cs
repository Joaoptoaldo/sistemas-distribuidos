namespace DesafioThreadPool.Model;

/// <summary>
/// representa um pedaço de log
/// </summary>
public class PedacoLog
{
    public int Id { get; set; }
    public List<RegistroLog> Registros { get; set; }

    /// <summary>
    /// inicializa uma nova instância da classe PedacoLog
    /// </summary>
    /// <param name="id">id do pedaço de log</param>
    /// <param name="registros">registros do pedaço de log</param>
    public PedacoLog(int id, List<RegistroLog> registros)
    {
        Id = id;
        Registros = registros;
    }
}