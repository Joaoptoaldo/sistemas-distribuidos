import controller.DadosController;
import model.Dados;
import view.DadosView;

public class Main {

    public static void main(String[] args) {

        Dados dados = new Dados();
        DadosController controller = new DadosController();
        DadosView view = new DadosView();

        view.mostrarInicio();

        controller.carregarDados(dados);

        controller.exibirDados(dados);

        view.mostrarFim();
    }
}