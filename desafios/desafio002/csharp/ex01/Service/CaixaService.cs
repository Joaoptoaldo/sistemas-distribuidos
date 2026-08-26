namespace ex01.Service;

public class CaixaService
{
    private const decimal VALOR_FICHA = 10.00m; // valor fixo (10 pila) de cada ficha vendida, m indica que é um decimal

    /// <summary>
    /// método que realiza a venda de uma ficha e atualiza o saldo atual
    /// </summary>
    /// <param name="saldoAtual">saldo atual do caixa</param>
    /// <returns>retorna o novo saldo após a venda</returns>
    public decimal RealizarVenda(decimal saldoAtual)
    {
        return saldoAtual + VALOR_FICHA;
    }
}