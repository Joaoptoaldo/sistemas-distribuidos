import java.util.List;

import controller.SomaController;
import model.Dados;
import view.ResultadosView;

public class Main {

    public static void main(String[] args) {

        Dados dados = new Dados();
        List<Integer> numeros = dados.gerarNumeros();
        List<List<Integer>> partes = dados.dividirEmPartes(numeros);

        SomaController controller = new SomaController();
        int somaTotal = controller.calcularSomaTotal(partes);

        // soma sequencial para validacao
        int somaSequencial = numeros.stream().mapToInt(Integer::intValue).sum();

        ResultadosView view = new ResultadosView();
        view.mostrarResultado(numeros.size(), partes, somaTotal, somaSequencial);
    }
}

