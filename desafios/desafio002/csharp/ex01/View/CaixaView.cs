using ex01.Model;

namespace ex01.View;

public class CaixaView
{
    /// <summary>
    /// método que exibe as informações de um caixa
    /// </summary>
    /// <param name="caixa"></param>
    public void ExibirCaixa(Caixa caixa)
    {
        Console.WriteLine($"Caixa {caixa.Id} terminou: {caixa.FichasVendidas} fichas");
    }

    /// <summary>
    /// método que exibe o saldo central do caixa
    /// </summary>
    /// <param name="saldo"></param>
    public void ExibirSaldo(decimal saldo)
    {
        Console.WriteLine();
        Console.WriteLine($"Saldo central: R$ {saldo:F2}");
    }
}