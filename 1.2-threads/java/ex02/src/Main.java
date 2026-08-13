import java.io.IOException;
import java.util.List;

import controller.FiltroController;
import model.Dados;
import view.ResultadosView;

public class Main {
    public static void main(String[] args) {
        String caminhoArquivo = "src/data/usuarios.txt";

        Dados dados = new Dados();

        try {

            List<String> nomes = dados.carregarNomes(caminhoArquivo);

            List<List<String>> partes = dados.dividirEmPartes(nomes);

            FiltroController controller = new FiltroController();

            List<String> resultado = controller.processar(partes);

            ResultadosView view = new ResultadosView();

            view.mostrarResultado(nomes, partes, resultado);

        } catch (IOException e) {
            System.err.println("Erro ao ler o arquivo: " + e.getMessage());

        } catch (RuntimeException e) {
            System.err.println("Erro durante o processamento: " + e.getMessage());
        }
    }
}
