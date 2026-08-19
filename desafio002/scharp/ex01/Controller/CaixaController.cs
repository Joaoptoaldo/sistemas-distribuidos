using ex01.Service;
using ex01.Model;
using ex01.View;
using System.Threading;

namespace ex01.Controller;

public class CaixaController
{
    // readonly é usado para indicar que a variável não pode ser reatribuída após a inicialização
    private readonly CaixaView _caixaView;
    private readonly CaixaService _caixaService;

    public CaixaController()
    {
        _caixaService = new CaixaService();
        _caixaView = new CaixaView();
    }

    /// <summary>
    /// método que executa a simulação de vendas de fichas em um caixa, atualizando o saldo e o número de fichas vendidas
    /// </summary>
    public void ExecutarVendas()
    {
        decimal saldoCentral = 0;

        // objeto de bloqueio para sincronizar o acesso ao saldoCentral entre as threads
        object lockSaldo = new object();
        
        // cria as 5 threads
        Thread[] threads = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            int idCaixa = i + 1;

            // em cada thread, cria um novo caixa e realiza 1000 vendas de fichas
            threads[i] = new Thread(() =>
            {
                Caixa caixa = new Caixa
                {
                    Id = idCaixa,
                    FichasVendidas = 0
                };

                // cada thread executa isso 1.000 vezes
                for (int j = 0; j < 1000; j++)
                {
                    // como existem 5 threads: 5 * 1000 = 5000 vendas
                    // lock vai garantir que apenas uma thread por vez possa acessar o saldoCentral, 
                    // evitando condições de corrida
                    lock (lockSaldo)
                    {
                        saldoCentral = _caixaService.RealizarVenda(saldoCentral);
                    }

                    caixa.FichasVendidas++;
                }

                _caixaView.ExibirCaixa(caixa);
            });

            // inicia a thread
            threads[i].Start();
        }

        // aguarda todas as threads terminarem antes de continuar
        foreach (Thread thread in threads)
        {
            // Join() vai bloquear a thread principal até que a thread atual termine sua execução
            thread.Join();
        }
        
        _caixaView.ExibirSaldo(saldoCentral);
    }
}