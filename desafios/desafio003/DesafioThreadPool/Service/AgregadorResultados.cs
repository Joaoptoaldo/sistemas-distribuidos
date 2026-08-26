using DesafioThreadPool.Model;

namespace DesafioThreadPool.Service;

/// <summary>
/// responsável por agregar os resultados parciais em um resultado final
/// </summary>
public class AgregadorResultados
{
    /// <summary>
    /// agrega os resultados parciais em um resultado final
    /// </summary>
    /// <param name="resultados"></param>
    /// <returns></returns>
    public ResultadoFinal Agregar(
        List<ResultadoParcial> resultados)
    {
        var resultado = new ResultadoFinal
        {
            QuantidadePedacos = resultados.Count
        };

        foreach (var parcial in resultados)
        {
            resultado.QuantidadeRegistros += parcial.QuantidadeRegistros;
            resultado.Codigo0 += parcial.Codigo0;
            resultado.Codigo1 += parcial.Codigo1;
            resultado.Codigo2 += parcial.Codigo2;
            resultado.Codigo3 += parcial.Codigo3;
        }

        return resultado;
    }
}