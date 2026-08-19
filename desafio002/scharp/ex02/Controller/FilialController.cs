using ex02.Model;
using ex02.View;

namespace ex02.Controller;

public class FilialController
{
    private readonly FilialView _filialView;

    public FilialController()
    {
        _filialView = new FilialView();
    }

    /// <summary>
    /// executa o processo de cálculo do faturamento total das filiais
    /// </summary>
    public void Executar()
    {
        List<decimal> vendasFilial1 = GerarVendas();
        List<decimal> vendasFilial2 = GerarVendas();
        List<decimal> vendasFilial3 = GerarVendas();
        List<decimal> vendasFilial4 = GerarVendas();

        // cria objetos Filial para armazenar os resultados de cada filial
        Filial filial1 = new Filial { Id = 1 };
        Filial filial2 = new Filial { Id = 2 };
        Filial filial3 = new Filial { Id = 3 };
        Filial filial4 = new Filial { Id = 4 };


        // cria uma thread para cada filial, que calcula o total de vendas de forma concorrente
        Thread thread1 = new Thread(() =>
        {
            filial1.Total = CalcularTotal(vendasFilial1);
        });

        Thread thread2 = new Thread(() =>
        {
            filial2.Total = CalcularTotal(vendasFilial2);
        });

        Thread thread3 = new Thread(() =>
        {
            filial3.Total = CalcularTotal(vendasFilial3);
        });

        Thread thread4 = new Thread(() =>
        {
            filial4.Total = CalcularTotal(vendasFilial4);
        });

        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();
        thread4.Join();

        // calcula o faturamento total somando os totais de cada filial
        decimal faturamentoTotal = filial1.Total + filial2.Total + filial3.Total + filial4.Total;



         // exibe o resultado de cada filial
        _filialView.ExibirResultado(filial1);
        _filialView.ExibirResultado(filial2);
        _filialView.ExibirResultado(filial3);
        _filialView.ExibirResultado(filial4);

        _filialView.ExibirFaturamentoTotal(faturamentoTotal);
    }

    /// <summary>
    /// método que calcula o total de vendas de uma filial
    /// </summary>
    /// <param name="vendas"></param>
    /// <returns></returns>
    private decimal CalcularTotal(List<decimal> vendas)
    {
        decimal total = 0;

        foreach (decimal venda in vendas)
        {
            total += venda;
        }

        return total;
    }

    /// <summary>
    /// método que gera uma lista de vendas
    /// </summary>
    /// <returns></returns>
    private List<decimal> GerarVendas()
    {
        List<decimal> vendas = new();

        Random random = new();

        for (int i = 0; i < 10000; i++)
        {
            vendas.Add(random.Next(10, 1000));
        }

        return vendas;
    }
}