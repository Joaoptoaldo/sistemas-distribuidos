using DesafioThreadPool.Model;

namespace DesafioThreadPool.Service;

/// <summary>
/// responsável por ler os logs e dividir em pedaços
/// </summary>
public class LeitorLogs
{
    /// <summary>
    /// lê os logs e divide em pedaços
    /// </summary>
    /// <param name="caminho"> caminho do arquivo de logs </param>
    /// <param name="tamanhoPedaco"> tamanho de cada pedaço </param>
    /// <returns></returns>
    public IEnumerable<PedacoLog> LerPedacos(
        string caminho,
        int tamanhoPedaco)
    {
        var registros = new List<RegistroLog>();
        var idPedaco = 1;

        foreach (var linha in File.ReadLines(caminho))
        {
            var partes = linha.Split(',');

            var registro = new RegistroLog(
                partes[0],
                partes[1],
                int.Parse(partes[2]),
                partes[3]
            );

            registros.Add(registro);

            if (registros.Count == tamanhoPedaco)
            {
                yield return new PedacoLog(idPedaco, registros);

                idPedaco++;
                registros = new List<RegistroLog>();
            }
        }

        if (registros.Count > 0)
        {
            yield return new PedacoLog(idPedaco, registros);
        }
    }
}