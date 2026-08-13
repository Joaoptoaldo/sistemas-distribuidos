using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ex01.Tasks;

namespace ex01.Controller;

public class SomaController
{
    public async Task<int> CalcularSomaTotalAsync(List<List<int>> partes)
    {
        if (partes == null || partes.Count == 0)
        {
            return 0;
        }

        List<Task<int>> tarefas = new();

        for (int i = 0; i < partes.Count; i++)
        {
            int numeroParte = i + 1;
            SomaTask task = new(partes[i], numeroParte);
            tarefas.Add(Task.Run(() => task.Executar()));
        }

        int[] resultados = await Task.WhenAll(tarefas);

        return resultados.Sum();
    }
}
