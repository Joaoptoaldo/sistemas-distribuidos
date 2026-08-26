namespace DesafioThreadPool.Model;

/// <summary>
/// representa um registro de log
/// </summary>
public class RegistroLog
{
    public string Data { get; set; }
    public string Hora { get; set; }
    public int Codigo { get; set; }
    public string Usuario { get; set; }

    /// <summary>
    /// inicializa uma nova instância da classe RegistroLog
    /// </summary>
    /// <param name="data">data do registro</param>
    /// <param name="hora">hora do registro</param>
    /// <param name="codigo">código do registro</param>
    /// <param name="usuario">usuário do registro</param>
    public RegistroLog(string data, string hora, int codigo, string usuario)
    {
        Data = data;
        Hora = hora;
        Codigo = codigo;
        Usuario = usuario;
    }
}