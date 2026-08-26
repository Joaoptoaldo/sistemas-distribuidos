using ex02.Model;

namespace ex02.View;

public class FilialView
{
    /// <summary>
    /// método que exibe o resultado de uma filial
    /// </summary>
    /// <param name="filial"></param>
    public void ExibirResultado(Filial filial)
    {
        Console.WriteLine(
            $"Filial {filial.Id}: R$ {filial.Total:F2}"
        );
    }

    /// <summary>
    /// método que exibe o faturamento total de todas as filiais
    /// </summary>
    /// <param name="total"></param>
    public void ExibirFaturamentoTotal(decimal total)
    {
        Console.WriteLine();
        Console.WriteLine($"Faturamento total: R$ {total:F2}");
    }
}